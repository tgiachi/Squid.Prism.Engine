using Microsoft.Extensions.Logging.Testing;
using Squid.Prism.Engine.Tests.Data;
using Squid.Prism.Network.Data;
using Squid.Prism.Network.Impl;
using Squid.Prism.Network.Interfaces.Services;

namespace Squid.Prism.Engine.Tests.Network;

public class NetworkDispatcherTests : IAsyncDisposable
{
    private readonly INetworkDispatcherService _networkDispatcherService;

    public NetworkDispatcherTests()
    {
        _networkDispatcherService = new NetworkDispatcherService(new FakeLogger<NetworkDispatcherService>());
    }


    [Test]
    public async Task EnqueuePacketToSendAsyncTest()
    {
        var isReceived = false;
        // Arrange
        var packet = new NetworkMessageData("sessionId", new TestPacket());


        Task.Run(
            async () =>
            {
                var pkp = await _networkDispatcherService.OutgoingMessages.Reader.ReadAsync();
                Assert.That(pkp.Message, Is.Not.Null);
                isReceived = true;
            }
        );


        // Act
        await _networkDispatcherService.EnqueuePacketToSendAsync(packet);

        await Task.Delay(1000);

        Assert.That(isReceived, Is.True);
    }

    [Test]
    public async Task EnqueuePacketToReceiveAsyncTest()
    {
        var isReceived = false;
        // Arrange
        var packet = new NetworkMessageData("sessionId", new TestPacket());


        Task.Run(
            async () =>
            {
                var pkp = await _networkDispatcherService.IncomingMessages.Reader.ReadAsync();
                Assert.That(pkp.Message, Is.Not.Null);
                isReceived = true;
            }
        );


        // Act
        await _networkDispatcherService.EnqueueMessageToReceiveAsync(packet);

        await Task.Delay(1000);

        Assert.That(isReceived, Is.True);
    }


    public async ValueTask DisposeAsync()
    {
        await _networkDispatcherService.DisposeAsync();
    }
}
