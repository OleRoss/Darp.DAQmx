using System;
using Ble.Uuid;

namespace Ble.Config;

public sealed class Service
{
    public Service(Guid uuid)
    {
        Uuid = uuid;
        Characteristics = new GenericCollection<GattCharacteristic, Service>(this);
    }

    public Guid Uuid { get; }
    public GenericCollection<GattCharacteristic, Service> Characteristics { get; }

    public Service(string serviceUuid) : this(serviceUuid.ToBleGuid()) {}
}