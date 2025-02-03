namespace Squid.Prism.Server.Core.Data.Services;

public record WorldBlockData(byte Id, string Name, int TextureId, bool IsSolid, bool IsTransparent, bool IsLiquid);
