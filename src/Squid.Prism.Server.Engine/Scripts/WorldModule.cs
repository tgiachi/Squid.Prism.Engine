using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Interfaces.Services.Game;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("blocks")]
public class WorldModule
{
    private readonly IBlockService _blockService;

    public WorldModule(IBlockService blockService)
    {
        _blockService = blockService;
    }

    [ScriptFunction("add_block")]
    public void AddBlockData(byte blockId, string Name, int TextureId, bool IsSolid, bool IsTransparent, bool IsLiquid)
    {
        _blockService.AddBlockData(blockId, Name, TextureId, IsSolid, IsTransparent, IsLiquid);
    }
}
