using Squid.Prism.Engine.Core.Data.World;
using Squid.Prism.Engine.Core.Interfaces.Services.Base;
using Squid.Prism.Server.Core.Data.Services;

namespace Squid.Prism.Server.Core.Interfaces.Services.Game;

public interface IBlockService : ISquidPrismGameService
{
    void AddBlockData(byte blockId, string Name, int TextureId, bool IsSolid, bool IsTransparent, bool IsLiquid);

    WorldBlockData GetBlockData(byte blockId);
}
