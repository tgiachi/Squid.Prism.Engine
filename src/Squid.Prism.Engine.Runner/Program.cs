using Microsoft.Extensions.Hosting;
using Serilog;
using Squid.Prism.Server.Engine.Builder;

namespace Squid.Prism.Engine.Runner;

class Program
{
    static async Task Main(string[] args)
    {
        await SquidPrismServerBuilder.Create(args)
            .ConfigureLogger(
                configuration =>
                    configuration.MinimumLevel.Debug().WriteTo.Console()
            )
            .Build()
            .RunAsync();
    }
}
