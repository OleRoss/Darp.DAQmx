using System.Runtime.CompilerServices;
using Bluetooth.Device;
using Bluetooth.Gatt;
using NrfBleDriver;
using Serilog;

namespace Darp.NrfBleDriver.Bluetooth;

public sealed class NrfDevice : IBleDevice
{
    private readonly NrfBluetoothService _service;
    private readonly byte[] _addressBytes;
    private readonly ILogger? _logger;

    public NrfDevice(NrfBluetoothService service, ushort connectionHandle, byte[] addressBytes, ILogger? logger)
    {
        _service = service;
        ConnectionHandle = connectionHandle;
        _addressBytes = addressBytes;
        _logger = logger;
        Connected = true;
    }

    public ushort ConnectionHandle { get; }
    public string Id => _addressBytes.ToAddressString();
    public bool Connected { get; }
    public ulong Address => _addressBytes.ToAddress();
    public string AddressString => _addressBytes.ToAddressString();

    public async IAsyncEnumerable<IGattService> GetServicesAsync(CacheMode cacheMode,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        _logger?.Information("Discovering services ...");

        ushort startHandle = 0x01;
        while (true)
        {
            if (ble_gattc.SdBleGattcPrimaryServicesDiscover(_service.Adapter, ConnectionHandle, startHandle, null)
                .IsFailed(_logger, $"Primary Service Discovery failed from {startHandle}"))
            {
                yield break;
            }
            BleGattcEvtT? evt = await _service.PrimaryServiceDiscoveryResponseQueue.NextValueAsync(cancellationToken);
            if (evt is null)
            {
                _logger?.Warning("Primary service discovery timed out");
                yield break;
            }
            if (((uint)evt.ErrorHandle).IsFailed(_logger, "Discovery failed"))
                yield break;
            BleGattcEvtPrimSrvcDiscRspT? response = evt.@params.PrimSrvcDiscRsp;
            if (response.Services.Length == 0)
                break;
            foreach (BleGattcServiceT bleGattcServiceT in response.Services)
            {
                yield return new NrfGattService(_logger, _service, this, bleGattcServiceT);
            }
            ushort endHandle = response.Services[^1].HandleRange.EndHandle;
            if (endHandle == 0xFFFF)
                yield break;
            startHandle = (ushort)(response.Services[^1].HandleRange.EndHandle + 1);
        }
    }

    public void Dispose()
    {
        _logger?.Verbose("Disconnecting from device {DeviceAddress}", AddressString);
        ble_gap.SdBleGapDisconnect(_service.Adapter, ConnectionHandle,
            (byte)BLE_HCI_STATUS_CODES.BLE_HCI_REMOTE_USER_TERMINATED_CONNECTION)
            .IsFailed(_logger, $"Disconnection of device 0x{AddressString} ({ConnectionHandle}) failed");
    }
}