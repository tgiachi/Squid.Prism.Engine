using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class MotdRequestMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.MotdRequest;
}
