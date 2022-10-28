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

    public async IAsyncEnumerable<IGattService> GetServicesAsync(CacheMode cacheMode)
    {
        if (ble_gattc.SdBleGattcPrimaryServicesDiscover(_service.Adapter, ConnectionHandle, 0x01, null)
            .IsFailed(_logger, "Primary Service Discovery failed"))
        {
            yield break;
        }

        _service.OnPrimaryServiceDiscoveryResponse += NrfUtils.CreateEventSubscription(out Func<BleGattcEvtT> func,
            4000, out CancellationToken token);
        BleGattcEvtT evt = await NrfUtils.FirstEventAsync(func, token);
        if (((uint)evt.ErrorHandle).IsFailed(_logger, "Discovery failed"))
            yield break;
        BleGattcEvtPrimSrvcDiscRspT? response = evt.@params.PrimSrvcDiscRsp;
        foreach (BleGattcServiceT bleGattcServiceT in response.Services)
        {
            yield return new NrfGattService(_service, this, bleGattcServiceT);
        }
    }

    public void Dispose()
    {
        ble_gap.SdBleGapDisconnect(_service.Adapter, ConnectionHandle,
            (byte)BLE_HCI_STATUS_CODES.BLE_HCI_REMOTE_USER_TERMINATED_CONNECTION)
            .IsFailed(_logger, $"Disconnection of device 0x{AddressString} ({ConnectionHandle}) failed");
    }
}