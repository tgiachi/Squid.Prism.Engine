using Microsoft.Extensions.Logging;
using Squid.Prism.Network.Data.Internal;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Interfaces.Services;

namespace Squid.Prism.Network.Services;

public class MessageTypesService : IMessageTypesService
{
    private readonly ILogger _logger;

    private readonly Dictionary<int, Type> _messageTypes = new(new Dictionary<int, Type>());

    private readonly Dictionary<Type, int> _messageTypesReverse =
        new(new Dictionary<Type, int>());

    public MessageTypesService(ILogger<MessageTypesService> logger, List<MessageTypeObject>? messageTypes = null)
    {
        _logger = logger;
        if (messageTypes != null)
        {
            foreach (var messageType in messageTypes)
            {
                RegisterMessageType(messageType.MessageType, messageType.Type);
            }
        }
    }


    public Type GetMessageType(int messageType)
    {
        if (!_messageTypes.TryGetValue(messageType, out var type))
        {
            _logger.LogError("Message type {messageType} is not registered", messageType);
            throw new ArgumentException("Message type is not registered", nameof(messageType));
        }

        return type;
    }

    public int GetMessageType(Type type)
    {
        if (!_messageTypesReverse.TryGetValue(type, out var messageType))
        {
            _logger.LogError("Type {type} is not registered", type.Name);
            throw new ArgumentException("Type is not registered", nameof(type));
        }

        return messageType;
    }

    public int GetMessageType<T>() where T : class
    {
        var messageType = _messageTypes.First(x => x.Value == typeof(T)).Key;

        return messageType;
    }

    public void RegisterMessageType(int messageType, Type type)
    {
        if (!typeof(INetworkMessage).IsAssignableFrom(type))
        {
            _logger.LogError("Type {type} does not implement INetworkMessage", type.Name);
            throw new ArgumentException("Type does not implement INetworkMessage", nameof(type));
        }

        if (_messageTypes.ContainsKey(messageType))
        {
            _logger.LogError("Message type {messageType} is already registered", messageType);
            throw new ArgumentException("Message type is already registered", nameof(messageType));
        }


        _logger.LogDebug("Registered message type {messageType} with type {type}", messageType, type.Name);

        _messageTypes.Add(messageType, type);
        _messageTypesReverse.Add(type, messageType);
    }

    public void RegisterMessage<T>(int messageType) where T : class
    {
        RegisterMessageType(messageType, typeof(T));
    }

    public void RegisterMessage<T>() where T : INetworkMessage
    {
        var instance = Activator.CreateInstance<T>();

        RegisterMessageType(instance.MessageType, typeof(T));

        instance = default;
    }
}
