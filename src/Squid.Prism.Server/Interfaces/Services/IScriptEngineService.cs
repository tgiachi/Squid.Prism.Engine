using Squid.Prism.Server.Data.Scripts;

namespace Squid.Prism.Server.Interfaces.Services;

public interface IScriptEngineService : IDisposable
{
    Task ExecuteFileAsync(string file);

    ScriptEngineExecutionResult ExecuteCommand(string command);

    List<ScriptFunctionDescriptor> Functions { get; }

    Dictionary<string, object> ContextVariables { get; }

    Task<string> GenerateDefinitionsAsync();

    void AddContextVariable(string name, object value);

    TVar? GetContextVariable<TVar>(string name, bool throwIfNotFound = true) where TVar : class;

    object? GetContextVariable(string name, Type type, bool throwIfNotFound = true);

    bool ExecuteContextVariable(string name, params object[] args);

    Task<bool> BootstrapAsync();

    void AddScriptModule(Type type);

    Task ScanScriptModulesAsync();
}
