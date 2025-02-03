using Microsoft.Extensions.Hosting;
using Serilog;
using Squid.Prism.Server.Engine.Builder;


namespace Squid.Prism.Server.Engine;

class Program
{
    public static async Task Main(string[] args)
    {
        await SquidPrismServerBuilder.Create(args)
            .ConfigureLogger(
                configuration =>
                    configuration.MinimumLevel.Debug().WriteTo.Console()
            )
            .Build()
            .RunAsync();
        // var rootDirectory = Environment.GetEnvironmentVariable("SQUID_PRISM_ROOT_DIRECTORY") ??
        //                     Path.Combine(Environment.CurrentDirectory, "sprism");
        //
        // var directoryConfig = new DirectoriesConfig(rootDirectory);
        //
        // Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
        //     .WriteTo.Console()
        //     .CreateLogger();
        //
        // var builder = Host.CreateApplicationBuilder(args);
        //
        // var runtimeData = new RuntimeData
        // {
        //     IsDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true",
        //     RootDirectory = rootDirectory,
        //     ProcessCount = Environment.ProcessorCount,
        //     ProcessId = Environment.ProcessId
        // };
        //
        //
        // builder.Services.AddSingleton(runtimeData);
        //
        // builder.Services.AddSingleton(directoryConfig);
        //
        // builder.Services.AddSingleton<ISquidPrismServiceProvider, SquidPrismServiceProvider>();
        //
        // builder.Services
        //     .LoadContainerModule<ServerServicesModule>()
        //     .LoadContainerModule<CoreServiceModule>();
        //
        //
        // builder.Services.AddHostedService<SquidPrismServerManager>();
        //
        // builder.Logging.ClearProviders().AddSerilog();
        //
        // Log.Logger.Information(runtimeData.ToString());
        //
        // var app = builder.Build();
        //
        //
        // PrintBanner();
        //
        //
        // await app.RunAsync();
    }
}
