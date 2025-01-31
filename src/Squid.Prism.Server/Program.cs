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


        builder.Services.AddSingleton(
            new RuntimeData()
            {
                IsDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true",
                RootDirectory = rootDirectory,
                ProcessCount = Environment.ProcessorCount,
                ProcessId = Environment.ProcessId
            }
        );

        builder.Services.AddSingleton(directoryConfig);


        builder.Logging.ClearProviders().AddSerilog();


        var app = builder.Build();


        await app.RunAsync();
    }
}
