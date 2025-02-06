namespace Squid.Prism.Server.Core.Types;

[Flags]
public enum BlockMetaType : byte
{
    None_Or_Air,
    Solid,
    Liquid,
    Gas,
    Light,
    Transparent,
    Walkable,
    Breakable,
    Placeable
}
