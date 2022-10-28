﻿using System.Reflection;
using System.Runtime.InteropServices;
using Bluetooth;
using Bluetooth.Advertisement;
using Bluetooth.Device;
using ErrorOr;
using NrfBleDriver;
using Serilog;
using Serilog.Events;

namespace Darp.NrfBleDriver.Bluetooth;

public class NrfBluetoothService : IBluetoothService
{
    public event Action<BleGapEvtT>? OnAdvertisementReceived;
    public event Action<BleGapEvtT>? OnConnected;
    public event Action<BleGattcEvtT>? OnPrimaryServiceDiscoveryResponse;

    private readonly ILogger? _logger;
    public AdapterT Adapter { get; }
    // ReSharper disable once CollectionNeverQueried.Local
    private readonly ICollection<Delegate> _delegates;
    private readonly IList<NrfDevice> _devices;
    private bool _connectionInProgress;
    private static byte _globalConfigIdCounter = 1;
    private readonly byte _configId;

    public NrfBluetoothService(string serialPort, uint baudRate = 1000000, ILogger? logger = null)
    {
        _configId = _globalConfigIdCounter++;
        _logger = logger;
        _logger?.Information("Using serial port {SerialPort}", serialPort);
        _logger?.Information("Using baud rate {BaudRate}", baudRate);
        _delegates = new List<Delegate>();
        _devices = new List<NrfDevice>();
        _connectionInProgress = false;
        ErrorOr<AdapterT> errorOrAdapter = AdapterInit(serialPort, baudRate);
        if (errorOrAdapter.IsError)
            throw new Exception(string.Join(',', errorOrAdapter.Errors.Select(x => x.Description)));
        Adapter = errorOrAdapter.Value;
    }

    public IBluetoothAdvertisementScanner AdvertisementScanner(CancellationToken token) =>
        new NrfBluetoothAdvertisementScanner(this, _logger, token);

    public Task<IBleDevice?> ConnectDeviceAsync(string id) => throw new NotImplementedException();
    public async Task<IBleDevice?> ConnectDeviceAsync(ulong bluetoothId)
    {
        const ushort timeoutMs = 4000;
        // Wait until running connections are finished
        while (_connectionInProgress) await Task.Delay(timeoutMs);
        _connectionInProgress = true;
        // Determines minimum connection interval in milliseconds
        const ushort minConnectionInterval = 7500 / 1250;
        // Determines maximum connection interval in milliseconds
        const ushort maxConnectionInterval = 7500 / 1250;
        // Slave Latency in number of connection events
        const ushort slaveLatency = 0;
        // Determines supervision time-out in units of 10 milliseconds
        var connectionSupervisionTimeout = (ushort) (timeoutMs / 10);
        const ushort scanInterval = 0x00A0;
        const ushort scanWindow = 0x0050;

        var addr = new BleGapAddrT
        {
            Addr = BitConverter.GetBytes(bluetoothId)[..6],
            AddrType = BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_PUBLIC,
            AddrIdPeer = 0
        };
        var mConnectionParam = new BleGapConnParamsT
        {
            MinConnInterval = minConnectionInterval,
            MaxConnInterval = maxConnectionInterval,
            SlaveLatency = slaveLatency,
            ConnSupTimeout = connectionSupervisionTimeout
        };
        var scanParam = new BleGapScanParamsT
        {
            Extended = 0,
            ReportIncompleteEvts = 0,
            Active = 0,
            FilterPolicy = BLE_GAP_SCAN_FILTER_POLICIES.BLE_GAP_SCAN_FP_ACCEPT_ALL,
            ScanPhys = BLE_GAP_PHYS.BLE_GAP_PHY_AUTO,
            Interval = scanInterval,
            Window = scanWindow,
            Timeout = 0,
            ChannelMask = new byte[] {0, 0, 0, 0, 0}
        };
        _logger?.Information("aa {@Address}, {@ScanParam}, {@ConnectionParam}, {ConfigId}", addr, scanParam, mConnectionParam, _configId);
        if (ble_gap.SdBleGapConnect(Adapter, addr, scanParam, mConnectionParam, _configId)
            .IsFailed(_logger, "Ble gap connection failed"))
        {
            return null;
        }

        OnConnected += NrfUtils.CreateEventSubscription(out Func<BleGapEvtT> action, timeoutMs, out CancellationToken token);
        BleGapEvtT evt = await NrfUtils.FirstEventAsync(action, token);
        _connectionInProgress = false;
        var device = new NrfDevice(this, evt.ConnHandle, addr.Addr, _logger);
        _devices.Add(device);
        return device;
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _logger?.Debug("Disposing of nrf bluetooth service");
        uint errorCode = sd_rpc.SdRpcClose(Adapter);
        if (errorCode != NrfError.NRF_SUCCESS)
            _logger?.Error("Failed to close nRF BLE Driver. Error code: 0x{ErrorCode:X}", errorCode);
    }

    IBluetoothScanner IBluetoothService.Scanner() => throw new NotImplementedException();

