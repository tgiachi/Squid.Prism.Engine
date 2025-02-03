using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Configs;
using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Network.Data.Events.Clients;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Packets;
using Squid.Prism.Server.Core.Interfaces.Services;

namespace Squid.Prism.Server.Engine.Services.Game;

public class PlayerService : IPlayerService
{
    private readonly ILogger _logger;

    private readonly INetworkServer _networkServer;

    private readonly IVersionService _versionService;

    private readonly IVariablesService _variablesService;

    [ConfigVariable("motd")] public string[] Motd { get; set; }

    public PlayerService(
        ILogger<PlayerService> logger, INetworkServer networkServer, IEventBusService eventBusService,
        IVariablesService variablesService, IVersionService versionService
    )
    {
        _logger = logger;
        _networkServer = networkServer;
        _variablesService = variablesService;
        _versionService = versionService;

        eventBusService.SubscribeAsync<ClientConnectedEvent>(OnClientConnected);
        eventBusService.SubscribeAsync<ClientDisconnectedEvent>(OnClientDisconnected);

        _networkServer.RegisterMessageListener<VersionRequestMessage>((s, _) => SendVersionAsync(s));
        _networkServer.RegisterMessageListener<MotdRequestMessage>((s, _) => SendMotdAsync(s));
    }

    private Task OnClientDisconnected(ClientDisconnectedEvent obj)
    {
        return Task.CompletedTask;
    }

    private async Task OnClientConnected(ClientConnectedEvent obj)
    {
        await SendVersionAsync(obj.SessionId);
        await SendMotdAsync(obj.SessionId);
    }

    private async ValueTask SendMotdAsync(string sessionId)
    {
        var parsedMotd = Motd.Select(s => _variablesService.TranslateText(s));
        await _networkServer.SendMessageAsync(sessionId, new MotdResponseMessage(parsedMotd.ToArray()));
    }

    private async ValueTask SendVersionAsync(string sessionId)
    {
        await _networkServer.SendMessageAsync(sessionId, new VersionResponseMessage(_versionService.GetVersion()));
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
