using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class VersionRequestMessage : INetworkMessage
{
    public int MessageRequestType => DefaultMessageTypeConst.VersionMessageRequest;
}
