using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Squid.Prism.Server.Data.Directories;
using Squid.Prism.Server.Data.Runtime;

namespace Squid.Prism.Server;

class Program
{
    public static async Task Main(string[] args)
    {
        var rootDirectory = Environment.GetEnvironmentVariable("SQUID_PRISM_ROOT_DIRECTORY") ??
                            Path.Combine(Environment.CurrentDirectory, "sprism");

        var directoryConfig = new DirectoriesConfig(rootDirectory);

        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        var builder = Host.CreateApplicationBuilder(args);

        var runtimeData = new RuntimeData
        {
            IsDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true",
            RootDirectory = rootDirectory,
            ProcessCount = Environment.ProcessorCount,
            ProcessId = Environment.ProcessId
        };


        builder.Services.AddSingleton(runtimeData);

        builder.Services.AddSingleton(directoryConfig);


        builder.Logging.ClearProviders().AddSerilog();

        Log.Logger.Information(runtimeData.ToString());

        var app = builder.Build();


        PrintBanner();


        await app.RunAsync();
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
