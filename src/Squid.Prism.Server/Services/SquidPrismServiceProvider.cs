using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Configs;
using Squid.Prism.Engine.Core.Extensions;
using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Data.Directories;
using Squid.Prism.Server.Types;

namespace Squid.Prism.Server.Services;

public class SquidPrismServiceProvider : ISquidPrismServiceProvider
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly DirectoriesConfig _directoriesConfig;
    private readonly IVariablesService _variablesService;

    private readonly IScriptEngineService _scriptEngineService;

    public SquidPrismServiceProvider(
        ILogger<SquidPrismServiceProvider> logger, IServiceProvider serviceProvider, DirectoriesConfig directoriesConfig,
        IScriptEngineService scriptEngineService, IVariablesService variablesService
    )
    {
        _serviceProvider = serviceProvider;
        _directoriesConfig = directoriesConfig;
        _scriptEngineService = scriptEngineService;
        _variablesService = variablesService;
        _logger = logger;
    }

    public TService GetService<TService>()
    {
        return (TService)GetService(typeof(TService));
    }

    public object? GetService(Type serviceType)
    {
        var service = _serviceProvider.GetService(serviceType);

        if (service == null)
        {
            throw new InvalidOperationException($"Service of type {serviceType.Name} not found.");
        }

        GetConfigAttribute(serviceType, service);

        return service;
    }

    private void GetConfigAttribute(Type type, object instance)
    {
        foreach (var prop in instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            var configAttribute = prop.GetCustomAttribute<ConfigVariableAttribute>();

            if (configAttribute != null)
            {
                var name = configAttribute.Name ?? prop.PropertyType.Name.ToSnakeCase();
                _logger.LogDebug("Config variable {Name} found.", name);

                var value = _scriptEngineService.GetContextVariable(name, prop.PropertyType, false);

                if (value == null)
                {
                    var configFile = Path.Combine(_directoriesConfig[DirectoryType.Configs], $"{name}.json");

                    if (!File.Exists(configFile))
                    {
                        _logger.LogDebug("Config file not {File} found, creating default", configFile);
                        var defaultJson = Activator.CreateInstance(prop.PropertyType);
                        File.WriteAllText(configFile, defaultJson.ToJson());
                    }


                    var json = _variablesService.TranslateText(File.ReadAllText(configFile));

                    value = json.FromJson(prop.PropertyType);
                }

                prop.SetValue(instance, value);
            }
        }
    }
}
