using System.Numerics;

namespace Squid.Prism.Server.Core.Data.World;

public class ChunkEntity
{
    public static readonly int Size = 16;
    public static readonly int Height = 16;

    public ChunkEntity()
    {
        Blocks = new BlockEntity[Size * Size * Height];
    }


    public BlockEntity[] Blocks { get; set; }

    public BlockEntity GetBlock(int x, int y, int z)
    {
        return Blocks[x + y * Size + z * Size * Size];
    }

    public void SetBlock(int x, int y, int z, BlockEntity block)
    {
        Blocks[x + y * Size + z * Size * Size] = block;
    }

    public BlockEntity GetBlock(Vector3 position)
    {
        return GetBlock((int)position.X, (int)position.Y, (int)position.Z);
    }

    public void SetBlock(Vector3 position, BlockEntity block)
    {
        SetBlock((int)position.X, (int)position.Y, (int)position.Z, block);
    }

    public BlockEntity GetBlock(int index)
    {
        return Blocks[index];
    }

    public void SetBlock(int index, BlockEntity block)
    {
        Blocks[index] = block;
    }

    public int GetIndex(int x, int y, int z)
    {
        return x + y * Size + z * Size * Size;
    }

    public int GetIndex(Vector3 position)
    {
        return GetIndex((int)position.X, (int)position.Y, (int)position.Z);
    }


    public BlockEntity this[int x, int y, int z]
    {
        get => GetBlock(x, y, z);
        set => SetBlock(x, y, z, value);
    }

    public BlockEntity this[Vector3 position]
    {
        get => GetBlock(position);
        set => SetBlock(position, value);
    }
}
