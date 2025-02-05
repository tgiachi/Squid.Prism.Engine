using System.ComponentModel;
using System.Numerics;
using System.Reactive.Subjects;

namespace Squid.Prism.Network.Client.Interfaces;

public interface INetworkClientService : INotifyPropertyChanged
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }
    
    public Subject<Vector3> PositionSubject { get; }
}
