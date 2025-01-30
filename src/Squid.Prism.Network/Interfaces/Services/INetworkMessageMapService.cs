using Squid.Prism.Network.Interfaces.Base;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Interfaces.Services;

public interface INetworkMessageMapService
{
    void RegisterMessage<TMessage>(int messageType) where TMessage : INetworkMessage;

    Type GetMessageType(int messageType);
}
