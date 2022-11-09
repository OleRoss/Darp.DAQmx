using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ble.Configuration;
using Ble.Utils;

namespace Ble.Connection;

public interface IConnectedGattService
{
    Guid Uuid { get; }
    ICollection<IConnectedGattCharacteristic> Characteristics { get; }

    IConnectedGattCharacteristic this[GattCharacteristic characteristic] { get; }
    IConnectedGattCharacteristic this[Guid characteristicGuid] { get; }
    bool ContainsCharacteristic(Guid guid);
    bool ContainsCharacteristic(DefaultUuid guid);
    ConnectedGattCharacteristic<TCharacteristic> Select<TCharacteristic>(TCharacteristic characteristic)
        where TCharacteristic : GattCharacteristic;

    public Task<bool> DiscoverCharacteristics(CancellationToken cancellationToken);
}