using Squid.Prism.Engine.Core.Data.World;

namespace Squid.Prism.Engine.Core.World;

public class BlockEntity
{
    public byte Id { get; set; }

    public BlockEntity(WorldBlockData data)
    {
        Id = data.Id;
    }

    public BlockEntity(byte id)
    {
        Id = id;
    }

    public BlockEntity()
    {
    }
}
