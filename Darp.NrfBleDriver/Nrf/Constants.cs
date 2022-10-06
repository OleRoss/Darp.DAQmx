namespace NrfBleDriver;

public static class BleConnHandles
{
    /// Invalid Connection Handle.
    public const ushort BLE_CONN_HANDLE_INVALID = 0xFFFF;

    /// Applies to all Connection Handles.
    public const ushort BLE_CONN_HANDLE_ALL = 0xFFFE;
}

public static class BLE_GAP_PHYS
{
    /// Automatic PHY selection. Refer @ref sd_ble_gap_phy_update for more information.
    public const byte BLE_GAP_PHY_AUTO = 0x00;
    /// 1 Mbps PHY.
    public const byte BLE_GAP_PHY_1MBPS = 0x01;
    /// 2 Mbps PHY.
    public const byte BLE_GAP_PHY_2MBPS = 0x02;
    /// Coded PHY.
    public const byte BLE_GAP_PHY_CODED = 0x04;
}

public static class NrfErrorBase
{
    /// Global error base
    public const ushort NRF_ERROR_BASE_NUM = 0x0;
    /// SDM error base
    public const ushort NRF_ERROR_SDM_BASE_NUM = 0x1000;
    /// SoC error base
    public const ushort NRF_ERROR_SOC_BASE_NUM = 0x2000;
    /// STK error base
    public const ushort NRF_ERROR_STK_BASE_NUM = 0x3000;
}

public static class NrfError
{
    /// Successful command
    public const ushort NRF_SUCCESS = NrfErrorBase.NRF_ERROR_BASE_NUM + 0;
    /// SVC handler is missing
    public const ushort NRF_ERROR_SVC_HANDLER_MISSING = NrfErrorBase.NRF_ERROR_BASE_NUM + 1;
    /// SoftDevice has not been enabled
    public const ushort NRF_ERROR_SOFTDEVICE_NOT_ENABLED = NrfErrorBase.NRF_ERROR_BASE_NUM + 2;
    /// Internal Error
    public const ushort NRF_ERROR_INTERNAL = NrfErrorBase.NRF_ERROR_BASE_NUM + 3;
    /// No Memory for operation
    public const ushort NRF_ERROR_NO_MEM = NrfErrorBase.NRF_ERROR_BASE_NUM + 4;
    /// Not found
    public const ushort NRF_ERROR_NOT_FOUND = NrfErrorBase.NRF_ERROR_BASE_NUM + 5;
    /// Not supported
    public const ushort NRF_ERROR_NOT_SUPPORTED = NrfErrorBase.NRF_ERROR_BASE_NUM + 6;
    /// Invalid Parameter
    public const ushort NRF_ERROR_INVALID_PARAM = NrfErrorBase.NRF_ERROR_BASE_NUM + 7;
    /// Invalid state, operation disallowed in this state
    public const ushort NRF_ERROR_INVALID_STATE = NrfErrorBase.NRF_ERROR_BASE_NUM + 8;
    /// Invalid Length
    public const ushort NRF_ERROR_INVALID_LENGTH = NrfErrorBase.NRF_ERROR_BASE_NUM + 9;
    /// Invalid Flags
    public const ushort NRF_ERROR_INVALID_FLAGS = NrfErrorBase.NRF_ERROR_BASE_NUM + 10;
    /// Invalid Data
    public const ushort NRF_ERROR_INVALID_DATA = NrfErrorBase.NRF_ERROR_BASE_NUM + 11;
    /// Invalid Data size
    public const ushort NRF_ERROR_DATA_SIZE = NrfErrorBase.NRF_ERROR_BASE_NUM + 12;
    /// Operation timed out
    public const ushort NRF_ERROR_TIMEOUT = NrfErrorBase.NRF_ERROR_BASE_NUM + 13;
    /// Null Pointer
    public const ushort NRF_ERROR_NULL = NrfErrorBase.NRF_ERROR_BASE_NUM + 14;
    /// Forbidden Operation
    public const ushort NRF_ERROR_FORBIDDEN = NrfErrorBase.NRF_ERROR_BASE_NUM + 15;
    /// Bad Memory Address
    public const ushort NRF_ERROR_INVALID_ADDR = NrfErrorBase.NRF_ERROR_BASE_NUM + 16;
    /// Busy
    public const ushort NRF_ERROR_BUSY = NrfErrorBase.NRF_ERROR_BASE_NUM + 17;
    /// Maximum connection count exceeded.
    public const ushort NRF_ERROR_CONN_COUNT = NrfErrorBase.NRF_ERROR_BASE_NUM + 18;
    /// Not enough resources for operation
    public const ushort NRF_ERROR_RESOURCES = NrfErrorBase.NRF_ERROR_BASE_NUM + 19;
}
         