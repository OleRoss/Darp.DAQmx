

using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Foundation;
using Ble.Gatt;
using Ble.Uuid;
using Microsoft.Extensions.Logging;

namespace Ble.WinRT.Gatt;

public class WinGattCharacteristic : IConnectedGattCharacteristic
{
    private readonly GattCharacteristic _characteristic;
    private readonly ILogger? _logger;
    private readonly IDictionary<Guid, IConnectedGattDescriptor> _descriptorDictionary = new Dictionary<Guid, IConnectedGattDescriptor>();

    public WinGattCharacteristic(GattCharacteristic characteristic, ILogger? logger)
    {
        _characteristic = characteristic;
        _logger = logger;
    }

    public Guid Uuid => _characteristic.Uuid;
    public ICollection<IConnectedGattDescriptor> Descriptors => _descriptorDictionary.Values;
    public IConnectedGattDescriptor this[Guid guid] => _descriptorDictionary[guid];
    public IConnectedGattDescriptor this[GattUuid guid] => _descriptorDictionary.Get(guid);
    public bool ContainsDescriptor(Guid guid) => _descriptorDictionary.ContainsKey(guid);
    public bool ContainsDescriptor(GattUuid guid) => _descriptorDictionary.ContainsKey(guid);

    public Property Property => (Property)_characteristic.CharacteristicProperties;
    public async Task<bool> WriteAsync(byte[] bytes, CancellationToken cancellationToken)
    {
        GattWriteResult? res = await _characteristic.WriteValueWithResultAsync(bytes.AsBuffer());
        if (res.Status is GattCommunicationStatus.Success) return true;
        _logger?.LogError("Could not write data [{@Data}] to {@Characteristic} because of {Status}",
            bytes, this, res.Status);
        return false;
    }

    public async Task<IObservable<byte[]>> EnableNotificationsAsync(CancellationToken cancellationToken)
    {
        var res = await _characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
            GattClientCharacteristicConfigurationDescriptorValue.Notify);
        if (res != GattCommunicationStatus.Success)
            return Observable.Empty<byte[]>();

        return Observable.FromEventPattern<TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs>, GattCharacteristic, GattValueChangedEventArgs>(
                    addHandler => _characteristic.ValueChanged += addHandler,
                    removeHandler => _characteristic.ValueChanged -= removeHandler)
            .Select(x => x.EventArgs.CharacteristicValue.ToArray())
            .Where(x => x != null);
    }
}