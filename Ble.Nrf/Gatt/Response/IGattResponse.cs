using Ble.Nrf.Nrf;

namespace Ble.Nrf.Gatt.Response;

public interface IGattResponse
{
    ushort ConnHandle { get; }
    BLE_GATT_STATUS_CODES Status { get; }
    ushort ErrorHandle { get; }
    ushort AttributeHandle { get; }
}