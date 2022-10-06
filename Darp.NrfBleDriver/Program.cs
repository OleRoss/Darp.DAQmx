﻿// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using NrfBleDriver;

IntPtr libHandle = IntPtr.Zero;
const byte ConfigId = 1;

NativeLibrary.SetDllImportResolver(typeof(Program).Assembly, ImportResolver);

IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
{
    if (libraryName != "NrfBleDriver" || libHandle != IntPtr.Zero)
        return libHandle;
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        NativeLibrary.TryLoad(@"C:\Users\VakuO\RiderProjects\NiTests\Darp.NrfBleDriver\Nrf\NrfBleDriver.dll", assembly, new DllImportSearchPath?(), out libHandle);
    return libHandle;
}

static AdapterT AdapterInit(string serialPort, uint baudRate)
{
    PhysicalLayerT phy;
    DataLinkLayerT data_link_layer;
    TransportLayerT transport_layer;

    phy = sd_rpc.SdRpcPhysicalLayerCreateUart(serialPort,
        baudRate,
        SdRpcFlowControlT.SD_RPC_FLOW_CONTROL_NONE,
        SdRpcParityT.SD_RPC_PARITY_NONE);
    data_link_layer = sd_rpc.SdRpcDataLinkLayerCreateBtThreeWire(phy, 250);
    transport_layer = sd_rpc.SdRpcTransportLayerCreate(data_link_layer, 1500);
    return sd_rpc.SdRpcAdapterCreate(transport_layer);
}

void status_handler(IntPtr adapter, SdRpcAppStatusT code, string message)
{
    Console.WriteLine($"Status: {code}, message: {message}");
}

ushort m_connection_handle = BleConnHandles.BLE_CONN_HANDLE_INVALID;
bool m_advertisement_timed_out;
bool m_send_notifications;

