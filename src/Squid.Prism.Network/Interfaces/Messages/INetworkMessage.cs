using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Interfaces.Messages;

public interface INetworkMessage
{
    int RequestType { get; }
}
