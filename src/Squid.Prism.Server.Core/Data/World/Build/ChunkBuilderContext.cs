using System.Numerics;

namespace Squid.Prism.Server.Core.Data.World.Build;

public class ChunkBuilderContext
{
    private readonly Dictionary<string, object> _data = new();
    public int Seed { get; set; }
    public Vector3 Position { get; }

    public ChunkEntity Chunk { get; set; }

    public ChunkBuilderContext(Vector3 position)
    {
        Position = position;
        Chunk = new ChunkEntity(position);
    }


    public void AddData(string key, object value)
    {
        _data[key] = value;
    }

    public object GetData(string key)
    {
        return _data[key];
    }


    public Vector3 GetLocalCoordinates(int worldX, int worldY, int worldZ)
    {
        var localX = worldX & (ChunkEntity.Size - 1);
        var localZ = worldZ & (ChunkEntity.Size - 1);
        var localY = worldY & (ChunkEntity.Size - 1);
        return new Vector3(localX, localY, localZ);
    }
}
