using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Runtime.InteropServices;
using Ble.Gatt;
using Ble.Nrf.Gatt;
using Ble.Nrf.Gatt.Response;
using Ble.Nrf.Nrf;
using Microsoft.Extensions.Logging;
using NrfBleDriver;

namespace Ble.Nrf;

public abstract class NrfAdapterBase
{
    private static IntPtr _libHandle;

    private static byte _globalConfigIdCounter = 1;

    protected readonly ILogger? _logger;
    protected readonly byte ConfigId;
    // ReSharper disable once CollectionNeverQueried.Local
    private readonly ICollection<Delegate> _delegates = new List<Delegate>();

    protected readonly IList<NrfConnectedPeripheral> Connections = new List<NrfConnectedPeripheral>();
    private readonly Subject<BleGapEvtT> _gapAdvertisementReports = new();
    private readonly Subject<BleGapEvtT> _gapConnectResponses = new();
    private readonly Subject<BleGattcEvtT> _primaryServiceDiscoveryResponses = new();
    private readonly Subject<BleGattcEvtT> _characteristicDiscoveryResponses = new();
    private readonly Subject<BleGattcEvtT> _descriptorDiscoveryResponses = new();
    private readonly Subject<BleGattcEvtT> _writeResponses = new();
    private readonly Subject<BleGattcEvtT> _readResponses = new();
    private readonly Subject<HvxResponse> _hvxResponses = new();
    
    private uint _appRamBase;

    public NrfAdapterBase(string serialPort, uint baudRate, ushort attMtu, ILogger? logger)
    {
        _logger = logger;
        ConfigId = _globalConfigIdCounter++;
        NativeLibrary.SetDllImportResolver(typeof(NrfAdapter).Assembly, ImportResolver);
        AdapterHandle = AdapterInit(serialPort, baudRate);
        BleConfigSet(ConfigId, attMtu);
        BleStackInit().ThrowIfFailed("Ble Stack Init failed");
    }
    public AdapterT AdapterHandle { get; }
    
    public IObservable<BleGapEvtT> GapAdvertisementReports => _gapAdvertisementReports.ObserveOn(Scheduler.Default);
    public IObservable<BleGapEvtT> GapConnectResponses => _gapConnectResponses.ObserveOn(Scheduler.Default);
    public IObservable<BleGattcEvtT> PrimaryServiceDiscoveryResponses => _primaryServiceDiscoveryResponses.ObserveOn(Scheduler.Default);
    public IObservable<BleGattcEvtT> CharacteristicDiscoveryResponses => _characteristicDiscoveryResponses.ObserveOn(Scheduler.Default);
    public IObservable<BleGattcEvtT> DescriptorDiscoveryResponses => _descriptorDiscoveryResponses.ObserveOn(Scheduler.Default);

    public IObservable<BleGattcEvtT> WriteResponses => _writeResponses.ObserveOn(Scheduler.Default);
    public IObservable<BleGattcEvtT> ReadResponses => _readResponses.ObserveOn(Scheduler.Default);
    public IObservable<HvxResponse> HvxResponses => _hvxResponses.ObserveOn(Scheduler.Default);

    private AdapterT AdapterInit(string serialPort, uint baudRate)
    {
        _logger?.LogDebug("Using serial port {SerialPort}", serialPort);
        _logger?.LogDebug("Using baud rate {BaudRate}", baudRate);
        PhysicalLayerT phy = sd_rpc.SdRpcPhysicalLayerCreateUart(serialPort,
            baudRate,
            SdRpcFlowControlT.SD_RPC_FLOW_CONTROL_NONE,
            SdRpcParityT.SD_RPC_PARITY_NONE);
        DataLinkLayerT dataLinkLayer = sd_rpc.SdRpcDataLinkLayerCreateBtThreeWire(phy, 250);
        TransportLayerT transportLayer = sd_rpc.SdRpcTransportLayerCreate(dataLinkLayer, 1500);
        AdapterT adapter = sd_rpc.SdRpcAdapterCreate(transportLayer);

        sd_rpc.SdRpcLogHandlerSeverityFilterSet(adapter, SdRpcLogSeverityT.SD_RPC_LOG_INFO)
            .ThrowIfFailed("Failed to set log handler severity filter");
        SdRpcStatusHandlerT statusHandler = StatusHandler;
        SdRpcEvtHandlerT dispatchHandler = BleEvtDispatch;
        SdRpcLogHandlerT logHandler = LogHandler;
        _delegates.Add(statusHandler);
        _delegates.Add(dispatchHandler);
        _delegates.Add(logHandler);
        sd_rpc.SdRpcOpen(adapter, statusHandler, dispatchHandler, logHandler)
            .ThrowIfFailed("Could not open connection to adapter");
        return adapter;
    }
    
    private void StatusHandler(IntPtr adapter, SdRpcAppStatusT code, string message) => _logger
        ?.LogTrace("Status: {Code}, message: {Message}", code, message);

