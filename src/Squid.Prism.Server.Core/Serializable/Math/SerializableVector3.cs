using System.Numerics;
using ProtoBuf;

namespace Squid.Prism.Server.Core.Serializable.Math;

[ProtoContract]
public class SerializableVector3
{
    [ProtoMember(1)]
    public float X { get; set; }

    [ProtoMember(2)]
    public float Y { get; set; }

    [ProtoMember(3)]
    public float Z { get; set; }

    public SerializableVector3()
    {
    }

    public SerializableVector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static implicit operator SerializableVector3(Vector3 vector3)
    {
        return new SerializableVector3(vector3.X, vector3.Y, vector3.Z);
    }

    public static implicit operator Vector3(SerializableVector3 serializableVector3)
    {
        return new Vector3(serializableVector3.X, serializableVector3.Y, serializableVector3.Z);
    }

}
