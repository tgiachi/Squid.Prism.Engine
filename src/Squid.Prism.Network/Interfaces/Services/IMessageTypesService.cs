using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Interfaces.Services;

public interface IMessageTypesService
{
    Type GetMessageType(int messageType);
    int GetMessageType(Type type);

    int GetMessageType<T>() where T : class;

    void RegisterMessageType(int messageType, Type type);

    void RegisterMessage<T>(int messageType) where T : class;
    void RegisterMessage<T>() where T : INetworkMessage;
}
