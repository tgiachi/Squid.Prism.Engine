using System.Diagnostics;
using Humanizer;
using Microsoft.Extensions.Logging;
using ProtoBuf;
using Squid.Prism.Network.Impl.Base;
using Squid.Prism.Network.Interfaces.Base;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Types;
using Squid.Prism.Network.Utils;

namespace Squid.Prism.Network.Impl.Encoders;

public class DefaultMessageEncoderDecoder : INetworkMessageEncoderDecoder
{
    private readonly INetworkMessageMapService _messageMapService;
    private readonly CompressionAlgorithmType _compressionAlgorithmType;
    private readonly ILogger _logger;

    public DefaultMessageEncoderDecoder(
        ILogger<DefaultMessageEncoderDecoder> logger, INetworkMessageMapService messageMapService,
        CompressionAlgorithmType compressionAlgorithmType
    )
    {
        _logger = logger;
        _compressionAlgorithmType = compressionAlgorithmType;
        _messageMapService = messageMapService;
    }


    public async Task<INetworkPacket> EncodeAsync(INetworkMessage message)
    {
        var startTime = Stopwatch.GetTimestamp();

        using var temporaryMemoryStream = new MemoryStream();

        Serializer.Serialize(temporaryMemoryStream, message);

        var beforeCompression = temporaryMemoryStream.ToArray().Length;

        byte[] data = temporaryMemoryStream.ToArray();

        if (_compressionAlgorithmType != CompressionAlgorithmType.None)
        {
            data = await CompressionUtils.Compress(temporaryMemoryStream.ToArray(), _compressionAlgorithmType);

            var afterCompression = data.Length;

            _logger.LogDebug(
                "Encoding packet with message type {MessageType} - Before Compression: {BeforeCompression} After Compression: {AfterCompression} in {ElapsedMilliseconds}ms",
                message.MessageType,
                beforeCompression.Bytes(),
                afterCompression.Bytes(),
                Stopwatch.GetElapsedTime(startTime)
            );
        }


        return new NetworkPacket()
        {
            MessageType = message.MessageType,
            Options = NetworkPacketOptionType.Compressed,
            Payload = data
        };
    }

    public async Task<INetworkMessage> DecodeAsync(INetworkPacket packet)
    {
        var startTime = Stopwatch.GetTimestamp();
        var messageType = _messageMapService.GetMessageType(packet.MessageType);
        var beforeDecompression = packet.Payload.Length;

        byte[] data = packet.Payload;

        if (_compressionAlgorithmType != CompressionAlgorithmType.None)
        {
            data = await CompressionUtils.Decompress(packet.Payload, _compressionAlgorithmType);
        }

        var afterDecompression = data.Length;

        _logger.LogDebug(
            "Decoding packet with message type {MessageType} - Before Decompression: {BeforeDecompression} After Decompression: {AfterDecompression} in {ElapsedMilliseconds}ms",
            packet.MessageType,
            beforeDecompression.Bytes(),
            afterDecompression.Bytes(),
            Stopwatch.GetElapsedTime(startTime)
        );

        var message = Serializer.Deserialize(messageType, new MemoryStream(data));

        return (INetworkMessage)message;
    }
}
