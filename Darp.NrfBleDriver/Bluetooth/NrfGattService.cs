using System.Runtime.CompilerServices;
using Bluetooth.Device;
using Bluetooth.Gatt;
using NrfBleDriver;
using Serilog;

namespace Darp.NrfBleDriver.Bluetooth;

public sealed class NrfGattService : IGattService
{
    private readonly ILogger? _logger;
    private readonly NrfBluetoothService _service;
    private readonly ushort _startHandle;
    private readonly ushort _endHandle;
    private readonly NrfDevice _device;

    public NrfGattService(ILogger? logger, NrfBluetoothService service, NrfDevice device, BleGattcServiceT gattcService)
    {
        _logger = logger;
        _service = service;
        _device = device;
        Uuid = BitConverter.GetBytes(gattcService.Uuid.Uuid).ToGuid();
        _startHandle = gattcService.HandleRange.StartHandle;
        _endHandle = gattcService.HandleRange.EndHandle;
    }

    public void Dispose()
    {

    }

    public IBleDevice Device => _device;
    public Guid Uuid { get; }

    public async IAsyncEnumerable<IGattCharacteristic> GetCharacteristicsAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ushort startHandle = _startHandle;
        while (true)
        { 
            ble_gattc.SdBleGattcCharacteristicsDiscover(_service.Adapter,
                _device.ConnectionHandle,
                new BleGattcHandleRangeT { StartHandle = startHandle, EndHandle = _endHandle });
            BleGattcEvtT? res = await _service.CharacteristicDiscoveryResponseQueue.NextValueAsync(cancellationToken);
            if (res is null)
            {
                _logger?.Verbose("Could not get next characteristic");
                yield break;
            }

            var gattStatus = (BLE_GATT_STATUS_CODES)res.GattStatus;
            if (gattStatus is BLE_GATT_STATUS_CODES.BLE_GATT_STATUS_ATTERR_ATTRIBUTE_NOT_FOUND)
                yield break;
            if (gattStatus is not BLE_GATT_STATUS_CODES.BLE_GATT_STATUS_SUCCESS)
            {
                _logger?.Warning("Got characteristic with error: {GattError}, 0x{ErrorCode:X}", gattStatus, res.GattStatus);
                yield break;
            }
            foreach (BleGattcCharT bleGattcCharT in res.@params.CharDiscRsp.Chars)
            {
                yield return new NrfGattCharacteristic(_logger, bleGattcCharT);
            }
            startHandle = (ushort)(res.@params.CharDiscRsp.Chars.Last().HandleDecl + 1);
        }
    }
}