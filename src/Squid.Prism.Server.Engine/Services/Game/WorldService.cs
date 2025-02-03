using Squid.Prism.Server.Core.Data.Services;
using Squid.Prism.Server.Core.Interfaces.Services.Game;

namespace Squid.Prism.Server.Engine.Services.Game;

public class WorldService : IWorldService
{
    private readonly Dictionary<byte, WorldBlockData> _blockData = new();


    public WorldBlockData GetBlockData(byte blockId)
    {
        return _blockData[blockId];
    }

    public void AddBlockData(byte blockId, string Name, int TextureId, bool IsSolid, bool IsTransparent, bool IsLiquid)
    {
        _blockData.Add(blockId, new WorldBlockData(blockId, Name, TextureId, IsSolid, IsTransparent, IsLiquid));
    }


    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
