using Microsoft.Extensions.Logging;
using Squid.Prism.Server.Core.Attributes.Scripts;

namespace Squid.Prism.Server.Scripts;

[ScriptModule("logger")]
public class LoggerModule
{
    private readonly ILogger _logger;

    public LoggerModule(ILogger<LoggerModule> logger)
    {
        _logger = logger;
    }


    [ScriptFunction("info")]
    public void LogInfo(string message)
    {
        _logger.LogInformation(message);
    }

    [ScriptFunction("debug")]
    public void LogDebug(string message)
    {
        _logger.LogDebug(message);
    }

    [ScriptFunction("warn")]
    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }

    [ScriptFunction("error")]
    public void LogError(string message)
    {
        _logger.LogError(message);
    }
}
