using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Squid.Prism.Network.Interfaces.Encoders;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Interfaces.Packets;
using Squid.Prism.Network.Interfaces.Services;

namespace Squid.Prism.Network.Services;

public class NetworkMessageFactory : INetworkMessageFactory
{
    private readonly ILogger _logger;

    private readonly IMessageTypesService _messageTypesService;

    private readonly INetworkMessageDecoder _decoder;

    private readonly INetworkMessageEncoder _encoder;

    public NetworkMessageFactory(
        IMessageTypesService messageTypesService, INetworkMessageDecoder decoder, INetworkMessageEncoder encoder,
        ILogger<NetworkMessageFactory> logger
    )
    {
        _messageTypesService = messageTypesService;
        _decoder = decoder;
        _encoder = encoder;
        _logger = logger;
    }


    public async Task<INetworkPacket> SerializeAsync<T>(T message) where T : class
    {
        if (_encoder == null)
        {
            _logger.LogError("No encoder registered");
            throw new InvalidOperationException("No message encoder registered");
        }

        var startTime = Stopwatch.GetTimestamp();


        var encodedNetworkPacket = await _encoder.EncodeAsync(message, _messageTypesService.GetMessageType(message.GetType()));


        _logger.LogDebug(
            "Encoding message of type {messageType} took {time}ms",
            message.GetType().Name,
            Stopwatch.GetElapsedTime(startTime)
        );

        return encodedNetworkPacket;
    }

    public async Task<INetworkMessage> ParseAsync(INetworkPacket packet)
    {
        if (_decoder == null)
        {
            _logger.LogDebug("No decoder registered");
            throw new InvalidOperationException("No message decoder registered");
        }

        var startTime = Stopwatch.GetTimestamp();

        var message = await _decoder.DecodeAsync(packet, _messageTypesService.GetMessageType(packet.MessageType));

        var endTime = Stopwatch.GetTimestamp();

        _logger.LogDebug(
            "Decoding message of type {messageType} took {time}ms",
            message.GetType().Name,
            Stopwatch.GetElapsedTime(startTime)
        );

        return message;
    }
}
