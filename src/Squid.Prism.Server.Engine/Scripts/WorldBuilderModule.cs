using NLua;
using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Data.World;
using Squid.Prism.Server.Core.Interfaces.Services.Game;
using Squid.Prism.Server.Engine.World;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("world_builder")]
public class WorldBuilderModule
{
    private readonly IWorldService _worldService;

    public WorldBuilderModule(IWorldService worldService)
    {
        _worldService = worldService;
    }

    [ScriptFunction("add_layer")]
    public void AddWorldBuilderPipeline(LuaFunction builder)
    {
        _worldService.AddChunkBuilderPipe(new LuaPipelineBuilder(builder));
    }

    [ScriptFunction("add_biome")]
    public void AddBiome(byte id, string name)
    {
        _worldService.AddBiome(id, new BiomeEntity { Id = id, Name = name });
    }
}
