using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NLua;
using NLua.Exceptions;
using Serilog;
using Squid.Prism.Engine.Core.Extensions;
using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Data.Scripts;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Utils.Script;
using Squid.Prism.Server.Data.Directories;
using Squid.Prism.Server.Types;

namespace Squid.Prism.Server.Services;

public class ScriptEngineService : IScriptEngineService
{
    private readonly ILogger _logger = Log.ForContext<ScriptEngineService>();

    private readonly IServiceProvider _serviceProvider;


    private readonly Lua _luaEngine;

    private readonly LuaTypeDefinitionsGenerator _typeGenerator = new();
    private readonly List<ScriptClassData> _scriptModules;
    private readonly DirectoriesConfig _directoryConfig;
    private const string _fileExtension = "*.lua";

    private FileSystemWatcher _watcher;

    private readonly Subject<string> _fileChanges = new();

    private const string _prefixToIgnore = "__";

    public List<ScriptFunctionDescriptor> Functions { get; } = new();
    public Dictionary<string, object> ContextVariables { get; } = new();


    private IDisposable _fileWatcherSubscription;

    public ScriptEngineService(
        DirectoriesConfig directoryConfig,  IServiceProvider serviceProvider,
        List<ScriptClassData> scriptClassData
    )
    {
        _scriptModules = scriptClassData;
        _directoryConfig = directoryConfig;
        _serviceProvider = serviceProvider;

        _luaEngine = new Lua();

        _luaEngine.LoadCLRPackage();

        AddModulesDirectory();

        // eventBusService.SubscribeAsync<AddScriptClassEvent>(
        //     @event =>
        //     {
        //         _scriptModules.Add(new ScriptClassData(@event.ScriptClassType));
        //         return Task.CompletedTask;
        //     }
        // );
    }


    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await ScanScriptModulesAsync();
        var scriptsToLoad = Directory.GetFiles(
            _directoryConfig[DirectoryType.Scripts],
            _fileExtension,
            SearchOption.AllDirectories
        );

        foreach (var script in scriptsToLoad)
        {
            var fileName = Path.GetFileName(script);

            if (!fileName.StartsWith(_prefixToIgnore))
            {
                await ExecuteFileAsync(script);
            }
        }

        _watcher = new FileSystemWatcher(_directoryConfig[DirectoryType.Scripts])
        {
            Filter = "*.lua",
            IncludeSubdirectories = true,
            EnableRaisingEvents = true
        };

        _watcher.Changed += OnScriptFileChanged;
        _watcher.Created += OnScriptFileChanged;

        _logger.Debug("Enabled file watcher for scripts directory: {Directory}", _directoryConfig[DirectoryType.Scripts]);

