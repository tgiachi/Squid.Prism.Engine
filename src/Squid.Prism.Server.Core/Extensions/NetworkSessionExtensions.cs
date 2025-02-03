using Squid.Prism.Network.Interfaces.Services;

namespace Squid.Prism.Server.Core.Extensions;

public static class NetworkSessionExtensions
{

    public static bool IsLoggedIn(this INetworkSessionService sessionService, string sessionId)
    {
        var session = sessionService.GetSessionObject(sessionId);

        return session is { IsLoggedIn: true };

    }

}
