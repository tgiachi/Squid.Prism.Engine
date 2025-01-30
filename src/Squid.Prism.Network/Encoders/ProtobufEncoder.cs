using Humanizer;
using Microsoft.Extensions.Logging;
using ProtoBuf;
using Squid.Prism.Network.Data.Configs;
using Squid.Prism.Network.Interfaces.Encoders;
using Squid.Prism.Network.Interfaces.Packets;
using Squid.Prism.Network.Packets.Base;
using Squid.Prism.Network.Types;
using Squid.Prism.Network.Utils;

namespace Squid.Prism.Network.Encoders;

public class ProtobufEncoder : INetworkMessageEncoder
{
    private readonly EncoderDecoderSettings _settings;
    private readonly ILogger _logger;


    public ProtobufEncoder(ILogger<ProtobufEncoder> logger,EncoderDecoderSettings settings)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<INetworkPacket> EncodeAsync<TMessage>(TMessage message, int messageType) where TMessage : class
    {
        using var memoryStream = new MemoryStream();

        Serializer.Serialize(memoryStream, message);

        var beforeCompressionLength = memoryStream.Length;

        var compressed = await CompressionUtils.Compress(memoryStream.ToArray(), _settings.CompressionAlgorithm);

        var packet = new NetworkPacket
        {
            Payload = compressed,
            PacketType = NetworkPacketType.Compressed,
            MessageType = messageType
        };

        _logger.LogDebug(
            "Encoded message type: {MessageType} - Compressed {BeforeCompression} bytes to {AfterCompression} bytes",
            messageType,
            beforeCompressionLength.Bytes(),
            compressed.Length.Bytes()
        );

        return packet;
    }
}
