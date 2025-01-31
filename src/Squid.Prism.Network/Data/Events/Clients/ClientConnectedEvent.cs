using LiteNetLib;
using Squid.Prism.Engine.Core.Interfaces.Events;

namespace Squid.Prism.Network.Data.Events.Clients;

public class ClientConnectedEvent : ISquidPrismEvent
{
    public string SessionId { get; set; }
    public NetPeer Peer { get; set; }

    public ClientConnectedEvent(string sessionId, NetPeer peer)
    {
        SessionId = sessionId;
        Peer = peer;
    }

    public ClientConnectedEvent()
    {

    }

    public override string ToString()
    {
        return $"ClientConnectedEvent: {SessionId}";
    }


}


