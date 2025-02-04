using Squid.Prism.Server.Core.Data.World.Build;

namespace Squid.Prism.Server.Core.Interfaces.World.Builder;

public interface IChunkBuilderPipe
{
    Task<ChunkBuilderContext> ProcessAsync(ChunkBuilderContext context);
}
