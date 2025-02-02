using NLua;
using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Interfaces.Services;

namespace Squid.Prism.Server.Scripts;

[ScriptModule("main")]
public class ScriptModule
{
    private readonly IScriptEngineService _scriptEngineSystemService;

    public ScriptModule(IScriptEngineService scriptEngineSystemService)
    {
        _scriptEngineSystemService = scriptEngineSystemService;
    }


    [ScriptFunction("on_bootstrap", "Called when the server is bootstrapping")]
    public void RegisterBootstrap(LuaFunction function)
    {
        _scriptEngineSystemService.AddContextVariable("bootstrap", function);
    }

    [ScriptFunction("gen_lua_def", "Generate lua definitions")]
    public string GenerateLuaDefinitions()
    {
        return _scriptEngineSystemService.GenerateDefinitionsAsync().Result;
    }
}
