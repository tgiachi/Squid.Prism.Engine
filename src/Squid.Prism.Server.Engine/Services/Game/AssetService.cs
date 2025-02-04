using Microsoft.Extensions.Logging;
using Squid.Prism.Server.Core.Data.Services;
using Squid.Prism.Server.Core.Interfaces.Services.Game;
using Squid.Prism.Server.Core.Types;
using Squid.Prism.Server.Engine.Data.Directories;

namespace Squid.Prism.Server.Engine.Services.Game;

public class AssetService : IAssetService
{
    private readonly ILogger _logger;

    private readonly DirectoriesConfig _directoriesConfig;

    private readonly List<AssetTypeData> _assetTypes = new();


    public AssetService(ILogger<AssetService> logger, DirectoriesConfig directoriesConfig)
    {
        _logger = logger;
        _directoriesConfig = directoriesConfig;
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public void AddAsset(string name, string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"File not found: {fileName}");
        }

        var extension = Path.GetExtension(fileName);

        if (extension is ".png" or ".jpg" or ".jpeg")
        {
            _logger.LogInformation("Adding texture: {fileName}", fileName);
            _assetTypes.Add(new AssetTypeData(name, fileName, AssetType.Texture));
        }
        else if (extension == ".ttf")
        {
            _logger.LogInformation("Adding font: {fileName}", fileName);
            _assetTypes.Add(new AssetTypeData(name, fileName, AssetType.Font));
        }
        else
        {
            _logger.LogWarning($"Unknown asset type: {fileName}");
        }
    }

    public List<AssetTypeData> GetAssets()
    {
        return _assetTypes;
    }
}
