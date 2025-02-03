using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Squid.Prism.Engine.Core.Interfaces.Modules;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Modules;
using Squid.Prism.Server.Data.Directories;
using Squid.Prism.Server.Data.Runtime;
using Squid.Prism.Server.Modules;
using Squid.Prism.Server.Services;
using Squid.Prism.Server.Types;

namespace Squid.Prism.Server.Builder;

public class SquidPrismServerBuilder
{
    delegate void OnBuiltDelegate(IHost host);

    event OnBuiltDelegate OnBuilt;


    private readonly HostApplicationBuilder _hostApplicationBuilder;

    private readonly LoggerConfiguration _loggerConfiguration;

    private IHost _applicationHost = null!;

    public static SquidPrismServerBuilder Create(string[] args)
    {
        return new SquidPrismServerBuilder(args);
    }

    public SquidPrismServerBuilder(string[] args)
    {
        var rootDirectory = Environment.GetEnvironmentVariable("SQUID_PRISM_ROOT_DIRECTORY") ??
                            Path.Combine(Environment.CurrentDirectory, "sprism");

        var directoryConfig = new DirectoriesConfig(rootDirectory);

        _hostApplicationBuilder = Host.CreateApplicationBuilder(args);

        _hostApplicationBuilder.Services.AddSingleton(directoryConfig);

        _loggerConfiguration = new LoggerConfiguration().WriteTo.File(
            rollingInterval: RollingInterval.Day,
            path: Path.Combine(directoryConfig[DirectoryType.Logs], "squid_prism_server_.log")
        );

        //Log.Logger = _loggerConfiguration.CreateLogger();

        var runtimeData = new RuntimeData
        {
            IsDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true",
            RootDirectory = rootDirectory,
            ProcessCount = Environment.ProcessorCount,
            ProcessId = Environment.ProcessId
        };


        _hostApplicationBuilder.Services.AddSingleton(runtimeData);

        _hostApplicationBuilder.Services.AddSingleton<ISquidPrismServiceProvider, SquidPrismServiceProvider>();

        _hostApplicationBuilder.Services
            .LoadContainerModule<ServerServicesModule>()
            .LoadContainerModule<CoreServiceModule>();

        _hostApplicationBuilder.Services.AddHostedService<SquidPrismServerManager>();

        ConfigureLogger(null);
    }


    public SquidPrismServerBuilder ConfigureLogger(Action<LoggerConfiguration>? configure)
    {
        if (configure != null)
        {
            configure(_loggerConfiguration);
        }


        return this;
    }

    public IHost Build()
    {
        Log.Logger = _loggerConfiguration.CreateLogger();

        _hostApplicationBuilder.Logging.ClearProviders().AddSerilog();

        _applicationHost = _hostApplicationBuilder.Build();

        PrintBanner();

        OnBuilt?.Invoke(_applicationHost);

        return _applicationHost;
    }

    public SquidPrismServerBuilder RegisterPrismService<TServiceInterface, TServiceImplementation>(
        int priority = 0
    ) where TServiceInterface : class where TServiceImplementation : class, TServiceInterface
    {
        _hostApplicationBuilder.Services.RegisterPrismService<TServiceInterface, TServiceImplementation>(priority);

        return this;
    }

    public SquidPrismServerBuilder RegisterPrismService<TService>(int priority = 0)
    {
        _hostApplicationBuilder.Services.RegisterPrismService<TService>(priority);

        return this;
    }

    public SquidPrismServerBuilder RegisterPrismService(Type serviceType, int priority = 0)
    {
        _hostApplicationBuilder.Services.RegisterPrismService(serviceType, priority);

        return this;
    }

    public SquidPrismServerBuilder RegisterPrismService(
        Type serviceInterface, Type serviceImplementation, int priority = 0
    )
    {
        _hostApplicationBuilder.Services.RegisterPrismService(serviceInterface, serviceImplementation, priority);

        return this;
    }

    public SquidPrismServerBuilder ConfigureServices(Func<IServiceCollection, IServiceCollection> configure)
    {
        configure(_hostApplicationBuilder.Services);
        return this;
    }

    public SquidPrismServerBuilder LoadContainerModule<TModule>() where TModule : IContainerModule, new()
    {
        _hostApplicationBuilder.Services.LoadContainerModule<TModule>();
        return this;
    }

    public SquidPrismServerBuilder LoadContainerModule(Type moduleType)
    {
        _hostApplicationBuilder.Services.LoadContainerModule(moduleType);
        return this;
    }

    private static void PrintBanner()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        var version = fvi.FileVersion;

        foreach (var line in File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Assets", "banner.txt")))
        {
            Log.Logger.Information(line.Replace("{version}", version));
        }
    }
}
