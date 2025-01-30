using Squid.Prism.Network.Interfaces.Messages;

namespace Squid.Prism.Network.Data.Events.Network;

public class RegisterNetworkListenerEvent<TMessage> where TMessage : class, INetworkMessage
{
    public Func<string, TMessage, ValueTask> Listener { get; }

    public RegisterNetworkListenerEvent(Func<string, TMessage, ValueTask> listener)
    {
        Listener = listener;
    }
}
