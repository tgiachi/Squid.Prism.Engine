using System.Numerics;
using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Serializable.Math;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class PlayerMoveRequestMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.PlayerMoveRequest;

    [ProtoMember(1)]
    public SerializableVector3 Position { get; set; }

    [ProtoMember(2)]
    public SerializableVector3 Rotation { get; set; }

    public PlayerMoveRequestMessage()
    {
    }

    public PlayerMoveRequestMessage(SerializableVector3 position, SerializableVector3 rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public PlayerMoveRequestMessage(Vector3 position, Vector3 rotation)
    {
        Position = position;
        Rotation = rotation;
    }
}
