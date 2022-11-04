// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Runtime.InteropServices;
using Bluetooth.Advertisement;
using Darp.NrfBleDriver.Bluetooth;
using NrfBleDriver;

unsafe
{
    IntPtr libHandle = IntPtr.Zero;
    const byte ConfigId = 1;
    byte m_connected_devices = 0;
    bool m_connection_is_in_progress = false;

    NativeLibrary.SetDllImportResolver(typeof(Program).Assembly, ImportResolver);

    IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName != "NrfBleDriver" || libHandle != IntPtr.Zero)
            return libHandle;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            NativeLibrary.TryLoad(@"C:\Users\OleRosskamp\RiderProjects\NiTests\Darp.NrfBleDriver\Nrf\NrfBleDriverV6.dll", assembly, new DllImportSearchPath?(), out libHandle);
        return libHandle;
    }

    AdapterT m_adapter;

// Determines scan interval in units of 0.625 milliseconds
    const ushort scanInterval = 0x00A0;
// Determines scan window in units of 0.625 milliseconds
    const ushort scanWindow = 0x00A0;
// Scan timeout between 0x01 and 0xFFFF in seconds, 0x0 disables timeout
    const ushort scanTimeout = 0x0;

    var m_scan_param = new BleGapScanParamsT
    {
        Extended = 0,
        ReportIncompleteEvts = 0,
        Active = 0,                       // Set if active scanning.
        FilterPolicy = BLE_GAP_SCAN_FILTER_POLICIES.BLE_GAP_SCAN_FP_ACCEPT_ALL,
        ScanPhys = BLE_GAP_PHYS.BLE_GAP_PHY_1MBPS,
        Interval = scanInterval,
        Window = scanWindow,
        Timeout = scanTimeout,
        ChannelMask = new byte[] {0, 0, 0, 0, 0}
    };

    const ushort timeoutMs = 4000;
    // Determines minimum connection interval in milliseconds
    const ushort minConnectionInterval = 7500 / 1250;
    // Determines maximum connection interval in milliseconds
    const ushort maxConnectionInterval = 7500 / 1250;
    // Slave Latency in number of connection events
    const ushort slaveLatency = 0;
    // Determines supervision time-out in units of 10 milliseconds
    var connectionSupervisionTimeout = (ushort) (timeoutMs / 10);

    var m_connection_param = new BleGapConnParamsT
    {
        MinConnInterval = minConnectionInterval,
        MaxConnInterval = maxConnectionInterval,
        SlaveLatency = slaveLatency,
        ConnSupTimeout = connectionSupervisionTimeout
    };

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

    const int STRING_BUFFER_SIZE = 50;

    void on_adv_report(BleGapEvtT gapEvt)
    {
        uint err_code;
        var str = new byte[STRING_BUFFER_SIZE];

        // Log the Bluetooth device address of advertisement packet received.
        //ble_address_to_string_convert(p_ble_gap_evt->params.adv_report.peer_addr, str);
        //Console.WriteLine($"Received advertisement report with device address: 0x{str}");

        BleGapEvtAdvReportT report = gapEvt.@params.AdvReport;
        if (report.Type.Status == (ushort) BLE_GAP_ADV_DATA_STATUS.BLE_GAP_ADV_DATA_STATUS_INCOMPLETE_MORE_DATA)
        {
            Console.WriteLine("Waiting for more data! Skipping ...");
            return;
        }
        IReadOnlyList<(SectionType, byte[])> dataSections = report.Data.ParseAdvertisementReports();
        Guid guid = Guid.Parse("0000fd4c-0000-1000-8000-00805f9b34fb");
        var guids = dataSections.GetServiceGuids();
        //Console.WriteLine($"{string.Join(',', guids)}");
        if (guids.Contains(guid))
        {
            Console.WriteLine("Got matching guid");
            if (m_connected_devices >= 1 || m_connection_is_in_progress)
            {
                return;
            }

            Console.WriteLine("Valid");
            err_code = ble_gap.SdBleGapConnect(m_adapter,
                gapEvt.@params.AdvReport.PeerAddr,
                m_scan_param,
                m_connection_param,
                ConfigId);
            Console.WriteLine("Started connection");
            if (err_code != NrfError.NRF_SUCCESS)
            {
                Console.WriteLine($"Connection Request Failed, reason {err_code}");
                return;
            }

            Console.WriteLine("In progress");
            m_connection_is_in_progress = true;
        }

        scan_start(m_adapter, null);
    }

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
                // Console.WriteLine("Received Advertisement");
                on_adv_report(ble_evt.evt.GapEvt);
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
            AdvSetCount = BLE_GAP_ADV_SET.BLE_GAP_ADV_SET_COUNT_DEFAULT,
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
                GapConnCfg = new BleGapConnCfgT
                {
                    ConnCount = 10,
                    EventLength = 320
                }
            }
        };

        error_code = ble.SdBleCfgSet(adapter, (uint)BLE_CONN_CFGS.BLE_CONN_CFG_GAP, bleCfg, ram_start);
        if (error_code != NrfError.NRF_SUCCESS)
        {
            Console.WriteLine($"sd_ble_cfg_set() failed when attempting to set BLE_CONN_CFG_GAP. Error code: 0x{error_code:X}");
            return error_code;
        }

        /*bleCfg = default;
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
        }*/

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

    var m_adv_report_buffer = new BleDataT();
    const ushort dataLength = 1000;
    byte* mp_data = stackalloc byte[dataLength];

    uint scan_start(AdapterT adapter, BleGapScanParamsT? scan_param)
    {
        m_adv_report_buffer!.PData = mp_data;
        m_adv_report_buffer!.Len = dataLength;

        uint error_code = ble_gap.SdBleGapScanStart(adapter, scan_param, m_adv_report_buffer);

        if (error_code != NrfError.NRF_SUCCESS)
        {
            Console.WriteLine($"Scan start failed with error code: {error_code}");
            return error_code;
        }

        // Console.WriteLine("Scan started");
        return error_code;
    }

    Console.WriteLine("Hello, World!");

    const string serialPort = "COM5";
    const uint baudRate = 1000000;
    Console.WriteLine($"Serial port used: {serialPort}");
    Console.WriteLine($"Baud rate used: {baudRate}");

    m_adapter = AdapterInit(serialPort, baudRate);
    sd_rpc.SdRpcLogHandlerSeverityFilterSet(m_adapter, SdRpcLogSeverityT.SD_RPC_LOG_INFO);
    uint errorCode = sd_rpc.SdRpcOpen(m_adapter, status_handler, ble_evt_dispatch, log_handler);

    if (errorCode != NrfError.NRF_SUCCESS)
    {
        Console.WriteLine($"Failed to open nRF BLE Driver. Error code: 0x{errorCode:X}");
        return;
    }

    errorCode = ble_cfg_set(m_adapter, ConfigId);
    if (errorCode != NrfError.NRF_SUCCESS)
        return;

    errorCode = ble_stack_init(m_adapter);

    if (errorCode != NrfError.NRF_SUCCESS)
    {
        return;
    }

    errorCode = scan_start(m_adapter, m_scan_param);

    if (errorCode != NrfError.NRF_SUCCESS)
    {
        return;
    }

    Thread.Sleep(5000);

    Console.WriteLine($"Closed code: 0x{sd_rpc.SdRpcClose(m_adapter):X}");

    Console.WriteLine("Tschauuu");
}



// dfu usb-serial -p COM3 -pkg "C:\Users\VakuO\Downloads\nrf-ble-driver-4.1.4-win_x86_64 (1)\nrf-ble-driver-4.1.4-win_x86_64\share\nrf-ble-driver\hex\sd_api_v6\connectivity_4.1.4_usb_with_s140_6.1.1_dfu_pkg.zip"