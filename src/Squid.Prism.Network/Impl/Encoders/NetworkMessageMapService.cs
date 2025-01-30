using Microsoft.Extensions.Logging;
using Squid.Prism.Network.Interfaces.Base;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Impl.Encoders;

public class NetworkMessageMapService : INetworkMessageMapService
{
    private readonly ILogger _logger;

    private readonly Dictionary<int, Type> _messageMap = new();
    private readonly Dictionary<int, string> _messageMapDescriptions = new();

    public NetworkMessageMapService(ILogger<NetworkMessageMapService> logger)
    {
        _logger = logger;
    }

    public void RegisterMessage<TMessage>(int messageType, string? description = null) where TMessage : INetworkMessage
    {
        _logger.LogDebug("Registering message type {MessageType} with {Message}", messageType, typeof(TMessage).Name);

        if (_messageMap.ContainsKey(messageType))
        {
            _logger.LogWarning("Message type {MessageType} is already registered", messageType);
            return;
        }

        description ??= typeof(TMessage).Name;

        _messageMapDescriptions.Add(messageType, description);

        _messageMap.Add(messageType, typeof(TMessage));
    }

    public Type GetMessageType(int messageType)
    {
        if (!_messageMap.TryGetValue(messageType, out var messageTypeValue))
        {
            _logger.LogError("Message type {MessageType} is not registered", messageType);
            throw new KeyNotFoundException($"Message type {messageType} is not registered");
        }

        return messageTypeValue;
    }

    public string GetMessageDescription(int messageType)
    {
        if (!_messageMapDescriptions.TryGetValue(messageType, out var messageDescription))
        {
            _logger.LogError("Message type {MessageType} is not registered", messageType);
            throw new KeyNotFoundException($"Message type {messageType} is not registered");
        }

        return messageDescription;
    }
}
