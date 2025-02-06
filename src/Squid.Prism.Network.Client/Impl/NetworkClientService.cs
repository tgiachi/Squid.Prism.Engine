using System.ComponentModel;
using System.Numerics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.World;
using Squid.Prism.Network.Client.Interfaces;
using Squid.Prism.Network.Packets;
using Squid.Prism.Server.Core.Data.World;

namespace Squid.Prism.Network.Client.Impl;

public class NetworkClientService : INetworkClientService
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }

    public Subject<Vector3> PositionSubject { get; } = new();
    public Subject<ChunkEntity> ChunkSubject { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ILogger _logger;
    private readonly INetworkClient _networkClient;

    public NetworkClientService(ILogger<NetworkClientService> logger)
    {
        _networkClient = new NetworkClient(null, null, null);


        _logger = logger;
        PropertyChanged += OnPropertyChanged;

        PositionSubject.Sample(TimeSpan.FromMilliseconds(50)).Subscribe(OnPositionChanged);
        //_networkClient.SubscribeToMessage<WorldC>()
    }

    private async void OnPositionChanged(Vector3 obj)
    {
        _logger.LogDebug("Position changed to {Position}", obj);

        if (!_networkClient.IsConnected)
        {
            return;
        }

        await _networkClient.SendMessageAsync(new PlayerMoveRequestMessage(Position, Rotation));
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Position))
        {
            PositionSubject.OnNext(Position);
        }
    }
}
