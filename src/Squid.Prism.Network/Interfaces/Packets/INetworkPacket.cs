using LiteNetLib.Utils;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Interfaces.Packets;

public interface INetworkPacket : INetSerializable
{
    NetworkPacketType PacketType { get; set; }
    byte[] Payload { get; set; }
    int MessageType { get; set; }
}
