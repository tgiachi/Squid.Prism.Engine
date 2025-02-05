using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Serializable.Math;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class ChunkRequestMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.ChunkRequest;

    [ProtoMember(1)] public SerializableVector3 ChunkPosition { get; set; }
}
