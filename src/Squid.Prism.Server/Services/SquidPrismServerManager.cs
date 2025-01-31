using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Squid.Prism.Server.Services;

public class SquidPrismServerManager : IHostedService
{
    private readonly ILogger _logger;

    public SquidPrismServerManager(ILogger<SquidPrismServerManager> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
    }
}
