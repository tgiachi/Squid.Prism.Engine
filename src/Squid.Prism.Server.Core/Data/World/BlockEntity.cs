using Squid.Prism.Server.Core.Data.Services;

namespace Squid.Prism.Server.Core.Data.World;

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
