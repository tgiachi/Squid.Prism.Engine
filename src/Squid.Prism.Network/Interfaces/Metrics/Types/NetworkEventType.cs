namespace Squid.Prism.Network.Interfaces.Metrics.Types;

public enum NetworkEventType
{
    Info,
    Warning,
    Error,
    ConnectionStateChanged,
    LatencySpike,
    PacketLoss,
    SecurityEvent
}
