using Microsoft.Extensions.Hosting;
using Serilog;
using Squid.Prism.Network.Data.Configs;
using Squid.Prism.Network.Types;
using Squid.Prism.Server.Engine.Builder;

namespace Squid.Prism.Engine.Runner;

public class Program
{
    public static async Task Main(string[] args)
    {
        await SquidPrismServerBuilder
            .Create(args)
            .ConfigureLogger(configuration => configuration.MinimumLevel.Debug().WriteTo.Console())
            .SetNetworkEncoderDecoderSettings(
                new EncoderDecoderSettings() { CompressionAlgorithm = CompressionAlgorithmType.GZip }
            )
            .Build()
            .RunAsync();
    }
}
