using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Ble.Connection;
using Ble.Nrf.Nrf;
using Ble.Utils;
using Microsoft.Extensions.Logging;
using NrfBleDriver;

namespace Ble.Nrf.Gatt;

public class NrfGattDescriptor : IConnectedGattDescriptor
{
    private readonly NrfAdapter _adapter;
    private readonly NrfConnection _connection;
    private readonly BleGattcDescT _gattcDesc;
    private readonly ILogger? _logger;

    public NrfGattDescriptor(NrfAdapter adapter, NrfConnection connection, BleGattcDescT gattcDesc, ILogger? logger)
    {
        _adapter = adapter;
        _connection = connection;
        _gattcDesc = gattcDesc;
        _logger = logger;
        Uuid = BitConverter.GetBytes(gattcDesc.Uuid.Uuid).ToBleGuid();
    }

    public Guid Uuid { get; }
    public async Task<bool> WriteAsync(byte[] bytes, CancellationToken token)
    {
        if (bytes.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(bytes), "Bytes must have a length of at least 1");
        unsafe
        {
            fixed (byte* pBytes = &bytes[0])
            {
                var writeParams = new BleGattcWriteParamsT
                {
                    WriteOp = (byte)BLE_GATT_WRITE_OPS.BLE_GATT_OP_WRITE_REQ,
                    Flags = (byte)BLE_GATT_EXEC_WRITE_FLAGS.UNUSED,
                    Handle = _gattcDesc.Handle,
                    PValue = pBytes,
                    Len = (ushort)bytes.Length,
                    Offset = 0,
                };
                ble_gattc.SdBleGattcWrite(_adapter.AdapterHandle, _connection.ConnectionHandle, writeParams);
            }
        }
        BleGattcEvtT? result = await _adapter.WriteResponses.FirstOrDefaultAsync().ToTask(token);
        if (result is null)
        {
            _logger?.LogWarning("Could not finish descriptor write");
            return false;
        }
        if ((BLE_GATT_STATUS_CODES)result.GattStatus is BLE_GATT_STATUS_CODES.BLE_GATT_STATUS_SUCCESS)
            return true;
        
        _logger?.LogError("Write failed with status {Status}", result.GattStatus);
        return false;
    }

    public async Task<byte[]> ReadAsync(CancellationToken token)
    {
        ble_gattc.SdBleGattcRead(_adapter.AdapterHandle, _connection.ConnectionHandle, _gattcDesc.Handle, 0);
        
        BleGattcEvtT? result = await _adapter.ReadResponses.FirstOrDefaultAsync().ToTask(token);
        if (result is null)
        {
            _logger?.LogWarning("Could not finish descriptor write");
            return Array.Empty<byte>();
        }
        if ((BLE_GATT_STATUS_CODES)result.GattStatus is BLE_GATT_STATUS_CODES.BLE_GATT_STATUS_SUCCESS)
            return result.@params.ReadRsp.Data;
        
        _logger?.LogError("Read failed with status {Status}", result.GattStatus);
        return Array.Empty<byte>();
    }

    public override string ToString() => $"{nameof(NrfGattDescriptor)} (Uuid={_gattcDesc.Uuid.Uuid:X}, Handle={_gattcDesc.Handle})";
    
}