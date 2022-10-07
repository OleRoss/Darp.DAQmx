namespace NrfBleDriver;

public static class BleConnHandles
{
    /// Invalid Connection Handle.
    public const ushort BLE_CONN_HANDLE_INVALID = 0xFFFF;

    /// Applies to all Connection Handles.
    public const ushort BLE_CONN_HANDLE_ALL = 0xFFFE;
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

public static class BLE_GAP_SCAN_FILTER_POLICIES
{
    /// Accept all advertising packets except directed advertising packets not addressed to this device.
    public const byte BLE_GAP_SCAN_FP_ACCEPT_ALL = 0x00;
    /// Accept advertising packets from devices in the whitelist except directed packets not addressed to this device
    public const byte BLE_GAP_SCAN_FP_WHITELIST = 0x01;
    /// <summary>
    /// Accept all advertising packets specified in @ref BLE_GAP_SCAN_FP_ACCEPT_ALL.
    /// In addition, accept directed advertising packets, where the advertiser's
    /// address is a resolvable private address that cannot be resolved.
    /// </summary>
    public const byte BLE_GAP_SCAN_FP_ALL_NOT_RESOLVED_DIRECTED = 0x02;
    /// <summary>
    /// Accept all advertising packets specified in @ref BLE_GAP_SCAN_FP_WHITELIST.
    /// In addition, accept directed advertising packets, where the advertiser's
    /// address is a resolvable private address that cannot be resolved.
    /// </summary>
    public const byte BLE_GAP_SCAN_FP_WHITELIST_NOT_RESOLVED_DIRECTED = 0x03;
}

public static class BLE_GAP_PHYS
{
    /// Automatic PHY selection. Refer @ref sd_ble_gap_phy_update for more information
    public const byte BLE_GAP_PHY_AUTO = 0x00;
    /// 1 Mbps PHY
    public const byte BLE_GAP_PHY_1MBPS = 0x01;
    /// 2 Mbps PHY
    public const byte BLE_GAP_PHY_2MBPS = 0x02;
    /// Coded PHY.
    public const byte BLE_GAP_PHY_CODED = 0x04;
    /// PHY is not configured
    public const byte BLE_GAP_PHY_NOT_SET = 0xFF;
    /// All PHYs are supported
    public const byte BLE_GAP_PHYS_SUPPORTED = BLE_GAP_PHY_1MBPS | BLE_GAP_PHY_2MBPS | BLE_GAP_PHY_CODED;
}

public static class BLE_GAP_ADV_SET
{
    /// @brief Advertising set handle not set.
    public const byte BLE_GAP_ADV_SET_HANDLE_NOT_SET = 0xFF;
    /// @brief The default number of advertising sets.
    public const byte  BLE_GAP_ADV_SET_COUNT_DEFAULT = 1;
    /// @brief The maximum number of advertising sets supported by this SoftDevice.
    public const byte  BLE_GAP_ADV_SET_COUNT_MAX = 1;
}

public static class BLE_GAP_ADDR_TYPES
{
    ///Public (identity) address.
    public const byte BLE_GAP_ADDR_TYPE_PUBLIC = 0x00;
    ///Random static (identity) address.
    public const byte BLE_GAP_ADDR_TYPE_RANDOM_STATIC = 0x01;
    ///Random private resolvable address.
    public const byte BLE_GAP_ADDR_TYPE_RANDOM_PRIVATE_RESOLVABLE = 0x02;
    ///Random private non-resolvable address.
    public const byte BLE_GAP_ADDR_TYPE_RANDOM_PRIVATE_NON_RESOLVABLE = 0x03;
    ///An advertiser may advertise without its address. This type of advertising is called anonymous.
    public const byte BLE_GAP_ADDR_TYPE_ANONYMOUS = 0x7F;
}

public enum BLE_GAP_ADV_DATA_STATUS : ushort
{
    /// All data in the advertising event have been received
    BLE_GAP_ADV_DATA_STATUS_COMPLETE = 0x00,
    /// <summary>
    /// More data to be received.
    /// @note This value will only be used if
    /// @ref ble_gap_scan_params_t::report_incomplete_evts and
    /// @ref ble_gap_adv_report_type_t::extended_pdu are set to true.
    /// </summary>
    BLE_GAP_ADV_DATA_STATUS_INCOMPLETE_MORE_DATA = 0x01,
    /// Incomplete data. Buffer size insufficient to receive more.
    /// @note This value will only be used if
    /// @ref ble_gap_adv_report_type_t::extended_pdu is set to true.
    BLE_GAP_ADV_DATA_STATUS_INCOMPLETE_TRUNCATED = 0x02,
    /// <summary>
    /// Failed to receive the remaining data.
    /// @note This value will only be used if
    /// @ref ble_gap_adv_report_type_t::extended_pdu is set to true
    /// </summary>
    BLE_GAP_ADV_DATA_STATUS_INCOMPLETE_MISSED = 0x03
}