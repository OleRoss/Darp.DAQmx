using Bluetooth.Device;
using Bluetooth.Gatt;
using NrfBleDriver;

namespace Darp.NrfBleDriver.Bluetooth;

public sealed class NrfGattService : IGattService
{
    private readonly NrfBluetoothService _service;

    public NrfGattService(NrfBluetoothService service, IBleDevice device, BleGattcServiceT gattcService)
    {
        _service = service;
        Device = device;
        Uuid = BitConverter.GetBytes(gattcService.Uuid.Uuid).ToGuid();
    }

    public void Dispose() { }

    public IBleDevice Device { get; }
    public Guid Uuid { get; }
    public IAsyncEnumerable<IGattCharacteristic> GetCharacteristicsAsync() => throw new NotImplementedException();
}