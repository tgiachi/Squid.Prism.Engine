namespace Squid.Prism.Engine.Core.Interfaces.Services.Base;

public interface ISquidPrismGameService
{
    Task StartAsync(CancellationToken cancellationToken = default);

    Task StopAsync(CancellationToken cancellationToken = default);
}
