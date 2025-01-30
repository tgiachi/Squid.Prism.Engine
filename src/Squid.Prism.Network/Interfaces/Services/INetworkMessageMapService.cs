using Squid.Prism.Network.Interfaces.Base;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Interfaces.Services;

public interface INetworkMessageMapService
{
    void RegisterMessage<TMessage>(int messageType, string? description = null) where TMessage : INetworkMessage;

    Type GetMessageType(int messageType);

    string GetMessageDescription(int messageType);
}
