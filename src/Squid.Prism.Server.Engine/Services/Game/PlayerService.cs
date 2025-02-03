using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Configs;
using Squid.Prism.Server.Core.Interfaces.Services;

namespace Squid.Prism.Server.Engine.Services.Game;

public class PlayerService : IPlayerService
{
    private readonly ILogger _logger;

    [ConfigVariable("motd")] public string[] Motd { get; set; }

    public PlayerService(ILogger<PlayerService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (Motd.Length == 0)
        {
            _logger.LogWarning("MOTD is empty");
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
