using System;
using System.Collections.Generic;
using Ble.Config;
using Ble.Uuid;

namespace Ble.Gatt;

public interface IConnectedPeripheral : IDisposable
{
    ushort ConnectionHandle { get; }
    ICollection<IConnectedGattService> Services { get; }
    IConnectedGattService this[Service service] { get; }
    IConnectedGattService this[Guid guid] { get; }
    IConnectedGattService this[GattUuid guid] { get; }
    IConnectedGattService Select(Service service);
}