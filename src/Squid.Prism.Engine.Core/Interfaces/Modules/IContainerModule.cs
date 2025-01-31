using Microsoft.Extensions.DependencyInjection;

namespace Squid.Prism.Engine.Core.Interfaces.Modules;

public interface IContainerModule
{
    IServiceCollection RegisterModule(IServiceCollection services);
}
