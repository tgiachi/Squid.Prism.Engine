namespace Squid.Prism.Server.Core.Interfaces.Services;

public interface ISquidPrismServiceProvider
{
    TService GetService<TService>();

}
