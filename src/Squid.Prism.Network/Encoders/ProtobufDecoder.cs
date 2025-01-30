using System.IO.Compression;
using Humanizer;
using Microsoft.Extensions.Logging;
using ProtoBuf;
using Squid.Prism.Network.Data.Configs;
using Squid.Prism.Network.Interfaces.Encoders;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Interfaces.Packets;
using Squid.Prism.Network.Types;
using Squid.Prism.Network.Utils;

namespace Squid.Prism.Network.Encoders;

public class ProtobufDecoder : INetworkMessageDecoder
{
    private readonly EncoderDecoderSettings _settings;
    private readonly ILogger _logger;

    public ProtobufDecoder(ILogger<ProtobufDecoder> logger, EncoderDecoderSettings settings)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<INetworkMessage> DecodeAsync(INetworkPacket packet, Type type)
    {
        if (packet.PacketType.HasFlag(NetworkPacketType.Compressed))
        {
            var beforeDecompress = packet.Payload.Length;
            var decompressed = await CompressionUtils.Decompress(packet.Payload, _settings.CompressionAlgorithm);
            using var tmpStream = new MemoryStream(decompressed);

            var message = Serializer.Deserialize(type, tmpStream) as INetworkMessage;

            _logger.LogDebug(
                "Parsed message type: {MessageType} - Decompressed {BeforeDecompress} bytes to {AfterDecompress} bytes",
                packet.MessageType,
                beforeDecompress.Bytes(),
                decompressed.Length.Bytes()
            );

            return message;
        }

        _logger.LogDebug("Parsed message type: {MessageType}", packet.MessageType);
        return Serializer.Deserialize(type, new MemoryStream(packet.Payload)) as INetworkMessage;
    }
}
