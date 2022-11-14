using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ble.Config;
using Ble.Uuid;

namespace Ble.Gatt;

public interface IConnectedGattService
{
    Guid Uuid { get; }
    ICollection<IConnectedGattCharacteristic> Characteristics { get; }

    IConnectedGattCharacteristic this[GattCharacteristic characteristic] { get; }
    IConnectedGattCharacteristic this[Guid characteristicGuid] { get; }
    bool ContainsCharacteristic(Guid guid);
    bool ContainsCharacteristic(GattUuid guid);

    public Task<bool> DiscoverCharacteristics(CancellationToken cancellationToken);
}

public static class CharEx
{
    public static ConnectedGattCharacteristic<TCharacteristic> Select<TCharacteristic>(this IConnectedGattService service,
        TCharacteristic characteristic)
        where TCharacteristic : Config.GattCharacteristic
    {
        IConnectedGattCharacteristic connChar = service[characteristic.Uuid];
        if (!characteristic.IsValid(connChar))
            throw new ArgumentException($"Characteristic {characteristic} is not valid for service", nameof(characteristic));
        return new ConnectedGattCharacteristic<TCharacteristic>(characteristic, connChar);
    }
}