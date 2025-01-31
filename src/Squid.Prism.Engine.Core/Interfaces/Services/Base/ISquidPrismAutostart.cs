namespace Squid.Prism.Engine.Core.Interfaces.Services.Base;

/// <summary>
///   Represents a service that can be started and stopped at startup.
/// </summary>
public interface ISquidPrismAutostart : ISquidPrismService
{
    Task StartAsync(CancellationToken cancellationToken = default);

    Task StopAsync(CancellationToken cancellationToken = default);
}
