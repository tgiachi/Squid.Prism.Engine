using Squid.Prism.Server.Core.Types;

namespace Squid.Prism.Engine.Core.Data.World;

public record WorldBlockData(byte Id, string Name, int TextureId, BlockMetaType MetaType);
