using LiteNetLib;
using LiteNetLib.Utils;
using Squid.Prism.Network.Packets.Base;

namespace Squid.Prism.Network.Interfaces.Services;

public interface IMessageParserWriterService
{
    void ReadPackets(NetDataReader reader, NetPeer peer);

    Task WriteMessageAsync(NetPeer peer, NetDataWriter writer, NetworkPacket message);
}