        _fileWatcherSubscription = _fileChanges
            .Throttle(TimeSpan.FromSeconds(1))
            .Subscribe(
                async file =>
                {
                    try
                    {
                        await ExecuteFileAsync(file);
                        _logger.Debug("Reloaded script: {File}", Path.GetFileName(file));
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Error reloading script: {File}", Path.GetFileName(file));
                    }
                }
            );
    }

    private void OnScriptFileChanged(object sender, FileSystemEventArgs e)
    {
        if (!e.Name.StartsWith(_prefixToIgnore))
        {
            _fileChanges.OnNext(e.FullPath);
        }
    }

    public void AddScriptModule(Type type)
    {
        try
        {
            var obj = _serviceProvider.GetRequiredService(type);

            foreach (var scriptMethod in type.GetMethods())
            {
                var sMethodAttr = scriptMethod.GetCustomAttribute<ScriptFunctionAttribute>();

                if (sMethodAttr == null)
                {
                    continue;
                }

                ExtractFunctionDescriptor(sMethodAttr, scriptMethod);

                _logger.Debug("Adding script method {M}", sMethodAttr.Alias ?? scriptMethod.Name);

                _luaEngine[sMethodAttr.Alias ?? scriptMethod.Name] = CreateLuaEngineDelegate(obj, scriptMethod);
            }
        }
        catch (Exception ex)
        {
            _logger.Error("Error during initialize script module {Alias}: {Ex}", type, ex);
        }
    }

    public Task ScanScriptModulesAsync()
    {
        foreach (var module in _scriptModules)
        {
            _logger.Debug("Found script module {Module}", module.ClassType.Name);
            AddScriptModule(module.ClassType);
        }

        return Task.CompletedTask;
    }

    public async Task ExecuteFileAsync(string file)
    {
        _logger.Information("Executing script: {File}", Path.GetFileName(file));
        try
        {
            var script = await File.ReadAllTextAsync(file);
            _luaEngine.DoString(script);
        }
        catch (LuaException ex)
        {
            _logger.Error(ex, "Error executing script: {File}: {Formatted}", Path.GetFileName(file), FormatException(ex));
        }
    }

    private void ExtractFunctionDescriptor(ScriptFunctionAttribute attribute, MethodInfo methodInfo)
    {
        var descriptor = new ScriptFunctionDescriptor
        {
            FunctionName = attribute.Alias ?? methodInfo.Name,
            Help = attribute.Help,
            Parameters = new(),
            ReturnType = methodInfo.ReturnType.Name,
            RawReturnType = methodInfo.ReturnType
        };

        foreach (var parameter in methodInfo.GetParameters())
        {
            descriptor.Parameters.Add(
                new ScriptFunctionParameterDescriptor(
                    parameter.Name,
                    parameter.ParameterType.Name,
                    parameter.ParameterType
                )
            );
        }

        Functions.Add(descriptor);
    }

    public ScriptEngineExecutionResult ExecuteCommand(string command)
    {
        try
        {
            var result = new ScriptEngineExecutionResult
            {
                Result = _luaEngine.DoString(command)
            };

            return result;
        }
        catch (LuaException ex)
        {
            return new ScriptEngineExecutionResult { Exception = ex };
        }
    }

    public void AddContextVariable(string name, object value)
    {
        _logger.Information("Adding context variable {Name} with value {Value}", name, value);
        _luaEngine[name] = value;
        ContextVariables[name] = value;
    }

    public TVar? GetContextVariable<TVar>(string name, bool throwIfNotFound = true) where TVar : class
    {
        return GetContextVariable(name, typeof(TVar), throwIfNotFound) as TVar;
    }

    public object? GetContextVariable(string name, Type type, bool throwIfNotFound = true)
    {
        if (!ContextVariables.TryGetValue(name, out var ctxVar))
        {
            if (throwIfNotFound)
            {
                _logger.Error("Variable {Name} not found", name);
                throw new KeyNotFoundException($"Variable {name} not found");
            }

            return default;
        }


        if (ctxVar is LuaFunction luaFunction)
        {
            return (object)luaFunction;
        }

        if (ctxVar is LuaTable luaTable)
        {
            var json = ScriptUtils.LuaTableToDictionary(luaTable).ToJson();
            return json.FromJson(type);
        }

        return ctxVar;
    }

    public bool ExecuteContextVariable(string name, params object[] args)
    {
        if (ContextVariables.TryGetValue(name, out var ctxVar) && ctxVar is LuaFunction luaFunction)
        {
            luaFunction.Call(args);
            return true;
        }

        _logger.Error("Variable {Name} not found", name);
        return false;
    }

    public Task<bool> BootstrapAsync()
    {
        if (ExecuteContextVariable("bootstrap"))
        {
            return Task.FromResult(true);
        }

        _logger.Error(
            "Bootstrap function not found, you should define a function callback 'on_bootstrap' in your scripts"
        );

        return Task.FromResult(false);
    }

    private static Delegate CreateLuaEngineDelegate(object obj, MethodInfo method)
    {
        var parameterTypes =
            method.GetParameters().Select(p => p.ParameterType).Concat(new[] { method.ReturnType }).ToArray();
        return method.CreateDelegate(Expression.GetDelegateType(parameterTypes), obj);
    }

    public async Task<string> GenerateDefinitionsAsync()
    {
        return await _typeGenerator.GenerateTypeDefinitionsAsync(Functions, ContextVariables);
    }


    private void AddModulesDirectory()
    {
        var modulesPath = Path.Combine(_directoryConfig[DirectoryType.Scripts]) + Path.DirectorySeparatorChar;
        var scriptModulePath = Path.Combine(_directoryConfig[DirectoryType.ScriptModules]) + Path.DirectorySeparatorChar;


        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            modulesPath = modulesPath.Replace(@"\", @"\\");
            scriptModulePath = scriptModulePath.Replace(@"\", @"\\");
        }

        _luaEngine.DoString(
            $"""
             -- Update the search path
             local module_folder = '{modulesPath}'
             local module_script_folder = '{scriptModulePath}'
             package.path = module_folder .. '?.lua;' .. package.path
             package.path = module_script_folder .. '?.lua;' .. package.path
             """
        );
    }

    private static string FormatException(LuaException e)
    {
        var source = (string.IsNullOrEmpty(e.Source)) ? "<no source>" : e.Source[..^2];
        return string.Format("{0}\nLua (at {2})", e.Message, "", source);
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _luaEngine.Dispose();
        _fileWatcherSubscription?.Dispose();
        _watcher?.Dispose();

        GC.SuppressFinalize(this);
    }
}
