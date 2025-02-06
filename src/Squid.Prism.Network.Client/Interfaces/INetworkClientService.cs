using System.ComponentModel;
using System.Numerics;
using System.Reactive.Subjects;
using Squid.Prism.Engine.Core.World;
using Squid.Prism.Server.Core.Data.World;

namespace Squid.Prism.Network.Client.Interfaces;

public interface INetworkClientService : INotifyPropertyChanged
{
    Vector3 Position { get; set; }
    Vector3 Rotation { get; set; }
    Subject<Vector3> PositionSubject { get; }
    Subject<ChunkEntity> ChunkSubject { get; }
}
