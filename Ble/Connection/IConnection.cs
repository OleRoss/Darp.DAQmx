using System;
using System.Collections.Generic;
using Ble.Configuration;
using Ble.Utils;

namespace Ble.Connection;

public interface IConnection : IDisposable
{
    ushort ConnectionHandle { get; }
    ICollection<IConnectedGattService> Services { get; }
    IConnectedGattService this[GattService service] { get; }
    IConnectedGattService this[Guid guid] { get; }
    IConnectedGattService this[DefaultUuid guid] { get; }
    IConnectedGattService Select(GattService service);
}