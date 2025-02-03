using System.Numerics;
using ProtoBuf;

namespace Squid.Prism.Server.Core.Serializable.Math;

[ProtoContract]
public class SerializableVector2
{
    [ProtoMember(1)] public float X { get; set; }

    [ProtoMember(2)] public float Y { get; set; }

    public SerializableVector2()
    {
    }

    public SerializableVector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static implicit operator SerializableVector2(Vector2 vector2)
    {
        return new SerializableVector2(vector2.X, vector2.Y);
    }

    public static implicit operator Vector2(SerializableVector2 serializableVector2)
    {
        return new Vector2(serializableVector2.X, serializableVector2.Y);
    }
}
