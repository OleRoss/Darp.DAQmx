namespace Darp.NrfBleDriver.Nrf;

/// Event structure for @ref BLE_GAP_EVT_CONNECTED.
/*public struct ble_gap_evt_connected_t
{
ble_gap_addr_t        peer_addr;              /// Bluetooth address of the peer device. If the peer_addr resolved: @ref ble_gap_addr_t::addr_id_peer is set to 1
                                                     and the address is the device's identity address.
uint8_t               role;                   /// BLE role for this connection, see @ref BLE_GAP_ROLES
ble_gap_conn_params_t conn_params;            /// GAP Connection Parameters.
}

/**@brief GAP event structure. 
public struct ble_gap_evt_t
{
  ushort conn_handle;                                     /// Connection Handle on which event occurred. 

    ble_gap_evt_connected_t                   connected;                    /// Connected Event Parameters. 
    ble_gap_evt_disconnected_t                disconnected;                 /// Disconnected Event Parameters. 
    ble_gap_evt_conn_param_update_t           conn_param_update;            /// Connection Parameter Update Parameters. 
    ble_gap_evt_sec_params_request_t          sec_params_request;           /// Security Parameters Request Event Parameters. 
    ble_gap_evt_sec_info_request_t            sec_info_request;             /// Security Information Request Event Parameters. 
    ble_gap_evt_passkey_display_t             passkey_display;              /// Passkey Display Event Parameters. 
    ble_gap_evt_key_pressed_t                 key_pressed;                  /// Key Pressed Event Parameters. 
    ble_gap_evt_auth_key_request_t            auth_key_request;             /// Authentication Key Request Event Parameters. 
    ble_gap_evt_lesc_dhkey_request_t          lesc_dhkey_request;           /// LE Secure Connections DHKey calculation request. 
    ble_gap_evt_auth_status_t                 auth_status;                  /// Authentication Status Event Parameters. 
    ble_gap_evt_conn_sec_update_t             conn_sec_update;              /// Connection Security Update Event Parameters. 
    ble_gap_evt_timeout_t                     timeout;                      /// Timeout Event Parameters. 
    ble_gap_evt_rssi_changed_t                rssi_changed;                 /// RSSI Event Parameters. 
    ble_gap_evt_adv_report_t                  adv_report;                   /// Advertising Report Event Parameters. 
    ble_gap_evt_sec_request_t                 sec_request;                  /// Security Request Event Parameters. 
    ble_gap_evt_conn_param_update_request_t   conn_param_update_request;    /// Connection Parameter Update Parameters. 
    ble_gap_evt_scan_req_report_t             scan_req_report;              /// Scan Request Report Parameters. 
    ble_gap_evt_phy_update_request_t          phy_update_request;           /// PHY Update Request Event Parameters. 
    ble_gap_evt_phy_update_t                  phy_update;                   /// PHY Update Parameters. 
    ble_gap_evt_data_length_update_request_t  data_length_update_request;   /// Data Length Update Request Event Parameters. 
    ble_gap_evt_data_length_update_t          data_length_update;           /// Data Length Update Event Parameters. 
}*/