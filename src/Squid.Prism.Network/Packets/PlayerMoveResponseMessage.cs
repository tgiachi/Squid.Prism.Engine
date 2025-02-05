using System.Numerics;
using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Serializable.Math;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

public class PlayerMoveResponseMessage : INetworkMessage
{
    [ProtoMember(1)] public string SessionId { get; set; }

    [ProtoMember(2)] public SerializableVector3 Position { get; set; }

    [ProtoMember(3)] public SerializableVector3 Rotation { get; set; }

    public int RequestType => DefaultMessageTypeConst.PlayerMoveResponse;

    public PlayerMoveResponseMessage()
    {
    }

    public PlayerMoveResponseMessage(string sessionId, SerializableVector3 position, SerializableVector3 rotation)
    {
        SessionId = sessionId;
        Position = position;
        Rotation = rotation;
    }

    public PlayerMoveResponseMessage(string sessionId, Vector3 position, Vector3 rotation)
    {
        SessionId = sessionId;
        Position = position;
        Rotation = rotation;
    }

}
