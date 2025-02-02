using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Interfaces.Services.Base;
using Squid.Prism.Server.Core.Data.Services;
using Squid.Prism.Server.Core.Interfaces.Services;

namespace Squid.Prism.Server.Services;

public class SquidPrismServerManager : IHostedService
{
    private readonly ILogger _logger;

    private readonly ISquidPrismServiceProvider _squidPrismServiceProvider;

    public SquidPrismServerManager(
        ILogger<SquidPrismServerManager> logger, ISquidPrismServiceProvider squidPrismServiceProvider
    )
    {
        _logger = logger;
        _squidPrismServiceProvider = squidPrismServiceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await LoadServicesAsync();

        
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
    }

    private async Task LoadServicesAsync()
    {
        var servicesData = _squidPrismServiceProvider.GetService<List<ServiceDefinitionData>>();

        foreach (var serviceType in servicesData.OrderBy(s => s.Priority))
        {
            _logger.LogInformation("Loading service {ServiceType}.", serviceType.ServiceType.Name);
            var service = _squidPrismServiceProvider.GetService(serviceType.ImplementationType);

            if (service is ISquidPrismAutostart autostartService)
            {
                _logger.LogInformation("Starting service {ServiceType}.", serviceType.ServiceType.Name);

                await autostartService.StartAsync();

                _logger.LogInformation("Service {ServiceType} started.", serviceType.ServiceType.Name);
            }
        }
    }
}
