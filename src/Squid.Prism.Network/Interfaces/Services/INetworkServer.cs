using Squid.Prism.Engine.Core.Interfaces.Services.Base;
using Squid.Prism.Network.Data.Internal;
using Squid.Prism.Network.Interfaces.Listeners;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Interfaces.Metrics.Server;

namespace Squid.Prism.Network.Interfaces.Services;

public interface INetworkServer : IDisposable, ISquidPrismAutostart
{
    bool IsRunning { get; }

    void RegisterMessageListener<TMessage>(INetworkMessageListener<TMessage> listener)
        where TMessage : class, INetworkMessage;

    void RegisterMessageListener<TMessage>(Func<string, TMessage, ValueTask> listener)
        where TMessage : class, INetworkMessage;

    ValueTask BroadcastMessageAsync(INetworkMessage message);
    ValueTask SendMessagesAsync(IEnumerable<SessionNetworkMessage> messages);
    ValueTask SendMessageAsync(SessionNetworkMessage messages);
    ValueTask SendMessageAsync(string sessionId, INetworkMessage message);
    IObservable<NetworkMetricsSnapshot> MetricsObservable { get; }
}
