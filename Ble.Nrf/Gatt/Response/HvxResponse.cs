using Ble.Nrf.Nrf;

namespace Ble.Nrf.Gatt.Response;

public readonly record struct HvxResponse(
    ushort ConnHandle,
    BLE_GATT_STATUS_CODES Status,
    ushort ErrorHandle,
    ushort AttributeHandle,
    ushort Handle,
    BLE_GATT_HVX_TYPES Type,
    byte[] Data) : IGattResponse;