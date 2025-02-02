using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Data.Events.Variables;
using Squid.Prism.Engine.Core.Interfaces.Listeners;
using Squid.Prism.Engine.Core.Interfaces.Services;

namespace Squid.Prism.Engine.Core.Impl.Services;

public partial class VariablesService
    : IVariablesService, IEventBusListener<AddVariableEvent>, IEventBusListener<AddVariableBuilderEvent>
{
    [GeneratedRegex(@"\$\{([^}]+)\}")]
    private static partial Regex MyRegex();

    private static Regex TokenRegex => MyRegex();

    private readonly ILogger _logger;
    private readonly Dictionary<string, Func<object>> _variableBuilder = new();
    private readonly Dictionary<string, object> _variables = new();

    public VariablesService(ILogger<VariablesService> logger, IEventBusService eventBusService)
    {
        _logger = logger;
        eventBusService.Subscribe<AddVariableEvent>(this);
        eventBusService.Subscribe<AddVariableBuilderEvent>(this);


        AddDefaultVariables();

    }

    private void AddDefaultVariables()
    {
        AddVariable("cpu_count", Environment.ProcessorCount);

    }

    public void AddVariableBuilder(string variableName, Func<object> builder)
    {
        _logger.LogDebug("Adding variable builder for {variableName}", variableName);
        _variableBuilder[variableName] = builder;
    }

    public void AddVariable(string variableName, object value)
    {
        _logger.LogDebug("Adding variable {variableName} with value {value}", variableName, value);
        _variables[variableName] = value;
    }

    public string TranslateText(string text)
    {
        var matches = TokenRegex.Matches(text);
        var result = new StringBuilder(text);

        foreach (Match match in matches)
        {
            string token = match.Groups[1].Value;
            string replacement = null;

            if (_variables.TryGetValue(token, out var variable))
            {
                replacement = variable.ToString();
            }
            else if (_variableBuilder.TryGetValue(token, out var value))
            {
                replacement = value().ToString();
            }

            if (replacement != null)
            {
                result.Replace(match.Value, replacement);
            }
        }

        return result.ToString();
    }

    public List<string> GetVariables()
    {
        var list = new List<string>();
        list.AddRange(_variables.Keys);
        list.AddRange(_variableBuilder.Keys);

        list = list.OrderByDescending(x => x).ToList();

        return list;
    }

    public void RebuildVariables()
    {
        foreach (var builder in _variableBuilder.AsParallel())
        {
            try
            {
                _variables[builder.Key] = builder.Value();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error building variable {VariableName}", builder.Key);
            }
        }
    }


    public Task OnEventAsync(AddVariableEvent message)
    {
        AddVariable(message.VariableName, message.Value);

        return Task.CompletedTask;
    }

    public Task OnEventAsync(AddVariableBuilderEvent message)
    {
        AddVariableBuilder(message.VariableName, message.Builder);

        return Task.CompletedTask;
    }
}
