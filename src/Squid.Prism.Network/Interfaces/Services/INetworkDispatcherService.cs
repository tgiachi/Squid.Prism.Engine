using System.Threading.Channels;
using Squid.Prism.Network.Data;


namespace Squid.Prism.Network.Interfaces.Services;

public interface INetworkDispatcherService : IAsyncDisposable
{
    Channel<NetworkMessageData> IncomingMessages { get; }

    Channel<NetworkMessageData> OutgoingMessages { get; }

    ValueTask EnqueuePacketToSendAsync(NetworkMessageData packet, CancellationToken cancellation = default);

    ValueTask EnqueueMessageToReceiveAsync(NetworkMessageData messageData,  CancellationToken cancellation = default);




}
