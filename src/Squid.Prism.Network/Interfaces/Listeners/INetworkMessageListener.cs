using Squid.Prism.Network.Interfaces.Messages;

namespace Squid.Prism.Network.Interfaces.Listeners;

public interface INetworkMessageListener<in TMessage> where TMessage : class, INetworkMessage
{
    ValueTask OnMessageReceivedAsync(string sessionId, TMessage message);
}
