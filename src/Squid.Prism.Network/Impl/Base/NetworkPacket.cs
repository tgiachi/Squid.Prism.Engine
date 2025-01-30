using Squid.Prism.Network.Interfaces.Base;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Impl.Base;

public struct NetworkPacket : INetworkPacket
{
    public int MessageType { get; set; }

    public NetworkPacketOptionType Options { get; set; }

    public byte[] Payload { get; set; }

    public NetworkPacket(int messageType, NetworkPacketOptionType options, byte[] payload)
    {
        MessageType = messageType;
        Options = options;
        Payload = payload;
    }

    public NetworkPacket()
    {

    }

}
