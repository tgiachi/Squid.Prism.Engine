using NLua;
using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Interfaces.Services;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("player")]
public class PlayerServiceModule
{
    private readonly IScriptEngineService _scriptEngineService;

    public PlayerServiceModule(IScriptEngineService scriptEngineService)
    {
        _scriptEngineService = scriptEngineService;
    }

    [ScriptFunction("set_motd", "Set the message of the day")]
    public void SetMotd(string motd)
    {
        _scriptEngineService.AddContextVariable("motd", new string[] { motd });
    }

    [ScriptFunction("get_motd", "Get the message of the day")]
    public string[] GetMotd()
    {
        return _scriptEngineService.GetContextVariable<string[]>("motd");
    }
}
