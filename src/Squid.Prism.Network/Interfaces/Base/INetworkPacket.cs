using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Interfaces.Base;

public interface INetworkPacket
{
    int MessageType { get; }

    NetworkPacketOptionType Options { get; set; }

    byte[] Payload { get; set; }
}
