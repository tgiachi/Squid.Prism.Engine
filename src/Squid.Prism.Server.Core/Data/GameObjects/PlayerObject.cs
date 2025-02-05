using System.ComponentModel;
using System.Numerics;
using System.Reactive.Subjects;

namespace Squid.Prism.Server.Core.Data.GameObjects;

public class PlayerObject : INotifyPropertyChanged
{
    public Vector3 Position { get; set; }

    public Vector3 Rotation { get; set; }

    public Subject<Vector3> PositionSubject { get; } = new();

    public Subject<Vector3> RotationSubject { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    public PlayerObject()
    {
        PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(Position))
            {
                PositionSubject.OnNext(Position);
            }
            else if (args.PropertyName == nameof(Rotation))
            {
                RotationSubject.OnNext(Rotation);
            }
        };
    }
}
