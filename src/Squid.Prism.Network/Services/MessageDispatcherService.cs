using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Squid.Prism.Network.Interfaces.Listeners;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Types;
using ListenerResult =
    System.Func<string, Squid.Prism.Network.Interfaces.Messages.INetworkMessage, System.Threading.Tasks.ValueTask>;

namespace Squid.Prism.Network.Services;

public class MessageDispatcherService : IMessageDispatcherService
{
    private readonly ILogger _logger;
    private readonly INetworkMessageFactory _networkMessageFactory;


    private readonly IMessageTypesService _messageTypesService;
    private readonly IMessageChannelService _messageChannelService;

    private readonly Task _dispatchIncomingMessagesTask;

    private readonly CancellationTokenSource _incomingMessagesCancellationTokenSource = new();

    private readonly ConcurrentDictionary<int, List<ListenerResult>> _handlers = new();

    public MessageDispatcherService(
        IMessageTypesService messageTypesService, INetworkMessageFactory networkMessageFactory,
        IMessageChannelService messageChannelService, ILogger<MessageDispatcherService> logger
    )
    {
        _messageTypesService = messageTypesService;

        _networkMessageFactory = networkMessageFactory;
        _messageChannelService = messageChannelService;
        _logger = logger;

        _dispatchIncomingMessagesTask = DispatchIncomingMessages();
    }

    private async Task DispatchIncomingMessages()
    {
        while (!_incomingMessagesCancellationTokenSource.Token.IsCancellationRequested)
        {
            await foreach (var message in _messageChannelService.IncomingReaderChannel.ReadAllAsync(
                               _incomingMessagesCancellationTokenSource.Token
                           ))
            {
                var parsedMessage = await _networkMessageFactory.ParseAsync(message.Packet);

                DispatchMessage(message.SessionId, parsedMessage);
            }
        }
    }

    public void RegisterMessageListener<TMessage>(INetworkMessageListener<TMessage> listener)
        where TMessage : class, INetworkMessage
    {
        RegisterMessageListener<TMessage>(
            async (sessionId, message) => await listener.OnMessageReceivedAsync(sessionId, message)
        );
    }


    public void RegisterMessageListener<TMessage>(
        Func<string, TMessage, ValueTask> listener
    ) where TMessage : class, INetworkMessage
    {
        var messageType = _messageTypesService.GetMessageType(typeof(TMessage));

        if (!_handlers.TryGetValue(messageType, out var handlers))
        {
            handlers = new List<ListenerResult>();
            _handlers.TryAdd(messageType, handlers);
        }

        handlers.Add(
            async (sessionId, message) =>
            {
                if (message is TMessage typedMessage)
                {
                    await listener.Invoke(sessionId, typedMessage);
                }
            }
        );
    }

    public async void DispatchMessage<TMessage>(string sessionId, TMessage message) where TMessage : class, INetworkMessage
    {
        var messageType = _messageTypesService.GetMessageType(message.GetType());

        if (!_handlers.TryGetValue(messageType, out var handlers))
        {
            _logger.LogWarning("No handlers registered for message type {messageType}", messageType);
            return;
        }

        foreach (var handler in handlers)
        {
            await handler.Invoke(sessionId, message);
        }
    }

    public void Dispose()
    {
        _incomingMessagesCancellationTokenSource.Dispose();
        GC.SuppressFinalize(this);
    }
}
