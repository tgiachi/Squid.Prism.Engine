using Humanizer;
using LiteNetLib.Utils;
using Squid.Prism.Network.Interfaces.Packets;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets.Base;

public class NetworkPacket : INetworkPacket
{
    public NetworkPacketType PacketType { get; set; }
    public byte[] Payload { get; set; }
    public int MessageType { get; set; }

    public void Serialize(NetDataWriter writer)
    {
        writer.Put((byte)PacketType);
        writer.PutBytesWithLength(Payload);
        writer.Put(MessageType);
    }

    public void Deserialize(NetDataReader reader)
    {
        PacketType = (NetworkPacketType)reader.GetByte();
        Payload = reader.GetBytesWithLength();
        MessageType = reader.GetInt();
    }


    public override string ToString() =>
        $"PacketType: {PacketType}, MessageType: {MessageType}, Payload: {Payload.Length.Bytes()}";
}
