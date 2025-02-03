using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Interfaces.Services.Game;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("world")]
public class WorldModule
{
    private readonly IWorldService _worldService;

    public WorldModule(IWorldService worldService)
    {
        _worldService = worldService;
    }

    [ScriptFunction("add_block")]
    public void AddBlockData(byte blockId, string Name, int TextureId, bool IsSolid, bool IsTransparent, bool IsLiquid)
    {
        _worldService.AddBlockData(blockId, Name, TextureId, IsSolid, IsTransparent, IsLiquid);
    }
}
