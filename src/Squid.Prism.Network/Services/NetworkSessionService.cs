using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Interfaces.Sessions;

namespace Squid.Prism.Network.Services;

public class NetworkSessionService : INetworkSessionService
{
    public int SessionCount => _sessions.Count;

    private readonly ConcurrentDictionary<string, ISessionObject> _sessions = new();
    private readonly ILogger _logger;


    public NetworkSessionService(ILogger<NetworkSessionService> logger)
    {
        _logger = logger;
    }

    public List<string> GetSessionIds => _sessions.Keys.ToList();

    public ISessionObject? GetSessionObject(string sessionId)
    {
        return _sessions.GetValueOrDefault(sessionId);
    }

    public void AddSession(string sessionId, ISessionObject sessionObject)
    {
        if (_sessions.ContainsKey(sessionId))
        {
            _logger.LogWarning("Session {sessionId} already exists", sessionId);

            _sessions.TryRemove(sessionId, out _);
        }

        _logger.LogDebug("Adding session {sessionId}", sessionId);
        var isAdded = _sessions.TryAdd(sessionId, sessionObject);

        if (isAdded)
        {
            //_eventBusService.Publish(new SessionAddedEvent(sessionId));
        }
    }

    public void RemoveSession(string sessionId)
    {
        _logger.LogDebug("Removing session {sessionId}", sessionId);
        _sessions.TryRemove(sessionId, out _);

        //_eventBusService.Publish(new SessionRemovedEvent(sessionId));
    }

    public void UpdateLastActive(string sessionId)
    {
        if (_sessions.TryGetValue(sessionId, out var session))
        {
            session.LastActive = DateTime.UtcNow;
        }
    }

    public IEnumerable<ISessionObject> GetExpiredSessions(TimeSpan expirationTime)
    {
        return _sessions
            .Where(x => x.Value.LastActive + expirationTime < DateTime.UtcNow)
            .Select(x => x.Value)
            .ToList();
    }
}
