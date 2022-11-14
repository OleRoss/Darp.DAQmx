using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Ble.Config;
using Ble.Gap;
using Ble.Gatt;
using Ble.WinRT.Gap;
using Ble.WinRT.Gatt;
using Microsoft.Extensions.Logging;

namespace Ble.WinRT;

public class WinBleAdapter : IBleAdapter
{
    private readonly ICollection<IConnectedPeripheral> _devices;

    private readonly ILogger? _logger;
    private readonly WinAdvertisementScanner _scanner;
    public WinBleAdapter(ILogger? logger)
    {
        _logger = logger;
        _devices = new List<IConnectedPeripheral>();
        _scanner = new WinAdvertisementScanner(this, logger);
    }

    public WinBleAdapter() : this(null)
    {
    }

    public IAdvertisementScanner Scanner() => _scanner;

    private async Task<IConnectedPeripheral?> ConnectDeviceAsync(Func<Task<BluetoothLEDevice?>> deviceFunc,
        Configuration? configuration,
        CancellationToken cancellationToken)
    {
        // Please make sure T is of IDevice, specifying the IDevice lead to errors for me :/
        _logger?.LogTrace("Attempting connection to device");

        BluetoothLEDevice? winDev = await deviceFunc();
        if (winDev is null)
        {
            _logger?.LogWarning("Connection to device has failed!");
            return null;
        }
        var peripheral = new WinConnectedPeripheral(winDev, this, _logger);
        _logger?.LogDebug("Connection to device {@Device} has succeeded!", peripheral);
        _devices.Add(peripheral);
            
        bool success = await peripheral.DiscoverServicesAsync(configuration, cancellationToken);
        if (success)
        {
            _logger?.LogDebug("Successfully connected");
            return peripheral;
        }

        _logger?.LogWarning("Service discovery with failed. Disconnecting from peripheral...");
        peripheral.Dispose();
        return null;
    }

    public async Task<IConnectedPeripheral?> ConnectAsync(ulong bluetoothId,
        Configuration? peripheral,
        CancellationToken cancellationToken = default)
    {
        return await ConnectDeviceAsync(async () => await BluetoothLEDevice.FromBluetoothAddressAsync(bluetoothId),
            peripheral,
            cancellationToken);
    }

    public void DisconnectAll()
    {
        _logger?.LogTrace("Clearing {Service}", nameof(WinBleAdapter));
        foreach (IConnectedPeripheral device in _devices.ToList())
            device.Dispose();
    }

    public void Dispose() => DisconnectAll();

    public bool Disconnect(IConnectedPeripheral bleDevice)
    {
        bleDevice.Dispose();
        return _devices.Remove(bleDevice);
    }
}