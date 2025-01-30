using System.Threading.Channels;
using Squid.Prism.Network.Data.Internal;

namespace Squid.Prism.Network.Interfaces.Services;

public interface IMessageChannelService : IDisposable
{
    ChannelReader<SessionNetworkPacket> IncomingReaderChannel { get; }
    ChannelWriter<SessionNetworkPacket> IncomingWriterChannel { get; }
    ChannelReader<SessionNetworkMessage> OutgoingReaderChannel { get; }
    ChannelWriter<SessionNetworkMessage> OutgoingWriterChannel { get; }



}
