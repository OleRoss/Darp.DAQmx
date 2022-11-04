#include <adapter.h>
#include <ble.h>
#include <ble_err.h>
#include <ble_gap.h>
#include <ble_gatt.h>
#include <ble_gattc.h>
#include <ble_gatts.h>
#include <ble_hci.h>
#include <ble_l2cap.h>
#include <ble_ranges.h>
#include <ble_types.h>
#include <nrf.h>
#include <nrf_error.h>
#include <nrf_svc.h>
#include <platform.h>
#include <sd_rpc.h>
#include <sd_rpc_types.h>
#include <new>

transport_layer_t& (transport_layer_t::*_0)(transport_layer_t&&) = &transport_layer_t::operator=;
data_link_layer_t& (data_link_layer_t::*_1)(data_link_layer_t&&) = &data_link_layer_t::operator=;
adapter_t& (adapter_t::*_2)(adapter_t&&) = &adapter_t::operator=;
physical_layer_t& (physical_layer_t::*_3)(physical_layer_t&&) = &physical_layer_t::operator=;
ble_user_mem_block_t& (ble_user_mem_block_t::*_4)(ble_user_mem_block_t&&) = &ble_user_mem_block_t::operator=;
ble_evt_user_mem_request_t& (ble_evt_user_mem_request_t::*_5)(ble_evt_user_mem_request_t&&) = &ble_evt_user_mem_request_t::operator=;
ble_evt_user_mem_release_t& (ble_evt_user_mem_release_t::*_6)(ble_evt_user_mem_release_t&&) = &ble_evt_user_mem_release_t::operator=;
ble_common_evt_t& (ble_common_evt_t::*_7)(ble_common_evt_t&&) = &ble_common_evt_t::operator=;
ble_evt_hdr_t& (ble_evt_hdr_t::*_8)(ble_evt_hdr_t&&) = &ble_evt_hdr_t::operator=;
ble_evt_t& (ble_evt_t::*_9)(ble_evt_t&&) = &ble_evt_t::operator=;
ble_version_t& (ble_version_t::*_10)(ble_version_t&&) = &ble_version_t::operator=;
ble_pa_lna_cfg_t& (ble_pa_lna_cfg_t::*_11)(ble_pa_lna_cfg_t&&) = &ble_pa_lna_cfg_t::operator=;
ble_common_opt_pa_lna_t& (ble_common_opt_pa_lna_t::*_12)(ble_common_opt_pa_lna_t&&) = &ble_common_opt_pa_lna_t::operator=;
ble_common_opt_conn_evt_ext_t& (ble_common_opt_conn_evt_ext_t::*_13)(ble_common_opt_conn_evt_ext_t&&) = &ble_common_opt_conn_evt_ext_t::operator=;
ble_common_opt_t& (ble_common_opt_t::*_14)(ble_common_opt_t&&) = &ble_common_opt_t::operator=;
ble_opt_t& (ble_opt_t::*_15)(ble_opt_t&&) = &ble_opt_t::operator=;
ble_conn_cfg_t& (ble_conn_cfg_t::*_16)(ble_conn_cfg_t&&) = &ble_conn_cfg_t::operator=;
ble_common_cfg_vs_uuid_t& (ble_common_cfg_vs_uuid_t::*_17)(ble_common_cfg_vs_uuid_t&&) = &ble_common_cfg_vs_uuid_t::operator=;
ble_common_cfg_t& (ble_common_cfg_t::*_18)(ble_common_cfg_t&&) = &ble_common_cfg_t::operator=;
ble_cfg_t& (ble_cfg_t::*_19)(ble_cfg_t&&) = &ble_cfg_t::operator=;
ble_uuid128_t& (ble_uuid128_t::*_20)(ble_uuid128_t&&) = &ble_uuid128_t::operator=;
ble_uuid_t& (ble_uuid_t::*_21)(ble_uuid_t&&) = &ble_uuid_t::operator=;
ble_data_t& (ble_data_t::*_22)(ble_data_t&&) = &ble_data_t::operator=;
ble_gap_addr_t& (ble_gap_addr_t::*_23)(ble_gap_addr_t&&) = &ble_gap_addr_t::operator=;
ble_gap_conn_params_t& (ble_gap_conn_params_t::*_24)(ble_gap_conn_params_t&&) = &ble_gap_conn_params_t::operator=;
ble_gap_conn_sec_mode_t& (ble_gap_conn_sec_mode_t::*_25)(ble_gap_conn_sec_mode_t&&) = &ble_gap_conn_sec_mode_t::operator=;
ble_gap_conn_sec_t& (ble_gap_conn_sec_t::*_26)(ble_gap_conn_sec_t&&) = &ble_gap_conn_sec_t::operator=;
ble_gap_irk_t& (ble_gap_irk_t::*_27)(ble_gap_irk_t&&) = &ble_gap_irk_t::operator=;
ble_gap_adv_ch_mask_t& (ble_gap_adv_ch_mask_t::*_28)(ble_gap_adv_ch_mask_t&&) = &ble_gap_adv_ch_mask_t::operator=;
ble_gap_adv_params_t& (ble_gap_adv_params_t::*_29)(ble_gap_adv_params_t&&) = &ble_gap_adv_params_t::operator=;
ble_gap_scan_params_t& (ble_gap_scan_params_t::*_30)(ble_gap_scan_params_t&&) = &ble_gap_scan_params_t::operator=;
ble_gap_privacy_params_t& (ble_gap_privacy_params_t::*_31)(ble_gap_privacy_params_t&&) = &ble_gap_privacy_params_t::operator=;
ble_gap_phys_t& (ble_gap_phys_t::*_32)(ble_gap_phys_t&&) = &ble_gap_phys_t::operator=;
ble_gap_sec_kdist_t& (ble_gap_sec_kdist_t::*_33)(ble_gap_sec_kdist_t&&) = &ble_gap_sec_kdist_t::operator=;
ble_gap_sec_params_t& (ble_gap_sec_params_t::*_34)(ble_gap_sec_params_t&&) = &ble_gap_sec_params_t::operator=;
ble_gap_enc_info_t& (ble_gap_enc_info_t::*_35)(ble_gap_enc_info_t&&) = &ble_gap_enc_info_t::operator=;
ble_gap_master_id_t& (ble_gap_master_id_t::*_36)(ble_gap_master_id_t&&) = &ble_gap_master_id_t::operator=;
ble_gap_sign_info_t& (ble_gap_sign_info_t::*_37)(ble_gap_sign_info_t&&) = &ble_gap_sign_info_t::operator=;
ble_gap_lesc_p256_pk_t& (ble_gap_lesc_p256_pk_t::*_38)(ble_gap_lesc_p256_pk_t&&) = &ble_gap_lesc_p256_pk_t::operator=;
ble_gap_lesc_dhkey_t& (ble_gap_lesc_dhkey_t::*_39)(ble_gap_lesc_dhkey_t&&) = &ble_gap_lesc_dhkey_t::operator=;
ble_gap_lesc_oob_data_t& (ble_gap_lesc_oob_data_t::*_40)(ble_gap_lesc_oob_data_t&&) = &ble_gap_lesc_oob_data_t::operator=;
ble_gap_evt_connected_t& (ble_gap_evt_connected_t::*_41)(ble_gap_evt_connected_t&&) = &ble_gap_evt_connected_t::operator=;
ble_gap_evt_disconnected_t& (ble_gap_evt_disconnected_t::*_42)(ble_gap_evt_disconnected_t&&) = &ble_gap_evt_disconnected_t::operator=;
ble_gap_evt_conn_param_update_t& (ble_gap_evt_conn_param_update_t::*_43)(ble_gap_evt_conn_param_update_t&&) = &ble_gap_evt_conn_param_update_t::operator=;
ble_gap_evt_phy_update_request_t& (ble_gap_evt_phy_update_request_t::*_44)(ble_gap_evt_phy_update_request_t&&) = &ble_gap_evt_phy_update_request_t::operator=;
ble_gap_evt_phy_update_t& (ble_gap_evt_phy_update_t::*_45)(ble_gap_evt_phy_update_t&&) = &ble_gap_evt_phy_update_t::operator=;
ble_gap_evt_sec_params_request_t& (ble_gap_evt_sec_params_request_t::*_46)(ble_gap_evt_sec_params_request_t&&) = &ble_gap_evt_sec_params_request_t::operator=;
ble_gap_evt_sec_info_request_t& (ble_gap_evt_sec_info_request_t::*_47)(ble_gap_evt_sec_info_request_t&&) = &ble_gap_evt_sec_info_request_t::operator=;
ble_gap_evt_passkey_display_t& (ble_gap_evt_passkey_display_t::*_48)(ble_gap_evt_passkey_display_t&&) = &ble_gap_evt_passkey_display_t::operator=;
ble_gap_evt_key_pressed_t& (ble_gap_evt_key_pressed_t::*_49)(ble_gap_evt_key_pressed_t&&) = &ble_gap_evt_key_pressed_t::operator=;
ble_gap_evt_auth_key_request_t& (ble_gap_evt_auth_key_request_t::*_50)(ble_gap_evt_auth_key_request_t&&) = &ble_gap_evt_auth_key_request_t::operator=;
ble_gap_evt_lesc_dhkey_request_t& (ble_gap_evt_lesc_dhkey_request_t::*_51)(ble_gap_evt_lesc_dhkey_request_t&&) = &ble_gap_evt_lesc_dhkey_request_t::operator=;
ble_gap_sec_levels_t& (ble_gap_sec_levels_t::*_52)(ble_gap_sec_levels_t&&) = &ble_gap_sec_levels_t::operator=;
ble_gap_enc_key_t& (ble_gap_enc_key_t::*_53)(ble_gap_enc_key_t&&) = &ble_gap_enc_key_t::operator=;
ble_gap_id_key_t& (ble_gap_id_key_t::*_54)(ble_gap_id_key_t&&) = &ble_gap_id_key_t::operator=;
ble_gap_sec_keys_t& (ble_gap_sec_keys_t::*_55)(ble_gap_sec_keys_t&&) = &ble_gap_sec_keys_t::operator=;
ble_gap_sec_keyset_t& (ble_gap_sec_keyset_t::*_56)(ble_gap_sec_keyset_t&&) = &ble_gap_sec_keyset_t::operator=;
ble_gap_data_length_params_t& (ble_gap_data_length_params_t::*_57)(ble_gap_data_length_params_t&&) = &ble_gap_data_length_params_t::operator=;
ble_gap_data_length_limitation_t& (ble_gap_data_length_limitation_t::*_58)(ble_gap_data_length_limitation_t&&) = &ble_gap_data_length_limitation_t::operator=;
ble_gap_evt_auth_status_t& (ble_gap_evt_auth_status_t::*_59)(ble_gap_evt_auth_status_t&&) = &ble_gap_evt_auth_status_t::operator=;
ble_gap_evt_conn_sec_update_t& (ble_gap_evt_conn_sec_update_t::*_60)(ble_gap_evt_conn_sec_update_t&&) = &ble_gap_evt_conn_sec_update_t::operator=;
ble_gap_evt_timeout_t& (ble_gap_evt_timeout_t::*_61)(ble_gap_evt_timeout_t&&) = &ble_gap_evt_timeout_t::operator=;
ble_gap_evt_rssi_changed_t& (ble_gap_evt_rssi_changed_t::*_62)(ble_gap_evt_rssi_changed_t&&) = &ble_gap_evt_rssi_changed_t::operator=;
ble_gap_evt_adv_report_t& (ble_gap_evt_adv_report_t::*_63)(ble_gap_evt_adv_report_t&&) = &ble_gap_evt_adv_report_t::operator=;
ble_gap_evt_sec_request_t& (ble_gap_evt_sec_request_t::*_64)(ble_gap_evt_sec_request_t&&) = &ble_gap_evt_sec_request_t::operator=;
ble_gap_evt_conn_param_update_request_t& (ble_gap_evt_conn_param_update_request_t::*_65)(ble_gap_evt_conn_param_update_request_t&&) = &ble_gap_evt_conn_param_update_request_t::operator=;
ble_gap_evt_scan_req_report_t& (ble_gap_evt_scan_req_report_t::*_66)(ble_gap_evt_scan_req_report_t&&) = &ble_gap_evt_scan_req_report_t::operator=;
ble_gap_evt_data_length_update_request_t& (ble_gap_evt_data_length_update_request_t::*_67)(ble_gap_evt_data_length_update_request_t&&) = &ble_gap_evt_data_length_update_request_t::operator=;
ble_gap_evt_data_length_update_t& (ble_gap_evt_data_length_update_t::*_68)(ble_gap_evt_data_length_update_t&&) = &ble_gap_evt_data_length_update_t::operator=;
ble_gap_evt_t& (ble_gap_evt_t::*_69)(ble_gap_evt_t&&) = &ble_gap_evt_t::operator=;
ble_gap_conn_cfg_t& (ble_gap_conn_cfg_t::*_70)(ble_gap_conn_cfg_t&&) = &ble_gap_conn_cfg_t::operator=;
ble_gap_cfg_role_count_t& (ble_gap_cfg_role_count_t::*_71)(ble_gap_cfg_role_count_t&&) = &ble_gap_cfg_role_count_t::operator=;
ble_gap_cfg_device_name_t& (ble_gap_cfg_device_name_t::*_72)(ble_gap_cfg_device_name_t&&) = &ble_gap_cfg_device_name_t::operator=;
ble_gap_cfg_t& (ble_gap_cfg_t::*_73)(ble_gap_cfg_t&&) = &ble_gap_cfg_t::operator=;
ble_gap_opt_ch_map_t& (ble_gap_opt_ch_map_t::*_74)(ble_gap_opt_ch_map_t&&) = &ble_gap_opt_ch_map_t::operator=;
ble_gap_opt_local_conn_latency_t& (ble_gap_opt_local_conn_latency_t::*_75)(ble_gap_opt_local_conn_latency_t&&) = &ble_gap_opt_local_conn_latency_t::operator=;
ble_gap_opt_slave_latency_disable_t& (ble_gap_opt_slave_latency_disable_t::*_76)(ble_gap_opt_slave_latency_disable_t&&) = &ble_gap_opt_slave_latency_disable_t::operator=;
ble_gap_opt_passkey_t& (ble_gap_opt_passkey_t::*_77)(ble_gap_opt_passkey_t&&) = &ble_gap_opt_passkey_t::operator=;
ble_gap_opt_scan_req_report_t& (ble_gap_opt_scan_req_report_t::*_78)(ble_gap_opt_scan_req_report_t&&) = &ble_gap_opt_scan_req_report_t::operator=;
ble_gap_opt_compat_mode_1_t& (ble_gap_opt_compat_mode_1_t::*_79)(ble_gap_opt_compat_mode_1_t&&) = &ble_gap_opt_compat_mode_1_t::operator=;
ble_gap_opt_auth_payload_timeout_t& (ble_gap_opt_auth_payload_timeout_t::*_80)(ble_gap_opt_auth_payload_timeout_t&&) = &ble_gap_opt_auth_payload_timeout_t::operator=;
ble_gap_opt_t& (ble_gap_opt_t::*_81)(ble_gap_opt_t&&) = &ble_gap_opt_t::operator=;
ble_l2cap_conn_cfg_t& (ble_l2cap_conn_cfg_t::*_82)(ble_l2cap_conn_cfg_t&&) = &ble_l2cap_conn_cfg_t::operator=;
ble_l2cap_ch_rx_params_t& (ble_l2cap_ch_rx_params_t::*_83)(ble_l2cap_ch_rx_params_t&&) = &ble_l2cap_ch_rx_params_t::operator=;
ble_l2cap_ch_setup_params_t& (ble_l2cap_ch_setup_params_t::*_84)(ble_l2cap_ch_setup_params_t&&) = &ble_l2cap_ch_setup_params_t::operator=;
ble_l2cap_ch_tx_params_t& (ble_l2cap_ch_tx_params_t::*_85)(ble_l2cap_ch_tx_params_t&&) = &ble_l2cap_ch_tx_params_t::operator=;
ble_l2cap_evt_ch_setup_request_t& (ble_l2cap_evt_ch_setup_request_t::*_86)(ble_l2cap_evt_ch_setup_request_t&&) = &ble_l2cap_evt_ch_setup_request_t::operator=;
ble_l2cap_evt_ch_setup_refused_t& (ble_l2cap_evt_ch_setup_refused_t::*_87)(ble_l2cap_evt_ch_setup_refused_t&&) = &ble_l2cap_evt_ch_setup_refused_t::operator=;
ble_l2cap_evt_ch_setup_t& (ble_l2cap_evt_ch_setup_t::*_88)(ble_l2cap_evt_ch_setup_t&&) = &ble_l2cap_evt_ch_setup_t::operator=;
ble_l2cap_evt_ch_sdu_buf_released_t& (ble_l2cap_evt_ch_sdu_buf_released_t::*_89)(ble_l2cap_evt_ch_sdu_buf_released_t&&) = &ble_l2cap_evt_ch_sdu_buf_released_t::operator=;
ble_l2cap_evt_ch_credit_t& (ble_l2cap_evt_ch_credit_t::*_90)(ble_l2cap_evt_ch_credit_t&&) = &ble_l2cap_evt_ch_credit_t::operator=;
ble_l2cap_evt_ch_rx_t& (ble_l2cap_evt_ch_rx_t::*_91)(ble_l2cap_evt_ch_rx_t&&) = &ble_l2cap_evt_ch_rx_t::operator=;
ble_l2cap_evt_ch_tx_t& (ble_l2cap_evt_ch_tx_t::*_92)(ble_l2cap_evt_ch_tx_t&&) = &ble_l2cap_evt_ch_tx_t::operator=;
ble_l2cap_evt_t& (ble_l2cap_evt_t::*_93)(ble_l2cap_evt_t&&) = &ble_l2cap_evt_t::operator=;
ble_gatt_conn_cfg_t& (ble_gatt_conn_cfg_t::*_94)(ble_gatt_conn_cfg_t&&) = &ble_gatt_conn_cfg_t::operator=;
ble_gatt_char_props_t& (ble_gatt_char_props_t::*_95)(ble_gatt_char_props_t&&) = &ble_gatt_char_props_t::operator=;
ble_gatt_char_ext_props_t& (ble_gatt_char_ext_props_t::*_96)(ble_gatt_char_ext_props_t&&) = &ble_gatt_char_ext_props_t::operator=;
ble_gattc_conn_cfg_t& (ble_gattc_conn_cfg_t::*_97)(ble_gattc_conn_cfg_t&&) = &ble_gattc_conn_cfg_t::operator=;
ble_gattc_handle_range_t& (ble_gattc_handle_range_t::*_98)(ble_gattc_handle_range_t&&) = &ble_gattc_handle_range_t::operator=;
ble_gattc_service_t& (ble_gattc_service_t::*_99)(ble_gattc_service_t&&) = &ble_gattc_service_t::operator=;
ble_gattc_include_t& (ble_gattc_include_t::*_100)(ble_gattc_include_t&&) = &ble_gattc_include_t::operator=;
ble_gattc_char_t& (ble_gattc_char_t::*_101)(ble_gattc_char_t&&) = &ble_gattc_char_t::operator=;
ble_gattc_desc_t& (ble_gattc_desc_t::*_102)(ble_gattc_desc_t&&) = &ble_gattc_desc_t::operator=;
ble_gattc_write_params_t& (ble_gattc_write_params_t::*_103)(ble_gattc_write_params_t&&) = &ble_gattc_write_params_t::operator=;
ble_gattc_attr_info16_t& (ble_gattc_attr_info16_t::*_104)(ble_gattc_attr_info16_t&&) = &ble_gattc_attr_info16_t::operator=;
ble_gattc_attr_info128_t& (ble_gattc_attr_info128_t::*_105)(ble_gattc_attr_info128_t&&) = &ble_gattc_attr_info128_t::operator=;
ble_gattc_evt_prim_srvc_disc_rsp_t& (ble_gattc_evt_prim_srvc_disc_rsp_t::*_106)(ble_gattc_evt_prim_srvc_disc_rsp_t&&) = &ble_gattc_evt_prim_srvc_disc_rsp_t::operator=;
ble_gattc_evt_rel_disc_rsp_t& (ble_gattc_evt_rel_disc_rsp_t::*_107)(ble_gattc_evt_rel_disc_rsp_t&&) = &ble_gattc_evt_rel_disc_rsp_t::operator=;
ble_gattc_evt_char_disc_rsp_t& (ble_gattc_evt_char_disc_rsp_t::*_108)(ble_gattc_evt_char_disc_rsp_t&&) = &ble_gattc_evt_char_disc_rsp_t::operator=;
ble_gattc_evt_desc_disc_rsp_t& (ble_gattc_evt_desc_disc_rsp_t::*_109)(ble_gattc_evt_desc_disc_rsp_t&&) = &ble_gattc_evt_desc_disc_rsp_t::operator=;
ble_gattc_evt_attr_info_disc_rsp_t& (ble_gattc_evt_attr_info_disc_rsp_t::*_110)(ble_gattc_evt_attr_info_disc_rsp_t&&) = &ble_gattc_evt_attr_info_disc_rsp_t::operator=;
ble_gattc_handle_value_t& (ble_gattc_handle_value_t::*_111)(ble_gattc_handle_value_t&&) = &ble_gattc_handle_value_t::operator=;
ble_gattc_evt_char_val_by_uuid_read_rsp_t& (ble_gattc_evt_char_val_by_uuid_read_rsp_t::*_112)(ble_gattc_evt_char_val_by_uuid_read_rsp_t&&) = &ble_gattc_evt_char_val_by_uuid_read_rsp_t::operator=;
ble_gattc_evt_read_rsp_t& (ble_gattc_evt_read_rsp_t::*_113)(ble_gattc_evt_read_rsp_t&&) = &ble_gattc_evt_read_rsp_t::operator=;
ble_gattc_evt_char_vals_read_rsp_t& (ble_gattc_evt_char_vals_read_rsp_t::*_114)(ble_gattc_evt_char_vals_read_rsp_t&&) = &ble_gattc_evt_char_vals_read_rsp_t::operator=;
ble_gattc_evt_write_rsp_t& (ble_gattc_evt_write_rsp_t::*_115)(ble_gattc_evt_write_rsp_t&&) = &ble_gattc_evt_write_rsp_t::operator=;
ble_gattc_evt_hvx_t& (ble_gattc_evt_hvx_t::*_116)(ble_gattc_evt_hvx_t&&) = &ble_gattc_evt_hvx_t::operator=;
ble_gattc_evt_exchange_mtu_rsp_t& (ble_gattc_evt_exchange_mtu_rsp_t::*_117)(ble_gattc_evt_exchange_mtu_rsp_t&&) = &ble_gattc_evt_exchange_mtu_rsp_t::operator=;
ble_gattc_evt_timeout_t& (ble_gattc_evt_timeout_t::*_118)(ble_gattc_evt_timeout_t&&) = &ble_gattc_evt_timeout_t::operator=;
ble_gattc_evt_write_cmd_tx_complete_t& (ble_gattc_evt_write_cmd_tx_complete_t::*_119)(ble_gattc_evt_write_cmd_tx_complete_t&&) = &ble_gattc_evt_write_cmd_tx_complete_t::operator=;
ble_gattc_evt_t& (ble_gattc_evt_t::*_120)(ble_gattc_evt_t&&) = &ble_gattc_evt_t::operator=;
unsigned int (*_121)(ble_gattc_evt_t*, ble_gattc_handle_value_t*) = &::sd_ble_gattc_evt_char_val_by_uuid_read_rsp_iter;
ble_gatts_conn_cfg_t& (ble_gatts_conn_cfg_t::*_122)(ble_gatts_conn_cfg_t&&) = &ble_gatts_conn_cfg_t::operator=;
ble_gatts_attr_md_t& (ble_gatts_attr_md_t::*_123)(ble_gatts_attr_md_t&&) = &ble_gatts_attr_md_t::operator=;
ble_gatts_attr_t& (ble_gatts_attr_t::*_124)(ble_gatts_attr_t&&) = &ble_gatts_attr_t::operator=;
ble_gatts_value_t& (ble_gatts_value_t::*_125)(ble_gatts_value_t&&) = &ble_gatts_value_t::operator=;
ble_gatts_char_pf_t& (ble_gatts_char_pf_t::*_126)(ble_gatts_char_pf_t&&) = &ble_gatts_char_pf_t::operator=;
ble_gatts_char_md_t& (ble_gatts_char_md_t::*_127)(ble_gatts_char_md_t&&) = &ble_gatts_char_md_t::operator=;
ble_gatts_char_handles_t& (ble_gatts_char_handles_t::*_128)(ble_gatts_char_handles_t&&) = &ble_gatts_char_handles_t::operator=;
ble_gatts_hvx_params_t& (ble_gatts_hvx_params_t::*_129)(ble_gatts_hvx_params_t&&) = &ble_gatts_hvx_params_t::operator=;
ble_gatts_authorize_params_t& (ble_gatts_authorize_params_t::*_130)(ble_gatts_authorize_params_t&&) = &ble_gatts_authorize_params_t::operator=;
ble_gatts_rw_authorize_reply_params_t& (ble_gatts_rw_authorize_reply_params_t::*_131)(ble_gatts_rw_authorize_reply_params_t&&) = &ble_gatts_rw_authorize_reply_params_t::operator=;
ble_gatts_cfg_service_changed_t& (ble_gatts_cfg_service_changed_t::*_132)(ble_gatts_cfg_service_changed_t&&) = &ble_gatts_cfg_service_changed_t::operator=;
ble_gatts_cfg_attr_tab_size_t& (ble_gatts_cfg_attr_tab_size_t::*_133)(ble_gatts_cfg_attr_tab_size_t&&) = &ble_gatts_cfg_attr_tab_size_t::operator=;
ble_gatts_cfg_t& (ble_gatts_cfg_t::*_134)(ble_gatts_cfg_t&&) = &ble_gatts_cfg_t::operator=;
ble_gatts_evt_write_t& (ble_gatts_evt_write_t::*_135)(ble_gatts_evt_write_t&&) = &ble_gatts_evt_write_t::operator=;
ble_gatts_evt_read_t& (ble_gatts_evt_read_t::*_136)(ble_gatts_evt_read_t&&) = &ble_gatts_evt_read_t::operator=;
ble_gatts_evt_rw_authorize_request_t& (ble_gatts_evt_rw_authorize_request_t::*_137)(ble_gatts_evt_rw_authorize_request_t&&) = &ble_gatts_evt_rw_authorize_request_t::operator=;
ble_gatts_evt_sys_attr_missing_t& (ble_gatts_evt_sys_attr_missing_t::*_138)(ble_gatts_evt_sys_attr_missing_t&&) = &ble_gatts_evt_sys_attr_missing_t::operator=;
ble_gatts_evt_hvc_t& (ble_gatts_evt_hvc_t::*_139)(ble_gatts_evt_hvc_t&&) = &ble_gatts_evt_hvc_t::operator=;
ble_gatts_evt_exchange_mtu_request_t& (ble_gatts_evt_exchange_mtu_request_t::*_140)(ble_gatts_evt_exchange_mtu_request_t&&) = &ble_gatts_evt_exchange_mtu_request_t::operator=;
ble_gatts_evt_timeout_t& (ble_gatts_evt_timeout_t::*_141)(ble_gatts_evt_timeout_t&&) = &ble_gatts_evt_timeout_t::operator=;
ble_gatts_evt_hvn_tx_complete_t& (ble_gatts_evt_hvn_tx_complete_t::*_142)(ble_gatts_evt_hvn_tx_complete_t&&) = &ble_gatts_evt_hvn_tx_complete_t::operator=;
ble_gatts_evt_t& (ble_gatts_evt_t::*_143)(ble_gatts_evt_t&&) = &ble_gatts_evt_t::operator=;
sd_rpc_serial_port_desc_t& (sd_rpc_serial_port_desc_t::*_144)(sd_rpc_serial_port_desc_t&&) = &sd_rpc_serial_port_desc_t::operator=;