void ble_evt_dispatch(IntPtr adapter, IntPtr p_ble_evt)
{
    var ble_evt = BleEvtT.__GetOrCreateInstance(p_ble_evt);

    uint err_code;
    BleGapDataLengthParamsT dl_params;

    if (ble_evt == null)
    {
        Console.WriteLine("Received an empty BLE event");
        return;
    }

    switch (ble_evt.Header.EvtId)
    {
        case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_CONNECTED:
            m_connection_handle = ble_evt.evt.GapEvt.ConnHandle;
            Console.WriteLine($"Connected, connection handle 0x{m_connection_handle}");
            break;

        case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_DISCONNECTED:
            Console.WriteLine("Disconnected");
            m_connection_handle = BleConnHandles.BLE_CONN_HANDLE_INVALID;
            m_send_notifications = false;
            // advertising_start();
            break;

        case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_TIMEOUT:
            Console.WriteLine("Advertisement timed out");
            m_advertisement_timed_out = true;
            break;
        case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_ADV_REPORT:
            Console.WriteLine("Received Advertisement");
            break;
        /*case (ushort)BLE_GATTS_EVTS.BLE_GATTS_EVT_SYS_ATTR_MISSING:
            err_code = sd_ble_gatts_sys_attr_set(adapter, m_connection_handle, NULL, 0, 0);

            if (err_code != )
            {
                printf("Failed updating persistent sys attr info. Error code: 0x%02X\n", err_code);
                fflush(stdout);
            }
            break;*/

            /*case (ushort)BLE_GATTS_EVTS.BLE_GATTS_EVT_WRITE:
                if (p_ble_evt->evt.gatts_evt.params.write.handle ==
                        m_heart_rate_measurement_handle.cccd_handle)
                {
                    uint8_t write_data = p_ble_evt->evt.gatts_evt.params.write.data[0];
                    m_send_notifications = write_data == BLE_GATT_HVX_NOTIFICATION;
                }
            break;*/

            /*case (ushort)BLE_GATTS_EVTS.BLE_GATTS_EVT_EXCHANGE_MTU_REQUEST:
                err_code = sd_ble_gatts_exchange_mtu_reply(adapter, m_connection_handle,
                                                           GATT_MTU_SIZE_DEFAULT);
    
                if (err_code != NRF_SUCCESS)
                {
                    printf("MTU exchange request reply failed. Error code: 0x%02X\n", err_code);
                    fflush(stdout);
                }
            break;*/


        case (ushort)BLE_GATTS_EVTS.BLE_GATTS_EVT_HVN_TX_COMPLETE:
            Console.WriteLine("Successfully transmitted a heart rate reading.");
            break;

    case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_DATA_LENGTH_UPDATE_REQUEST:
            Console.WriteLine("Received BLE_GAP_EVT_DATA_LENGTH_UPDATE_REQUEST.\n");

            break;

    case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_DATA_LENGTH_UPDATE:
            Console.WriteLine("Received BLE_GAP_EVT_DATA_LENGTH_UPDATE " +
                                $"(max_rx_octets:{ble_evt.evt.GapEvt.@params.DataLengthUpdate.EffectiveParams.MaxRxOctets}, " +
                                $"max_rx_time_us:{ble_evt.evt.GapEvt.@params.DataLengthUpdate.EffectiveParams.MaxRxTimeUs}, " +
                                $"max_tx_octets:{ble_evt.evt.GapEvt.@params.DataLengthUpdate.EffectiveParams.MaxTxOctets}, " +
                                $"max_tx_time_us:{ble_evt.evt.GapEvt.@params.DataLengthUpdate.EffectiveParams.MaxTxTimeUs}");
            break;

    case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_PHY_UPDATE_REQUEST:
            Console.WriteLine("Received BLE_GAP_EVT_PHY_UPDATE_REQUEST.");

            /*var phys = new BleGapPhysT
            {
                RxPhys = BLE_GAP_PHYS.BLE_GAP_PHY_AUTO,
                TxPhys = BLE_GAP_PHYS.BLE_GAP_PHY_AUTO,
            };
            err_code = sd_ble_gap_phy_update(adapter, p_ble_evt->evt.gap_evt.conn_handle, &phys);           */
            break;

    case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_PHY_UPDATE:
            Console.WriteLine("Received BLE_GAP_EVT_PHY_UPDATE " +
                              $"(RX:{ble_evt.evt.GapEvt.@params.PhyUpdate.RxPhy}, " +
                              $"TX:{ble_evt.evt.GapEvt.@params.PhyUpdate.TxPhy}, " +
                              $"status:{ble_evt.evt.GapEvt.@params.PhyUpdate.Status})");
            break;

        default:
            Console.WriteLine($"Received an un-handled event with ID: {ble_evt.Header.EvtId}");
            break;
        }
}

static void log_handler(IntPtr adapter, SdRpcLogSeverityT severity, string message)
{
    switch (severity)
    {
        case SdRpcLogSeverityT.SD_RPC_LOG_ERROR:
            Console.WriteLine($"Error: {message}");
            break;

        case SdRpcLogSeverityT.SD_RPC_LOG_WARNING:
            Console.WriteLine($"Warning: {message}");
            break;

        case SdRpcLogSeverityT.SD_RPC_LOG_INFO:
            Console.WriteLine($"Info: {message}");
            break;

        default:
            Console.WriteLine($"Log: {message}");
            break;
    }
}

