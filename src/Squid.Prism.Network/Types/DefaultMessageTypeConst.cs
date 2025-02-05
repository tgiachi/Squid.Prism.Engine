namespace Squid.Prism.Network.Types;

public static class DefaultMessageTypeConst
{
    public const int VersionRequest = 0x0001;
    public const int VersionResponse = 0x0002;

    public const int MotdRequest = 0x0003;
    public const int MotdResponse = 0x0004;


    public const int LoginRequest = 0x0005;
    public const int LoginResponse = 0x0006;

    public const int AssetRequest = 0x0007;
    public const int AssetResponse = 0x0008;

    public const int AssetListRequest = 0x0009;
    public const int AssetListResponse = 0x000A;
}
