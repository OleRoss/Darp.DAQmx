using System.Reflection;

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

    public static string GetName(uint nrfError)
    {
        foreach (FieldInfo field in typeof(NrfError).GetFields(
                     BindingFlags.Static | BindingFlags.Public))
        {
            if (field.GetValue(null) is not ushort value) continue;
            if (value == nrfError)
                return field.Name;
        }
        return $"Unknown({nrfError:X})";
    }
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

public enum BLE_UUID_TYPES : byte
{
    /// Invalid UUID type.
    BLE_UUID_TYPE_UNKNOWN = 0x00,
    /// Bluetooth SIG UUID (16-bit)
    BLE_UUID_TYPE_BLE = 0x01,
    /// Vendor UUID types start at this index (128-bit)
    BLE_UUID_TYPE_VENDOR_BEGIN = 0x02
}

public enum BLE_HCI_STATUS_CODES : byte
{
    /// Success
    BLE_HCI_STATUS_CODE_SUCCESS = 0x00,
    /// Unknown BLE Command
    BLE_HCI_STATUS_CODE_UNKNOWN_BTLE_COMMAND = 0x01,
    /// Unknown Connection Identifier
    BLE_HCI_STATUS_CODE_UNKNOWN_CONNECTION_IDENTIFIER = 0x02,
/*0x03 Hardware Failure
0x04 Page Timeout
*/
    /// Authentication Failure
    BLE_HCI_AUTHENTICATION_FAILURE = 0x05,
    /// Pin or Key missing
    BLE_HCI_STATUS_CODE_PIN_OR_KEY_MISSING = 0x06,
    /// Memory Capacity Exceeded
    BLE_HCI_MEMORY_CAPACITY_EXCEEDED = 0x07,
    /// Connection Timeout
    BLE_HCI_CONNECTION_TIMEOUT = 0x08,
/*0x09 Connection Limit Exceeded
0x0A Synchronous Connection Limit To A Device Exceeded
0x0B ACL Connection Already Exists*/
    /// Command Disallowed
    BLE_HCI_STATUS_CODE_COMMAND_DISALLOWED = 0x0C,
/*0x0D Connection Rejected due to Limited Resources
0x0E Connection Rejected Due To Security Reasons
0x0F Connection Rejected due to Unacceptable BD_ADDR
0x10 Connection Accept Timeout Exceeded
0x11 Unsupported Feature or Parameter Value*/
    /// Invalid BLE Command Parameters
    BLE_HCI_STATUS_CODE_INVALID_BTLE_COMMAND_PARAMETERS = 0x12,
    /// Remote User Terminated Connection
    BLE_HCI_REMOTE_USER_TERMINATED_CONNECTION = 0x13,
    /// Remote Device Terminated Connection due to lowresources
    BLE_HCI_REMOTE_DEV_TERMINATION_DUE_TO_LOW_RESOURCES = 0x14,
    /// Remote Device Terminated Connection due to power off
    BLE_HCI_REMOTE_DEV_TERMINATION_DUE_TO_POWER_OFF = 0x15,
    /// Local Host Terminated Connection
    BLE_HCI_LOCAL_HOST_TERMINATED_CONNECTION = 0x16,
/*
0x17 Repeated Attempts
0x18 Pairing Not Allowed
0x19 Unknown LMP PDU
*/
    /// Unsupported Remote Feature
    BLE_HCI_UNSUPPORTED_REMOTE_FEATURE = 0x1A,
/*
0x1B SCO Offset Rejected
0x1C SCO Interval Rejected
0x1D SCO Air Mode Rejected*/
    /// Invalid LMP Parameters
    BLE_HCI_STATUS_CODE_INVALID_LMP_PARAMETERS = 0x1E,
    /// Unspecified Error
    BLE_HCI_STATUS_CODE_UNSPECIFIED_ERROR = 0x1F,
/*0x20 Unsupported LMP Parameter Value
0x21 Role Change Not Allowed
*/
    /// LMP Response Timeout
    BLE_HCI_STATUS_CODE_LMP_RESPONSE_TIMEOUT = 0x22,
    /// LMP Error Transaction Collision/LL Procedure Collision
    BLE_HCI_STATUS_CODE_LMP_ERROR_TRANSACTION_COLLISION = 0x23,
    /// LMP PDU Not Allowed
    BLE_HCI_STATUS_CODE_LMP_PDU_NOT_ALLOWED = 0x24,
/*0x25 Encryption Mode Not Acceptable
0x26 Link Key Can Not be Changed
0x27 Requested QoS Not Supported
*/
    /// Instant Passed
    BLE_HCI_INSTANT_PASSED = 0x28,
    /// Pairing with Unit Key Unsupported
    BLE_HCI_PAIRING_WITH_UNIT_KEY_UNSUPPORTED = 0x29,
    /// Different Transaction Collision
    BLE_HCI_DIFFERENT_TRANSACTION_COLLISION = 0x2A,
/*
0x2B Reserved
0x2C QoS Unacceptable Parameter
0x2D QoS Rejected
0x2E Channel Classification Not Supported
0x2F Insufficient Security
*/
    /// Parameter Out Of Mandatory Range
    BLE_HCI_PARAMETER_OUT_OF_MANDATORY_RANGE =0x30,
/*
0x31 Reserved
0x32 Role Switch Pending
0x33 Reserved
0x34 Reserved Slot Violation
0x35 Role Switch Failed
0x36 Extended Inquiry Response Too Large
0x37 Secure Simple Pairing Not Supported By Host.
0x38 Host Busy - Pairing
0x39 Connection Rejected due to No Suitable Channel Found*/
    /// Controller Busy
    BLE_HCI_CONTROLLER_BUSY = 0x3A,
    /// Connection Interval Unacceptable
    BLE_HCI_CONN_INTERVAL_UNACCEPTABLE = 0x3B,
    /// Directed Advertisement Timeout
    BLE_HCI_DIRECTED_ADVERTISER_TIMEOUT = 0x3C,
    /// Connection Terminated due to MIC Failure
    BLE_HCI_CONN_TERMINATED_DUE_TO_MIC_FAILURE = 0x3D,
    /// Connection Failed to be Established
    BLE_HCI_CONN_FAILED_TO_BE_ESTABLISHED = 0x3E
}

