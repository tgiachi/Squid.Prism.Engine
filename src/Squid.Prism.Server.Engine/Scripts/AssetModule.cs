using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Interfaces.Services.Game;
using Squid.Prism.Server.Engine.Data.Directories;
using Squid.Prism.Server.Engine.Types;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("assets")]
public class AssetModule
{
    private readonly DirectoriesConfig _directoriesConfig;

    private readonly IAssetService _assetService;

    public AssetModule(DirectoriesConfig directoriesConfig, IAssetService assetService)
    {
        _directoriesConfig = directoriesConfig;
        _assetService = assetService;
    }

    [ScriptFunction("add_file")]
    public void AddAsset(string name, string fileName)
    {
        _assetService.AddAsset(name, Path.Combine(_directoriesConfig[DirectoryType.Assets], fileName));
    }
}
