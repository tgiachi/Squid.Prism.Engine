using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Interfaces.Services.Base;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Packets;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services.Game;

namespace Squid.Prism.Server.Engine.Services.Handlers;

public class AssetHandler : ISquidPrismGameService
{
    private readonly ILogger _logger;
    private readonly INetworkSessionService _networkSessionService;
    private readonly INetworkServer _networkServer;
    private readonly IAssetService _assetService;

    private readonly long _dataContentSize = 30 * 1024;

    public AssetHandler(
        ILogger<AssetHandler> logger, INetworkSessionService networkSessionService, INetworkServer networkServer,
        IAssetService assetService
    )
    {
        _logger = logger;
        _networkSessionService = networkSessionService;
        _networkServer = networkServer;
        _assetService = assetService;

        _networkServer.RegisterMessageListener<AssetRequestMessage>(OnAssetRequest);
        _networkServer.RegisterMessageListener<AssetListRequestMessage>(OnAssetListRequest);
    }

    private async ValueTask OnAssetListRequest(string sessionId, AssetListRequestMessage message)
    {
        if (_networkSessionService.IsLoggedIn(sessionId))
        {
            var assets = _assetService.GetAssets()
                .Select(
                    s => (s.Name, (byte)s.AssetType)
                )
                .ToList();

            var responseMessage = new AssetListResponseMessage(assets);

            await _networkServer.SendMessageAsync(sessionId, responseMessage);
        }
    }

    private async ValueTask OnAssetRequest(string sessionId, AssetRequestMessage message)
    {
        if (_networkSessionService.IsLoggedIn(sessionId))
        {
            var asset = _assetService.GetAssetContent(message.Name, _dataContentSize, 0);
            var totalSize = _assetService.GetAssetSize(message.Name);

            foreach (var index in Enumerable.Range(1, asset.totalParts - 1))
            {
                var part = _assetService.GetAssetContent(message.Name, _dataContentSize, index);

                asset.data = asset.data.Concat(part.data).ToArray();

                await _networkServer.SendMessageAsync(
                    sessionId,
                    new AssetResponseMessage(message.Name, asset.data, totalSize, index, asset.totalParts)
                );
            }
        }
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
