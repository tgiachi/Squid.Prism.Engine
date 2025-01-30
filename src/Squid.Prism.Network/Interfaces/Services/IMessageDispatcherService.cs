using Squid.Prism.Network.Interfaces.Listeners;
using Squid.Prism.Network.Interfaces.Messages;

namespace Squid.Prism.Network.Interfaces.Services;

public interface IMessageDispatcherService : IDisposable
{
    void RegisterMessageListener<TMessage>(INetworkMessageListener<TMessage> listener)
        where TMessage : class, INetworkMessage;

    void RegisterMessageListener<TMessage>(Func<string, TMessage, ValueTask> listener)
        where TMessage : class, INetworkMessage;

    void DispatchMessage<TMessage>(string sessionId, TMessage message) where TMessage : class, INetworkMessage;
}