    private void BleEvtDispatch(IntPtr adapter, IntPtr pBleEvt)
    {
        var bleEvt = BleEvtT.__GetOrCreateInstance(pBleEvt);
        if (bleEvt == null)
        {
            _logger?.LogWarning("Received an empty BLE event");
            return;
        }

        switch (bleEvt.Header.EvtId)
        {
            case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_ADV_REPORT:
                _logger?.LogTrace("Adv report");
                _gapAdvertisementReports.OnNext(bleEvt.evt.GapEvt);
                break;
            case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_CONNECTED:
                _gapConnectResponses.OnNext(bleEvt.evt.GapEvt);
                break;
            case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_DISCONNECTED:
                for (int i = Connections.Count - 1; i >= 0; i--)
                {
                    if (Connections[i].ConnectionHandle != bleEvt.evt.GapEvt.ConnHandle) continue;
                    Connections[i].Dispose();
                }
                
                _logger?.LogTrace("Connection 0x{Handle:X} disconnected", bleEvt.evt.GapEvt.ConnHandle);
                break;
            case (ushort)BLE_GATTC_EVTS.BLE_GATTC_EVT_PRIM_SRVC_DISC_RSP:
                _logger?.LogTrace("Service discovery response");
                _primaryServiceDiscoveryResponses.OnNext(bleEvt.evt.GattcEvt);
                break;
            case (ushort)BLE_GATTC_EVTS.BLE_GATTC_EVT_CHAR_DISC_RSP:
                _logger?.LogTrace("Characteristic discovery response");
                _characteristicDiscoveryResponses.OnNext(bleEvt.evt.GattcEvt);
                break;
            case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_DATA_LENGTH_UPDATE_REQUEST:
                _logger?.LogTrace("length update request {@Request}",
                    bleEvt.evt.GapEvt.@params.DataLengthUpdateRequest);
                break;
            case (ushort)BLE_GATTC_EVTS.BLE_GATTC_EVT_WRITE_RSP:
                _writeResponses.OnNext(bleEvt.evt.GattcEvt);
                break;
            case (ushort)BLE_GATTC_EVTS.BLE_GATTC_EVT_READ_RSP:
                _readResponses.OnNext(bleEvt.evt.GattcEvt);
                break;
            case (ushort)BLE_GATTC_EVTS.BLE_GATTC_EVT_HVX:
                unsafe
                {
                    // 20 is the number of bytes for the evt before the data starts
                    var span = new Span<byte>((byte*)pBleEvt, 20+bleEvt.evt.GattcEvt.@params.Hvx.Len);
                    var hvxResponse = new HvxResponse
                    {
                        ConnHandle = bleEvt.evt.CommonEvt.ConnHandle,
                        Status = (BLE_GATT_STATUS_CODES)bleEvt.evt.GattcEvt.GattStatus,
                        ErrorHandle = bleEvt.evt.GattcEvt.ErrorHandle,
                        AttributeHandle = bleEvt.evt.GattcEvt.ErrorHandle,
                        Handle = bleEvt.evt.GattcEvt.@params.Hvx.Handle,
                        Type = (BLE_GATT_HVX_TYPES)bleEvt.evt.GattcEvt.@params.Hvx.Type,
                        Data = span[20..].ToArray()
                    };
                    _hvxResponses.OnNext(hvxResponse);
                }
                break;
            case (ushort)BLE_GATTC_EVTS.BLE_GATTC_EVT_DESC_DISC_RSP:
                _descriptorDiscoveryResponses.OnNext(bleEvt.evt.GattcEvt);
                break;
            default:
                _logger?.LogWarning("Received an un-handled event with ID: {EventId}", bleEvt.Header.EvtId);
                break;
        }
    }

    private void LogHandler(IntPtr adapter, SdRpcLogSeverityT severity, string message)
    {
        _logger?.Log((LogLevel)severity, "Received {Level} log: {Message}", severity, message);
    }
    
    protected uint BleStackInit()
    {
        uint errCode = ble.SdBleEnable(AdapterHandle, ref _appRamBase);

        switch (errCode) {
            case NrfError.NRF_SUCCESS:
                break;
            case NrfError.NRF_ERROR_INVALID_STATE:
                _logger?.LogError("BLE stack already enabled");
                break;
            default:
                _logger?.LogError("Failed to enable BLE stack. Error code: 0x{ErrorCode:X}", errCode);
                break;
        }

        return errCode;
    }

    protected void BleConfigSet(byte connCfgTag, ushort attMtu)
    {
        var bleCfg = default(BleCfgT);
        bleCfg.ConnCfg = new BleConnCfgT
        {
            ConnCfgTag = connCfgTag,
            @params = new BleConnCfgT.Params
            {
                GattConnCfg = new BleGattConnCfgT
                {
                    AttMtu = attMtu
                }
            }
        };
        ble.SdBleCfgSet(AdapterHandle, (uint)BLE_CONN_CFGS.BLE_CONN_CFG_GATT, bleCfg, _appRamBase)
            .ThrowIfFailed("sd_ble_cfg_set() failed when attempting to set BLE_CONN_CFG_GATT");
    }
    
    private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName != "NrfBleDriver" || _libHandle != IntPtr.Zero)
            return _libHandle;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            NativeLibrary.TryLoad(@"Nrf\NrfBleDriver.dll", assembly, new DllImportSearchPath(), out _libHandle);
        else
            throw new PlatformNotSupportedException();
        if (_libHandle == IntPtr.Zero)
            throw new Exception("Could not load native library!");
        return _libHandle;
    }
}