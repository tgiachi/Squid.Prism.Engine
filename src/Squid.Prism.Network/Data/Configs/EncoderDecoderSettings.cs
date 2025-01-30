using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Data.Configs;

public class EncoderDecoderSettings
{
    public CompressionAlgorithmType CompressionAlgorithm { get; set; } = CompressionAlgorithmType.Brotli;
}
