using LiteNetLib;
using LiteNetLib.Utils;

namespace Squid.Prism.Network.Interfaces.Sessions;

public interface ISessionObject
{

    string Id { get;  }

    NetPeer Peer { get; }

    NetDataWriter Writer { get; }

    Dictionary<string, object> Data { get; }

    DateTime LastActive { get; set; }

    SemaphoreSlim WriteLock { get; }

    bool IsLoggedIn { get; set; }

    TDataObject GetDataObject<TDataObject>(string key, bool throwIfNowExist = true);

    void SetDataObject<TDataObject>(string key, TDataObject value);
}
