using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Interfaces.Services;


namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("ctx")]
public class ContextVariableModule
{
    private readonly IScriptEngineService _scriptEngineService;

    public ContextVariableModule(IScriptEngineService scriptEngineService)
    {
        _scriptEngineService = scriptEngineService;
    }

    [ScriptFunction("add_var")]
    public void AddContextVariable(string variableName, object value)
    {
        _scriptEngineService.AddContextVariable(variableName, value);
    }
}
