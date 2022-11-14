using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Ble.Gatt;
using Ble.Uuid;
using Microsoft.Extensions.Logging;

namespace Ble.WinRT.Gatt;

public class WinGattService : IConnectedGattService, IDisposable
{
    private readonly GattDeviceService _service;
    private readonly ILogger? _logger;
    private readonly IDictionary<Guid, IConnectedGattCharacteristic> _characteristicDictionary = new Dictionary<Guid, IConnectedGattCharacteristic>();

    public WinGattService(GattDeviceService service, ILogger? logger)
    {
        _service = service;
        _logger = logger;
    }

    public Guid Uuid => _service.Uuid;
    public ICollection<IConnectedGattCharacteristic> Characteristics => _characteristicDictionary.Values;

    public IConnectedGattCharacteristic this[Config.GattCharacteristic characteristic] => _characteristicDictionary[characteristic.Uuid];
    public IConnectedGattCharacteristic this[Guid characteristicGuid] => _characteristicDictionary[characteristicGuid];
    public bool ContainsCharacteristic(Guid guid) => _characteristicDictionary.ContainsKey(guid);
    public bool ContainsCharacteristic(GattUuid guid) => _characteristicDictionary.ContainsKey(guid);

    public async Task<bool> DiscoverCharacteristics(CancellationToken cancellationToken)
    {
        var charRes = await _service.GetCharacteristicsAsync();
        if (charRes?.Status is not GattCommunicationStatus.Success)
        {
            _logger?.LogWarning("Could not query new characteristics - got result {Status}", charRes?.Status);
            return false;
        }
        foreach (GattCharacteristic gattCharacteristic in charRes.Characteristics)
        {
            _characteristicDictionary[gattCharacteristic.Uuid] = new WinGattCharacteristic(gattCharacteristic, _logger);
        }
        return true;
    }

    public void Dispose()
    {
        _logger?.LogTrace("Disposing service {Uuid}", _service.Uuid);
        _service.Dispose();
    }
}