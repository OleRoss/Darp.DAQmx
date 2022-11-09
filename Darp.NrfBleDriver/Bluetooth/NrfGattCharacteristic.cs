using Bluetooth.Gatt;
using Darp.NrfBleDriver.Nrf;
using ErrorOr;
using NrfBleDriver;
using Serilog;

namespace Darp.NrfBleDriver.Bluetooth;

public class NrfGattDescriptor
{
    
}

public class NrfGattCharacteristic : IGattCharacteristic
{
    private readonly ILogger? _logger;
    private readonly NrfBluetoothService _service;
    private readonly NrfDevice _device;
    private readonly BleGattcCharT _gattcCharacteristic;

    public NrfGattCharacteristic(ILogger? logger,
        NrfBluetoothService service,
        NrfDevice device,
        BleGattcCharT gattcCharacteristic)
    {
        _logger = logger;
        _service = service;
        _device = device;
        _gattcCharacteristic = gattcCharacteristic;
        Property = default;
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
        Uuid = BitConverter.GetBytes(gattcCharacteristic.Uuid.Uuid).ToGuid();
    }

    public ushort HandleValue => _gattcCharacteristic.HandleValue;
    public bool IsConnected => true;
    public Property Property { get; }
    public Guid Uuid { get; }

    public async Task<ErrorOr<Success>> SubscribeToNotify(Action<byte[]> callbackFunc, CancellationToken cancellationToken)
    {
        _logger?.Debug("Starting subscription");
        var paramList = new byte[] { 1, 0 };
        return await WriteValueAsync(paramList, cancellationToken);
    }

    public async Task<ErrorOr<Success>> WriteValueAsync(byte[] bytes, CancellationToken cancellationToken)
    {
        _logger?.Debug("Starting writing");
        unsafe
        {
            fixed (byte* pBytes = &bytes[0])
            {
                var writeParams = new BleGattcWriteParamsT
                {
                    WriteOp = (byte)BLE_GATT_WRITE_OPS.BLE_GATT_OP_WRITE_REQ,
                    Flags = (byte)BLE_GATT_EXEC_WRITE_FLAGS.UNUSED,
                    Handle = HandleValue,
                    PValue = pBytes,
                    Len = (byte)bytes.Length,
                    Offset = 0
                };
                ble_gattc.SdBleGattcWrite(_service.Adapter, _device.ConnectionHandle, writeParams);
            }
        }
        BleGattcEvtT? writeResult = await _service.WriteResponseQueue.NextValueAsync(cancellationToken);
        if (writeResult is null)
            return BleErrors.WriteToPeripheralFailed;
        if (writeResult.GattStatus is not (ushort)BLE_GATT_STATUS_CODES.BLE_GATT_STATUS_SUCCESS)
        {
            _logger?.Debug("No success: {GattStatus} (0x{GattStatusCode})", (BLE_GATT_STATUS_CODES)writeResult.GattStatus, writeResult.GattStatus);
            return BleErrors.WriteToPeripheralSendFailed;
        }
        _logger?.Debug("Writing successful");
        return Result.Success;
    }

    public Task<ErrorOr<byte[]>> ReadDataAsync()
    {
        throw new NotImplementedException();
    }

    public static async Task<ErrorOr<NrfGattCharacteristic>> FromNrfGattcChar(ILogger? logger,
        NrfBluetoothService service,
        NrfDevice device,
        BleGattcCharT gattcCharacteristic)
    {
        ushort startHandle = gattcCharacteristic.HandleValue;
        while (true)
        {
            ble_gattc.SdBleGattcCharacteristicsDiscover(service.Adapter,
                device.ConnectionHandle,
                new BleGattcHandleRangeT { StartHandle = startHandle, EndHandle = gattcCharacteristic. });
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
                ErrorOr<NrfGattCharacteristic> errorOrCharacteristic = await NrfGattCharacteristic
                    .FromNrfGattcChar(_logger, _service, _device, bleGattcCharT);
                if (!errorOrCharacteristic.IsError)
                    yield return errorOrCharacteristic.Value;
                _logger?.Warning("Could not get characteristic: {@Errors}", errorOrCharacteristic.Errors);
            }
            startHandle = (ushort)(res.@params.CharDiscRsp.Chars.Last().HandleDecl + 1);
        }
        return new NrfGattCharacteristic(logger, service, device, gattcCharacteristic);
    }
}