    private void StatusHandler(IntPtr adapter, SdRpcAppStatusT code, string message) => _logger
        ?.Verbose("Status: {Code}, message: {Message}", code, message);

    private void BleEvtDispatch(IntPtr adapter, IntPtr pBleEvt)
    {
        var bleEvt = BleEvtT.__GetOrCreateInstance(pBleEvt);
        if (bleEvt == null)
        {
            _logger?.Warning("Received an empty BLE event");
            return;
        }

        switch (bleEvt.Header.EvtId)
        {
            case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_ADV_REPORT:
                OnAdvertisementReceived?.Invoke(bleEvt.evt.GapEvt);
                break;
            case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_CONNECTED:
                _logger?.Debug("Device connected");
                OnConnected?.Invoke(bleEvt.evt.GapEvt);
                break;
            case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_DISCONNECTED:
                _logger?.Debug("Device disconnected");
                var removedAny = false;
                for (int i = _devices.Count - 1; i >= 0; i--)
                {
                    if (_devices[i].ConnectionHandle != bleEvt.evt.GapEvt.ConnHandle) continue;
                    _devices.RemoveAt(i);
                    removedAny = true;
                }
                if (!removedAny)
                    _logger?.Warning("Unknown device with handle 0x{Handle:X} disconnected", bleEvt.evt.GapEvt.ConnHandle);
                break;
            case (ushort)BLE_GATTC_EVTS.BLE_GATTC_EVT_PRIM_SRVC_DISC_RSP:
                _logger?.Debug("Service discovery response");
                OnPrimaryServiceDiscoveryResponse?.Invoke(bleEvt.evt.GattcEvt);
                break;
            default:
                _logger?.Warning("Received an un-handled event with ID: {EventId}", bleEvt.Header.EvtId);
                break;
        }
    }

    private void LogHandler(IntPtr adapter, SdRpcLogSeverityT severity, string message)
    {
        LogEventLevel level = severity switch
        {
            SdRpcLogSeverityT.SD_RPC_LOG_TRACE => LogEventLevel.Verbose,
            SdRpcLogSeverityT.SD_RPC_LOG_DEBUG => LogEventLevel.Debug,
            SdRpcLogSeverityT.SD_RPC_LOG_INFO => LogEventLevel.Information,
            SdRpcLogSeverityT.SD_RPC_LOG_WARNING => LogEventLevel.Warning,
            SdRpcLogSeverityT.SD_RPC_LOG_ERROR => LogEventLevel.Error,
            _ => LogEventLevel.Fatal,
        };
        _logger?.Write(level, "Received {Level} log: {Message}", severity, message);
    }

    private ErrorOr<AdapterT> AdapterInit(string serialPort, uint baudRate)
    {
        PhysicalLayerT phy = sd_rpc.SdRpcPhysicalLayerCreateUart(serialPort,
            baudRate,
            SdRpcFlowControlT.SD_RPC_FLOW_CONTROL_NONE,
            SdRpcParityT.SD_RPC_PARITY_NONE);
        DataLinkLayerT dataLinkLayer = sd_rpc.SdRpcDataLinkLayerCreateBtThreeWire(phy, 250);
        TransportLayerT transportLayer = sd_rpc.SdRpcTransportLayerCreate(dataLinkLayer, 1500);
        AdapterT adapter = sd_rpc.SdRpcAdapterCreate(transportLayer);

        uint errorCode = sd_rpc.SdRpcLogHandlerSeverityFilterSet(adapter, SdRpcLogSeverityT.SD_RPC_LOG_INFO);
        if (errorCode != NrfError.NRF_SUCCESS)
            return Error.Failure($"Failed to register log handler. Error code: 0x{errorCode:X}");
        SdRpcStatusHandlerT statusHandler = StatusHandler;
        SdRpcEvtHandlerT dispatchHandler = BleEvtDispatch;
        SdRpcLogHandlerT logHandler = LogHandler;
        _delegates.Add(statusHandler);
        _delegates.Add(dispatchHandler);
        _delegates.Add(logHandler);
        errorCode = sd_rpc.SdRpcOpen(adapter, statusHandler, dispatchHandler, logHandler);
        if (errorCode != NrfError.NRF_SUCCESS)
            return Error.Failure($"Failed to open nRF BLE Driver. Error code: 0x{errorCode:X}");
        return adapter;
    }

    private static IntPtr _libHandle = IntPtr.Zero;
    public static void Setup()
    {
        NativeLibrary.SetDllImportResolver(typeof(Program).Assembly, ImportResolver);
    }

    private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName != "NrfBleDriver" || _libHandle != IntPtr.Zero)
            return _libHandle;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            NativeLibrary.TryLoad(@"C:\Users\VakuO\RiderProjects\NiTests\Darp.NrfBleDriver\Nrf\NrfBleDriverV6.dll", assembly, new DllImportSearchPath?(), out _libHandle);
        return _libHandle;
    }
}