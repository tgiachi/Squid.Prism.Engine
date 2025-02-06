using System.Numerics;
using ProtoBuf;
using Squid.Prism.Engine.Core.Data.World;
using Squid.Prism.Engine.Core.World;
using Squid.Prism.Network.Serializable.Math;

namespace Squid.Prism.Network.Serializable.World;

[ProtoContract]
public class SerializableChunk
{
    [ProtoMember(1)] public SerializableVector3 Position { get; set; }

    [ProtoMember(2)] public List<byte> Blocks { get; set; } = new List<byte>();

    public SerializableChunk()
    {
    }

    public SerializableChunk(Vector3 position, ChunkEntity chunkEntity)
    {
        Position = position;

        foreach (var block in chunkEntity.Blocks)
        {
            Blocks.Add(block.Id);
        }
    }


}
