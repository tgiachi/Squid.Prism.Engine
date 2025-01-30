using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Squid.Prism.Network.Data;
using Squid.Prism.Network.Interfaces.Services;

namespace Squid.Prism.Network.Impl;

public class NetworkDispatcherService : INetworkDispatcherService
{
    private readonly ILogger _logger;

    public Channel<NetworkMessageData> IncomingMessages { get; }

    public Channel<NetworkMessageData> OutgoingMessages { get; }

    private readonly CancellationTokenSource _cts;

    public NetworkDispatcherService(ILogger<NetworkDispatcherService> logger)
    {
        _cts = new CancellationTokenSource();
        var channelSettings = new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = false,
            SingleWriter = false
        };

        IncomingMessages = Channel.CreateBounded<NetworkMessageData>(channelSettings);
        OutgoingMessages = Channel.CreateBounded<NetworkMessageData>(channelSettings);

        _logger = logger;
    }


    public ValueTask EnqueuePacketToSendAsync(NetworkMessageData packet, CancellationToken cancellation = default)
    {
        _logger.LogDebug("EnqueuePacketToSendAsync: {Packet}", packet);
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellation, _cts.Token);
        return OutgoingMessages.Writer.WriteAsync(packet, linkedCts.Token);
    }

    public ValueTask EnqueueMessageToReceiveAsync(NetworkMessageData messageData, CancellationToken cancellation = default)
    {
        _logger.LogDebug("EnqueueMessageToReceiveAsync: {MessageData}", messageData);
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellation, _cts.Token);
        return IncomingMessages.Writer.WriteAsync(messageData, linkedCts.Token);
    }

    public async ValueTask DisposeAsync()
    {
        await _cts.CancelAsync();
        IncomingMessages.Writer.Complete();
        OutgoingMessages.Writer.Complete();
        _cts.Dispose();
    }
}
