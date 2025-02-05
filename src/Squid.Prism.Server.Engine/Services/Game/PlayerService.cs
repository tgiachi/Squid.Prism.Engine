using System.Collections.Concurrent;
using System.Numerics;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Configs;
using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Network.Data.Events.Clients;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Packets;
using Squid.Prism.Server.Core.Data.GameObjects;
using Squid.Prism.Server.Core.Entities;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Interfaces.Services.Game;

namespace Squid.Prism.Server.Engine.Services.Game;

public class PlayerService : IPlayerService
{
    private readonly ILogger _logger;

    private readonly INetworkServer _networkServer;

    private readonly IVersionService _versionService;

    private readonly IVariablesService _variablesService;

    private readonly IDatabaseService _databaseService;

    private readonly INetworkSessionService _networkSessionService;

    [ConfigVariable("motd")] public string[] Motd { get; set; }

    private readonly ConcurrentDictionary<string, PlayerObject> _players = new();


    public PlayerService(
        ILogger<PlayerService> logger, INetworkServer networkServer, IEventBusService eventBusService,
        IVariablesService variablesService, IVersionService versionService, INetworkSessionService networkSessionService,
        IDatabaseService databaseService
    )
    {
        _logger = logger;
        _networkServer = networkServer;
        _variablesService = variablesService;
        _versionService = versionService;
        _networkSessionService = networkSessionService;
        _databaseService = databaseService;

        eventBusService.SubscribeAsync<ClientConnectedEvent>(OnClientConnected);
        eventBusService.SubscribeAsync<ClientDisconnectedEvent>(OnClientDisconnected);

        _networkServer.RegisterMessageListener<VersionRequestMessage>((s, _) => SendVersionAsync(s));
        _networkServer.RegisterMessageListener<MotdRequestMessage>((s, _) => SendMotdAsync(s));
        _networkServer.RegisterMessageListener<LoginRequestMessage>(OnLoginMessageRequest);
        _networkServer.RegisterMessageListener<PlayerMoveRequestMessage>(OnPlayerMoveRequest);
    }

    private async ValueTask OnPlayerMoveRequest(string sessionId, PlayerMoveRequestMessage message)
    {
        if (_networkSessionService.IsLoggedIn(sessionId))
        {
            _logger.LogDebug("Player {sessionId} moved to {position}", sessionId, message.Position);
            if (_players.TryGetValue(sessionId, out var playerObject))
            {
                playerObject.Position = message.Position;
                playerObject.Rotation = message.Rotation;
            }
        }
    }

    private async ValueTask OnLoginMessageRequest(string sessionId, LoginRequestMessage requestMessage)
    {
        var user = (await _databaseService.QueryAsync<UserEntity>(entity => entity.Username == requestMessage.Username))
            .FirstOrDefault();

        if (user == null)
        {
            await _networkServer.SendMessageAsync(
                sessionId,
                new LoginResponseMessage(false, "Invalid username or password")
            );
            return;
        }

        if (user.Password != requestMessage.Password)
        {
            await _networkServer.SendMessageAsync(
                sessionId,
                new LoginResponseMessage(false, "Invalid username or password")
            );
            return;
        }

        var sessionObject = _networkSessionService.GetSessionObject(sessionId);
        sessionObject.IsLoggedIn = true;

        AddPlayer(sessionId);

        await _networkServer.SendMessageAsync(sessionId, new LoginResponseMessage(true, "Login successful"));
    }

    private Task OnClientDisconnected(ClientDisconnectedEvent obj)
    {
        return Task.CompletedTask;
    }

    private void AddPlayer(string sessionId)
    {
        var playerObject = new PlayerObject();
        _logger.LogDebug("Adding player {sessionId}", sessionId);
        _players.TryAdd(sessionId, playerObject);
        playerObject.PositionSubject.Sample(TimeSpan.FromMilliseconds(50))
            .Subscribe(
                position => OnPlayerPositionChanged(sessionId, position)
            );
    }

    private async Task OnPlayerPositionChanged(string sessionId, Vector3 obj)
    {
        _logger.LogDebug("Player {sessionId} position changed to {position}", sessionId, obj);
        await NotifyPlayerPositionChanged(sessionId);
    }


    private async Task NotifyPlayerPositionChanged(string sessionId)
    {
        var sessions = _networkSessionService.GetSessionIds
            .Where(s => s != sessionId);

        foreach (var destinationSessionId in sessions)
        {
            if (_players.TryGetValue(sessionId, out var playerObject))
            {
                await _networkServer.SendMessageAsync(
                    destinationSessionId,
                    new PlayerMoveResponseMessage(sessionId, playerObject.Position, playerObject.Rotation)
                );
            }
        }
    }


    private async Task OnClientConnected(ClientConnectedEvent obj)
    {
        InitSession(obj.SessionId);
        await SendVersionAsync(obj.SessionId);
        await SendMotdAsync(obj.SessionId);
    }

    private void InitSession(string sessionId)
    {
        var sessionObject = _networkSessionService.GetSessionObject(sessionId);

        sessionObject.IsLoggedIn = false;
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
