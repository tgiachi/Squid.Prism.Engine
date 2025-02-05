using System.Reactive.Linq;
using System.Reactive.Subjects;
using LiteNetLib;
using LiteNetLib.Utils;
using Microsoft.Extensions.Logging;
using Squid.Prism.Network.Client.Data.Configs;
using Squid.Prism.Network.Client.Interfaces;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Packets.Base;

namespace Squid.Prism.Network.Client.Impl;

public class NetworkClient : INetworkClient
{
    public event INetworkClient.MessageReceivedEventHandler? MessageReceived;
    public event EventHandler? Connected;
    public bool IsConnected { get; private set; }

    private readonly ILogger _logger;
    private readonly NetworkClientConfig _config;

    private readonly EventBasedNetListener _clientListener = new();

    private readonly Subject<INetworkMessage> _messageSubject = new();

    private readonly SemaphoreSlim _writeLock = new(1, 1);

    private readonly NetManager _netManager;

    private bool _connected;

    private readonly INetworkMessageFactory _networkMessageFactory;

    private readonly NetPacketProcessor _netPacketProcessor = new();

    private readonly NetDataWriter writer = new();

    public NetworkClient(
        ILogger<NetworkClient> logger,
        NetworkClientConfig config, INetworkMessageFactory networkMessageFactory
    )
    {
        _logger = logger;
        _config = config;
        _networkMessageFactory = networkMessageFactory;

        _netManager = new NetManager(_clientListener);


        _netPacketProcessor.SubscribeReusable<NetworkPacket, NetPeer>(OnReceivePacket);
        _clientListener.NetworkReceiveEvent += OnMessageReceived;
    }

    private async void OnReceivePacket(NetworkPacket packet, NetPeer peer)
    {
        _logger.LogDebug("Received packet from server type: {Type}", packet.MessageType);

        if (!_connected)
        {
            _connected = true;
            IsConnected = true;
            Connected?.Invoke(this, EventArgs.Empty);
        }

        var message = await _networkMessageFactory.ParseAsync(packet);

        _logger.LogDebug("Parsed message from server type: {Type}", message.GetType().Name);

        MessageReceived?.Invoke(packet.MessageType, message);

        _messageSubject.OnNext(message);
    }

    private void OnMessageReceived(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        _netPacketProcessor.ReadAllPackets(reader, peer);
    }


    public void PoolEvents()
    {
        if (_connected)
        {
            _netManager.PollEvents();
        }
    }

    public void Connect()
    {
        _netManager.Start();
        _netManager.Connect(_config.Host, _config.Port, "");
    }

    public async Task SendMessageAsync<T>(T message) where T : class, INetworkMessage
    {
        if (!IsConnected)
        {
            _logger.LogWarning("Dropping message {messageType} as client is not connected", message.GetType().Name);
            return;
        }

        await _writeLock.WaitAsync();

        var packet = (NetworkPacket)(await _networkMessageFactory.SerializeAsync(message));

        _logger.LogDebug(">> Sending message {messageType}", message.GetType().Name);

        writer.Reset();

        _netPacketProcessor.Write(writer, packet);


        foreach (var peer in _netManager.ConnectedPeerList)
        {
            peer.Send(writer, DeliveryMethod.ReliableOrdered);
        }

        _writeLock.Release();
    }

    public IObservable<T> SubscribeToMessage<T>() where T : class, INetworkMessage
    {
        return _messageSubject.OfType<T>();
    }
}