public enum BLE_GATT_STATUS_CODES : ushort
{
    ///Success.
    BLE_GATT_STATUS_SUCCESS = 0x0000,
    ///Unknown or not applicable status.
    BLE_GATT_STATUS_UNKNOWN = 0x0001,
    ///ATT Error: Invalid Error Code.
    BLE_GATT_STATUS_ATTERR_INVALID = 0x0100,
    ///ATT Error: Invalid Attribute Handle.
    BLE_GATT_STATUS_ATTERR_INVALID_HANDLE = 0x0101,
    ///ATT Error: Read not permitted.
    BLE_GATT_STATUS_ATTERR_READ_NOT_PERMITTED = 0x0102,
    ///ATT Error: Write not permitted.
    BLE_GATT_STATUS_ATTERR_WRITE_NOT_PERMITTED = 0x0103,
    ///ATT Error: Used in ATT as Invalid PDU.
    BLE_GATT_STATUS_ATTERR_INVALID_PDU = 0x0104,
    ///ATT Error: Authenticated link required.
    BLE_GATT_STATUS_ATTERR_INSUF_AUTHENTICATION = 0x0105,
    ///ATT Error: Used in ATT as Request Not Supported.
    BLE_GATT_STATUS_ATTERR_REQUEST_NOT_SUPPORTED = 0x0106,
    ///ATT Error: Offset specified was past the end of the attribute.
    BLE_GATT_STATUS_ATTERR_INVALID_OFFSET = 0x0107,
    ///ATT Error: Used in ATT as Insufficient Authorization.
    BLE_GATT_STATUS_ATTERR_INSUF_AUTHORIZATION = 0x0108,
    ///ATT Error: Used in ATT as Prepare Queue Full.
    BLE_GATT_STATUS_ATTERR_PREPARE_QUEUE_FULL = 0x0109,
    ///ATT Error: Used in ATT as Attribute not found.
    BLE_GATT_STATUS_ATTERR_ATTRIBUTE_NOT_FOUND = 0x010A,
    ///ATT Error: Attribute cannot be read or written using read/write blob requests.
    BLE_GATT_STATUS_ATTERR_ATTRIBUTE_NOT_LONG = 0x010B,
    ///ATT Error: Encryption key size used is insufficient.
    BLE_GATT_STATUS_ATTERR_INSUF_ENC_KEY_SIZE = 0x010C,
    ///ATT Error: Invalid value size.
    BLE_GATT_STATUS_ATTERR_INVALID_ATT_VAL_LENGTH = 0x010D,
    ///ATT Error: Very unlikely error.
    BLE_GATT_STATUS_ATTERR_UNLIKELY_ERROR = 0x010E,
    ///ATT Error: Encrypted link required.
    BLE_GATT_STATUS_ATTERR_INSUF_ENCRYPTION = 0x010F,
    ///ATT Error: Attribute type is not a supported grouping attribute.
    BLE_GATT_STATUS_ATTERR_UNSUPPORTED_GROUP_TYPE = 0x0110,
    ///ATT Error: Encrypted link required.
    BLE_GATT_STATUS_ATTERR_INSUF_RESOURCES = 0x0111,
    ///ATT Error: Reserved for Future Use range #1 begin.
    BLE_GATT_STATUS_ATTERR_RFU_RANGE1_BEGIN = 0x0112,
    ///ATT Error: Reserved for Future Use range #1 end.
    BLE_GATT_STATUS_ATTERR_RFU_RANGE1_END = 0x017F,
    ///ATT Error: Application range begin.
    BLE_GATT_STATUS_ATTERR_APP_BEGIN = 0x0180,
    ///ATT Error: Application range end.
    BLE_GATT_STATUS_ATTERR_APP_END = 0x019F,
    ///ATT Error: Reserved for Future Use range #2 begin.
    BLE_GATT_STATUS_ATTERR_RFU_RANGE2_BEGIN = 0x01A0,
    ///ATT Error: Reserved for Future Use range #2 end.
    BLE_GATT_STATUS_ATTERR_RFU_RANGE2_END = 0x01DF,
    ///ATT Error: Reserved for Future Use range #3 begin.
    BLE_GATT_STATUS_ATTERR_RFU_RANGE3_BEGIN = 0x01E0,
    ///ATT Error: Reserved for Future Use range #3 end.
    BLE_GATT_STATUS_ATTERR_RFU_RANGE3_END = 0x01FC,
    ///ATT Common Profile and Service Error: Client Characteristic Configuration Descriptor improperly configured.
    BLE_GATT_STATUS_ATTERR_CPS_CCCD_CONFIG_ERROR = 0x01FD,
    ///ATT Common Profile and Service Error: Procedure Already in Progress.
    BLE_GATT_STATUS_ATTERR_CPS_PROC_ALR_IN_PROG = 0x01FE,
    ///ATT Common Profile and Service Error: Out Of Range.
    BLE_GATT_STATUS_ATTERR_CPS_OUT_OF_RANGE = 0x01FF
}