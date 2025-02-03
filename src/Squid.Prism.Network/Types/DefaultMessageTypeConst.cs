namespace Squid.Prism.Network.Types;

public static class DefaultMessageTypeConst
{
    public static readonly int VersionMessageRequest = 0x0001;
    public static readonly int VersionMessageResponse = 0x0002;

    public static readonly int MotdMessageRequest = 0x0003;
    public static readonly int MotdMessageResponse = 0x0004;


    public static readonly int LoginMessageRequest = 0x0005;
    public static readonly int LoginMessageResponse = 0x0006;
}
