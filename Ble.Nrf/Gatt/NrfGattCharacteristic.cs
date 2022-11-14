using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ble.Config;
using Ble.Gatt;
using Ble.Nrf.Nrf;
using Ble.Uuid;
using Microsoft.Extensions.Logging;
using NrfBleDriver;

namespace Ble.Nrf.Gatt;

public class NrfGattCharacteristic : IConnectedGattCharacteristic
{
    public ushort StartHandle { get; }
    public ushort EndHandle { get; internal set; }
    private readonly NrfAdapter _adapter;
    private readonly BleGattcCharT _gattcCharacteristic;
    private readonly ILogger? _logger;
    private readonly IDictionary<Guid, IConnectedGattDescriptor> _descriptorDictionary = new Dictionary<Guid, IConnectedGattDescriptor>();
    public NrfGattCharacteristic(NrfAdapter adapter,
        NrfGattService nrfGattService,
        BleGattcCharT gattcCharacteristic,
        ushort startHandle,
        ushort endHandle,
        ILogger? logger)
    {
        StartHandle = startHandle;
        EndHandle = endHandle;
        _adapter = adapter;
        _gattcCharacteristic = gattcCharacteristic;
        _logger = logger;
        Uuid = BitConverter.GetBytes(gattcCharacteristic.Uuid.Uuid).ToBleGuid();
        Property = Property.None;
        if (gattcCharacteristic.CharProps.Broadcast == (ushort)Property.Broadcast)
            Property |= Property.Broadcast;
        if (gattcCharacteristic.CharProps.Read == (ushort)Property.Read)
            Property |= Property.Read;
        if (gattcCharacteristic.CharProps.WriteWoResp == (ushort)Property.WriteWithoutResponse)
            Property |= Property.WriteWithoutResponse;
        if (gattcCharacteristic.CharProps.Write == (ushort)Property.Write)
            Property |= Property.Write;
        if (gattcCharacteristic.CharProps.Notify == (ushort)Property.Notify)
            Property |= Property.Notify;
        if (gattcCharacteristic.CharProps.Indicate == (ushort)Property.Indicate)
            Property |= Property.Indicate;
        if (gattcCharacteristic.CharProps.AuthSignedWr == (ushort)Property.AuthenticatedSignedWrites)
            Property |= Property.AuthenticatedSignedWrites;
    }

    public override string ToString() => $"{nameof(GattCharacteristic)} (Uuid={_gattcCharacteristic.Uuid.Uuid:X}, NumChars={Descriptors.Count}, Property={Property}, StartHandle={StartHandle}, EndHandle={EndHandle})";

    public Guid Uuid { get; }
    public ICollection<IConnectedGattDescriptor> Descriptors => _descriptorDictionary.Values;
    public IConnectedGattDescriptor this[Guid guid] => _descriptorDictionary[guid];
    public IConnectedGattDescriptor this[GattUuid guid] => _descriptorDictionary.Get(guid);
    public bool ContainsDescriptor(Guid guid) => _descriptorDictionary.ContainsKey(guid);
    public bool ContainsDescriptor(GattUuid guid) => _descriptorDictionary.ContainsKey(guid);

    public Property Property {get;}
    public async Task<bool> WriteAsync(byte[] bytes, CancellationToken cancellationToken)
    {
        return await this[Uuid].WriteAsync(bytes, cancellationToken);
    }

    public async Task<IObservable<byte[]>> EnableNotificationsAsync(CancellationToken cancellationToken)
    {
        if (!await this[GattUuid.ClientCharacteristicConfiguration]
                .WriteAsync(new byte[] { 1, 0 }, cancellationToken))
        {
            return Observable.Empty<byte[]>();
        }
        return _adapter.HvxResponses
            .Where(x => x.Type is BLE_GATT_HVX_TYPES.BLE_GATT_HVX_NOTIFICATION)
            .Select(x => x.Data);
    }

    internal void AddDescriptor(IConnectedGattDescriptor descriptor) => _descriptorDictionary[descriptor.Uuid] = descriptor;
}