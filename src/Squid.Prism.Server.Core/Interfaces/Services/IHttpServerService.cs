using System.Text.RegularExpressions;
using Squid.Prism.Engine.Core.Interfaces.Services.Base;
using Squid.Prism.Server.Core.Types;
using WatsonWebserver.Core;

namespace Squid.Prism.Server.Core.Interfaces.Services;

public interface IHttpServerService : ISquidPrismAutostart
{
    // Content routes
    void AddContentRoute(string path, bool listFiles);

    // Static routes
    void AddStaticRoute(RouteMethodType method, string path, Func<HttpContextBase, Task> handler);

    // Parameter routes
    void AddParameterRoute(RouteMethodType method, string path, Func<HttpContextBase, Task> handler);

    // Dynamic routes
    void AddDynamicRoute(RouteMethodType method, Regex pattern, Func<HttpContextBase, Task> handler);
}