uint ble_cfg_set(AdapterT adapter, byte conn_cfg_tag)
{
    const uint ram_start = 0; // Value is not used by ble-driver
    uint error_code;

    // Configure the connection roles.
    var bleCfg = default(BleCfgT);
    bleCfg.GapCfg.RoleCountCfg = new BleGapCfgRoleCountT
    {
        PeriphRoleCount = 0,
        CentralRoleCount = 1,
        CentralSecCount  = 0
    };

    error_code = ble.SdBleCfgSet(adapter, (uint)BLE_GAP_CFGS.BLE_GAP_CFG_ROLE_COUNT, bleCfg, ram_start);
    if (error_code != NrfError.NRF_SUCCESS)
    {
        Console.WriteLine($"sd_ble_cfg_set() failed when attempting to set BLE_GAP_CFG_ROLE_COUNT. Error code: 0x{error_code:X}");
        return error_code;
    }

    bleCfg = default;
    bleCfg.ConnCfg = new BleConnCfgT
    {
        ConnCfgTag = conn_cfg_tag,
        @params = new BleConnCfgT.Params
        {
            GattConnCfg = new BleGattConnCfgT
            {
                AttMtu = 150
            }
        }
    };

    error_code = ble.SdBleCfgSet(adapter, (uint)BLE_CONN_CFGS.BLE_CONN_CFG_GATT, bleCfg, ram_start);
    if (error_code != NrfError.NRF_SUCCESS)
    {
        Console.WriteLine($"sd_ble_cfg_set() failed when attempting to set BLE_CONN_CFG_GATT. Error code: 0x{error_code:X}");
        return error_code;
    }

    return NrfError.NRF_SUCCESS;
}

uint ble_stack_init(AdapterT adapter)
{
    uint appRamBase = 0;
    uint errCode = ble.SdBleEnable(adapter, ref appRamBase);

    switch (errCode) {
        case NrfError.NRF_SUCCESS:
            break;
        case NrfError.NRF_ERROR_INVALID_STATE:
            Console.WriteLine("BLE stack already enabled\n");
            break;
        default:
            Console.WriteLine($"Failed to enable BLE stack. Error code: {errCode}");
            break;
    }

    return errCode;
}

const ushort scanInterval = 0x00A0; /**< Determines scan interval in units of 0.625 milliseconds. */
const ushort scanWindow = 0x0050; /**< Determines scan window in units of 0.625 milliseconds. */
const ushort scanTimeout = 0x0;    /**< Scan timeout between 0x01 and 0xFFFF in seconds, 0x0 disables timeout. */

var m_scan_param = new BleGapScanParamsT()
{
    Active = 0,                       // Set if active scanning.
    UseWhitelist = 0,                       // Set if selective scanning.
    AdvDirReport = 0,                       // Set adv_dir_report.
    Interval = scanInterval,
    Window = scanWindow,
    Timeout = scanTimeout
};

uint scan_start(AdapterT adapter)
{
    uint error_code = ble_gap.SdBleGapScanStart(adapter, m_scan_param);

    if (error_code != NrfError.NRF_SUCCESS)
    {
        Console.WriteLine($"Scan start failed with error code: {error_code}");
    }
    else
    {
        Console.WriteLine("Scan started");
    }

    return error_code;
}

Console.WriteLine("Hello, World!");

const string serialPort = "COM3";
const uint baudRate = 1000000;
Console.WriteLine($"Serial port used: {serialPort}");
Console.WriteLine($"Baud rate used: {baudRate}");

AdapterT adapter = AdapterInit(serialPort, baudRate);
sd_rpc.SdRpcLogHandlerSeverityFilterSet(adapter, SdRpcLogSeverityT.SD_RPC_LOG_INFO);
uint errorCode = sd_rpc.SdRpcOpen(adapter, status_handler, ble_evt_dispatch, log_handler);

if (errorCode != NrfError.NRF_SUCCESS)
{
    Console.WriteLine($"Failed to open nRF BLE Driver. Error code: 0x{errorCode:X}");
    return;
}

errorCode = ble_cfg_set(adapter, ConfigId);
if (errorCode != NrfError.NRF_SUCCESS)
    return;

errorCode = ble_stack_init(adapter);

if (errorCode != NrfError.NRF_SUCCESS)
{
    return;
}

errorCode = scan_start(adapter);

if (errorCode != NrfError.NRF_SUCCESS)
{
    return;
}

await Task.Delay(5000);

Console.WriteLine($"Closed code: 0x{sd_rpc.SdRpcClose(adapter):X}");

Console.WriteLine("Tschauuu");