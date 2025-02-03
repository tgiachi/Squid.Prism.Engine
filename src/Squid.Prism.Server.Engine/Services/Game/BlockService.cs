using Microsoft.Extensions.Logging;
using Squid.Prism.Server.Core.Data.Services;
using Squid.Prism.Server.Core.Interfaces.Services.Game;

namespace Squid.Prism.Server.Engine.Services.Game;

public class BlockService : IBlockService
{
    private readonly Dictionary<byte, WorldBlockData> _blockData = new();

    private readonly ILogger _logger;

    public BlockService(ILogger<BlockService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public WorldBlockData GetBlockData(byte blockId)
    {
        return _blockData[blockId];
    }

    public void AddBlockData(byte blockId, string Name, int TextureId, bool IsSolid, bool IsTransparent, bool IsLiquid)
    {
        _logger.LogInformation("Adding block data for block {BlockId} Name: {Name}", blockId.ToString("x8"), Name);
        _blockData.Add(blockId, new WorldBlockData(blockId, Name, TextureId, IsSolid, IsTransparent, IsLiquid));
    }
}
