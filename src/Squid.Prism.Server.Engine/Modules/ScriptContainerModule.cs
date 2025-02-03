using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Engine.Core.Interfaces.Modules;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Engine.Scripts;

namespace Squid.Prism.Server.Engine.Modules;

public class ScriptContainerModule : IContainerModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services
            .RegisterPrismService<ContextVariableModule>()
            .RegisterPrismService<LoggerModule>()
            .RegisterPrismService<ScriptModule>()
            .RegisterPrismService<EventsModule>()
            .RegisterPrismService<FileModule>()
            .RegisterPrismService<UsersModule>()
            .RegisterPrismService<WorldModule>()
            .RegisterPrismService<NoiseModule>()
            .RegisterPrismService<SchedulerJobModule>()
            .RegisterPrismService<PlayerServiceModule>()
            .RegisterPrismService<MathModule>()
            .RegisterPrismService<VariableServiceModule>();
        ;
    }
}
