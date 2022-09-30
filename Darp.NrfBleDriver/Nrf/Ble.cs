using System.Runtime.InteropServices;

namespace Darp.NrfBleDriver.Nrf;

public class Ble
{
    static Ble()
    {
        NativeLibrary.SetDllImportResolver(typeof(SdRpc).Assembly, InteropHelper.ImportResolver);
    }
}

/// BLE Event header
public struct ble_evt_hdr_t
{
    /// Value from a BLE_[module]_EVT series
    ushort evt_id;
    /// Length in octets including this header
    ushort evt_len;
}

public struct ble_user_mem_block_t
{
IntPtr p_mem;      /**< Pointer to the start of the user memory block. */
ushort          len;        /**< Length in bytes of the user memory block. */
}

public struct ble_evt_user_mem_release_t
{
byte                     type;       /**< User memory type, see @ref BLE_USER_MEM_TYPES. */
ble_user_mem_block_t        mem_block;  /**< User memory block */
}

/**@brief Event structure for events not associated with a specific function module. */
public struct ble_common_evt_t
{
ushort conn_handle;                                 /**< Connection Handle on which this event occurred. */

    byte      user_mem_request;    /**< User Memory Request Event Parameters. */
    ble_evt_user_mem_release_t      user_mem_release;    /**< User Memory Release Event Parameters. */
}

/// Common BLE Event type, wrapping the module specific event reports.
public struct ble_evt_t
{
    ble_evt_hdr_t header;           /**< Event header. */
    ble_common_evt_t  common_evt; /**< Common Event, evt_id in BLE_EVT_* series. */
    // ble_gap_evt_t     gap_evt;    /**< GAP originated event, evt_id in BLE_GAP_EVT_* series. */
    //ble_gattc_evt_t   gattc_evt;  /**< GATT client originated event, evt_id in BLE_GATTC_EVT* series. */
    //ble_gatts_evt_t   gatts_evt;  /**< GATT server originated event, evt_id in BLE_GATTS_EVT* series. */
    //ble_l2cap_evt_t   l2cap_evt;  /**< L2CAP originated event, evt_id in BLE_L2CAP_EVT* series. */
}