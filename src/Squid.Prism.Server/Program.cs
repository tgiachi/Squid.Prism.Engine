using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Squid.Prism.Server;

class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        var builder = Host.CreateApplicationBuilder(args);

        builder.Logging.ClearProviders().AddSerilog();


        var app = builder.Build();


        await app.RunAsync();
    }
}
