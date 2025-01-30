using Squid.Prism.Network.Interfaces.Base;

namespace Squid.Prism.Network.Data;

public struct MessageDestinationData
{
    public string SessionId { get; set; }

    public INetworkMessage Message { get; set; }

    public override string ToString()
    {
        return $"MessageType: {Message.MessageType:B} - SessionId: {SessionId}"
            
    }
}
