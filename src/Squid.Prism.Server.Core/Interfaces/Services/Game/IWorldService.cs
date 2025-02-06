using System.Numerics;
using Squid.Prism.Engine.Core.Interfaces.Services.Base;
using Squid.Prism.Engine.Core.World;
using Squid.Prism.Server.Core.Data.World;
using Squid.Prism.Server.Core.Interfaces.World.Builder;

namespace Squid.Prism.Server.Core.Interfaces.Services.Game;

public interface IWorldService : ISquidPrismGameService
{
    void AddChunkBuilderPipe(IChunkBuilderPipe pipe);

    Task<ChunkEntity> GetChunkAsync(Vector3 position);

    void AddBiome(byte id, BiomeEntity biome);

    BiomeEntity GetBiome(byte id);
}
