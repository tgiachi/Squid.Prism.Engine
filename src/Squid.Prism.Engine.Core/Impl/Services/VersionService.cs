using Squid.Prism.Engine.Core.Data.Events.Variables;
using Squid.Prism.Engine.Core.Interfaces.Services;

namespace Squid.Prism.Engine.Core.Impl.Services;

public class VersionService : IVersionService
{
    public VersionService(IEventBusService eventBusService)
    {
        eventBusService.Publish(new AddVariableEvent("app_version", GetVersion()));
    }

    public string GetVersion()
    {
        var assembly = typeof(VersionService).Assembly;
        var version = assembly.GetName().Version;

        return version.ToString();
    }
}
