using Bluetooth.Gatt;
using FluentResults;
using NrfBleDriver;
using Serilog;

namespace Darp.NrfBleDriver.Bluetooth;

public class NrfGattCharacteristic : IGattCharacteristic
{
    private readonly ILogger? _logger;

    public NrfGattCharacteristic(ILogger? logger, BleGattcCharT gattcCharacteristic)
    {
        _logger = logger;
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

    public bool IsConnected => true;
    public Property Property { get; }
    public Guid Uuid { get; }

    public Task<bool> SubscribeToNotifyTemporary(Action<byte[]> callbackFunc)
    {
        throw new NotImplementedException();
    }

    public Task<Result> WriteValueAsync(byte[] bytes)
    {
        throw new NotImplementedException();
    }

    public Task<Result<byte[]>> ReadDataAsync()
    {
        throw new NotImplementedException();
    }
}