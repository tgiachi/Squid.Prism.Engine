using ProtoBuf;
using Squid.Prism.Server.Core.Data.World;

namespace Squid.Prism.Server.Core.Serializable.World;

[ProtoContract]
public class SerializableBlockEntity
{
    [ProtoMember(1)] public byte Id { get; set; }

    public SerializableBlockEntity()
    {
    }

    public SerializableBlockEntity(byte id)
    {
        Id = id;
    }

    public static implicit operator BlockEntity(SerializableBlockEntity serializableBlockEntity)
    {
        return new BlockEntity(serializableBlockEntity.Id);
    }

    public static implicit operator SerializableBlockEntity(BlockEntity blockEntity)
    {
        return new SerializableBlockEntity(blockEntity.Id);
    }
}
