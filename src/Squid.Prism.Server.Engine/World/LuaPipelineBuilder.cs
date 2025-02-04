using NLua;
using Squid.Prism.Server.Core.Data.World.Build;
using Squid.Prism.Server.Core.Interfaces.World.Builder;

namespace Squid.Prism.Server.Engine.World;

public class LuaPipelineBuilder : IChunkBuilderPipe
{
    private readonly LuaFunction _function;

    public LuaPipelineBuilder(LuaFunction function)
    {
        _function = function;
    }

    public async Task<ChunkBuilderContext> ProcessAsync(ChunkBuilderContext context)
    {
        _function.Call(context);
        return context;
    }
}
