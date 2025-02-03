using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Attributes.Scripts;


namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("vars")]
public class VariableServiceModule
{
    private readonly IVariablesService _variablesService;

    public VariableServiceModule(IVariablesService variablesService)
    {
        _variablesService = variablesService;
    }


    [ScriptFunction("r_text")]
    public string ReplaceText(string text)
    {
        return _variablesService.TranslateText(text);
    }
}
