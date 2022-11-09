using System;
using Ble.Utils;

namespace Ble.Configuration;

public sealed class GattService
{
    public GattService(Guid serviceUuid)
    {
        ServiceUuid = serviceUuid;
        Characteristics = new GenericCollection<GattCharacteristic, GattService>(this);
    }

    public Guid ServiceUuid { get; }
    public GenericCollection<GattCharacteristic, GattService> Characteristics { get; }

    public GattService(string serviceUuid) : this(serviceUuid.ToBleGuid()) {}
}