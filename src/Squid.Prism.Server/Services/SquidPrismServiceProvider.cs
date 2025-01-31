using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Data.Directories;

namespace Squid.Prism.Server.Services;

public class SquidPrismServiceProvider : ISquidPrismServiceProvider
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly DirectoriesConfig _directoriesConfig;

    public SquidPrismServiceProvider(
        ILogger<SquidPrismServiceProvider> logger, IServiceProvider serviceProvider, DirectoriesConfig directoriesConfig
    )
    {
        _serviceProvider = serviceProvider;
        _directoriesConfig = directoriesConfig;
        _logger = logger;
    }

    public TService GetService<TService>()
    {
        var service = _serviceProvider.GetRequiredService<TService>();

        if (service == null)
        {
            throw new InvalidOperationException($"Service of type {typeof(TService).Name} not found.");
        }

        return service;
    }
}
