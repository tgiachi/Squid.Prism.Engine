using System.Threading.Channels;
using Squid.Prism.Network.Data.Internal;
using Squid.Prism.Network.Interfaces.Services;

namespace Squid.Prism.Network.Services;

public class MessageChannelService : IMessageChannelService
{
    private readonly Channel<SessionNetworkPacket> _incomingChannel;
    private readonly Channel<SessionNetworkMessage> _outgoingChannel;


    public ChannelReader<SessionNetworkPacket> IncomingReaderChannel => _incomingChannel;
    public ChannelWriter<SessionNetworkPacket> IncomingWriterChannel => _incomingChannel;
    public ChannelReader<SessionNetworkMessage> OutgoingReaderChannel => _outgoingChannel;
    public ChannelWriter<SessionNetworkMessage> OutgoingWriterChannel => _outgoingChannel;

    public MessageChannelService()
    {
        var channelsOption = new BoundedChannelOptions(1024)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = false,
            SingleWriter = false
        };
        _incomingChannel = Channel.CreateBounded<SessionNetworkPacket>(channelsOption);
        _outgoingChannel = Channel.CreateBounded<SessionNetworkMessage>(channelsOption);
    }

    public void Dispose()
    {
        _incomingChannel.Writer.Complete();
        _outgoingChannel.Writer.Complete();
        GC.SuppressFinalize(this);
    }
}
