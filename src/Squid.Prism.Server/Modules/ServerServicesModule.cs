using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Engine.Core.Interfaces.Modules;
using Squid.Prism.Server.Core.Data.Scripts;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Services;

namespace Squid.Prism.Server.Modules;

public class ServerServicesModule : IContainerModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services.RegisterPrismService<IScriptEngineService, ScriptEngineService>()
            .AddSingleton(new List<ScriptClassData>());
    }
}
