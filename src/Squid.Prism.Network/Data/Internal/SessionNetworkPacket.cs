using Squid.Prism.Network.Interfaces.Packets;

namespace Squid.Prism.Network.Data.Internal;

public class SessionNetworkPacket
{
    public string SessionId { get; set; }

    public INetworkPacket Packet { get; set; }

    public SessionNetworkPacket(string sessionId, INetworkPacket packet)
    {
        SessionId = sessionId;
        Packet = packet;
    }

    public SessionNetworkPacket()
    {
    }

    public override string ToString() => $"SessionId: {SessionId}, Packet: {Packet.MessageType}";
}
