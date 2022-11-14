using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Ble.Config;
using Ble.Gatt;
using Ble.Uuid;
using Microsoft.Extensions.Logging;

namespace Ble.WinRT.Gatt;

public sealed class WinConnectedPeripheral : IConnectedPeripheral
{
    private readonly WinBleAdapter _adapter;
    private readonly BluetoothLEDevice _device;
    private readonly ILogger? _logger;
    private readonly IDictionary<Guid, WinGattService> _serviceDictionary = new Dictionary<Guid, WinGattService>();

    public WinConnectedPeripheral(BluetoothLEDevice device, WinBleAdapter adapter, ILogger? logger)
    {
        _device = device;
        _adapter = adapter;
        _logger = logger;
    }


    public async Task<bool> DiscoverServicesAsync(Guid? serviceUuid, CancellationToken cancellationToken = default)
    {
        _logger?.LogDebug("Discovering primary services ...");
        
        DeviceAccessStatus accessStatus = await _device.RequestAccessAsync();
        if (accessStatus is not DeviceAccessStatus.Allowed)
            _logger?.LogWarning("Access request disallowed: {@Status}...", accessStatus);
        GattDeviceServicesResult serviceRes = serviceUuid is null
            ? await _device.GetGattServicesAsync()
            : await _device.GetGattServicesForUuidAsync(serviceUuid.Value);
        if (serviceRes?.Status is not GattCommunicationStatus.Success)
        {
            _logger?.LogWarning("Could not query new services for device {@Device} - got result {Status}",
                this, serviceRes?.Status);
            return false;
        }
        foreach (GattDeviceService serviceResService in serviceRes.Services)
        {
            _serviceDictionary[serviceResService.Uuid] = new WinGattService(serviceResService, _logger);
        }
        return true;
    }

    public ICollection<IConnectedGattService> Services => _serviceDictionary.Values.ToArray();
    public IConnectedGattService this[Service service] => _serviceDictionary[service.Uuid];
    public IConnectedGattService this[Guid guid] => _serviceDictionary[guid];
    public IConnectedGattService this[GattUuid guid] => _serviceDictionary[guid.ToBleGuid()];
    public bool ContainsService(Guid guid) => _serviceDictionary.ContainsKey(guid);
    public bool ContainsService(GattUuid guid) => _serviceDictionary.ContainsKey(guid);
    public IConnectedGattService Select(Service service) => _serviceDictionary[service.Uuid];

    public void Dispose()
    {
        
        foreach (var service in _serviceDictionary.Values) service.Dispose();
        _logger?.LogTrace("Disposing device {Name}", _device.Name);
        _device.Dispose();
        _adapter.Disconnect(this);
    }
}