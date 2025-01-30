using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Data.Internal;

public class MessageTypeObject
{
    public int MessageType { get; set; }

    public Type Type { get; set; }

    public MessageTypeObject(int messageType, Type type)
    {
        MessageType = messageType;
        Type = type;
    }


}
