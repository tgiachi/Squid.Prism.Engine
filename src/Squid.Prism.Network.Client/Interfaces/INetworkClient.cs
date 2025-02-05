using Squid.Prism.Network.Interfaces.Messages;

namespace Squid.Prism.Network.Client.Interfaces;

public interface INetworkClient
{
    delegate void MessageReceivedEventHandler(int messageType, INetworkMessage message);

    event MessageReceivedEventHandler MessageReceived;

    event EventHandler Connected;

    bool IsConnected { get; }

    void PoolEvents();

    void Connect();

    Task SendMessageAsync<T>(T message) where T : class, INetworkMessage;

    public IObservable<T> SubscribeToMessage<T>() where T : class, INetworkMessage;
}
