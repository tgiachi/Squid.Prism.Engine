using Squid.Prism.Engine.Core.Interfaces.Services.Base;
using Squid.Prism.Server.Core.Data.Services;

namespace Squid.Prism.Server.Core.Interfaces.Services.Game;

public interface IAssetService : ISquidPrismGameService
{
    void AddAsset(string name, string fileName);

    List<AssetTypeData> GetAssets();
    (byte[] data, int totalParts) GetAssetContent(string name, long size, int currentPart);

    long GetAssetSize(string name);

    int GetMaxParts(string name, int size);
}
