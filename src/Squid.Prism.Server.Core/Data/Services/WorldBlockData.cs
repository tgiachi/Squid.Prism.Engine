using Squid.Prism.Server.Core.Types;

namespace Squid.Prism.Server.Core.Data.Services;

public record WorldBlockData(byte Id, string Name, int TextureId, BlockMetaType MetaType);
