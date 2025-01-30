using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Interfaces.Base;

public interface INetworkMessage
{
    int MessageType { get; }
}
