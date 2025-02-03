using System.Reflection;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets.Base;

public abstract class BaseNetworkMessage : INetworkMessage
{
    public int MessageRequestType { get; }
}
