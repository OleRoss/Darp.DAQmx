using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Runtime.InteropServices;
using Ble.Connection;
using Ble.Nrf.Nrf;
using Microsoft.Extensions.Logging;
using NrfBleDriver;

namespace Ble.Nrf;

public sealed partial class NrfAdapter : IBleAdapter
{
    private static byte _globalConfigIdCounter = 1;
    private static IntPtr _libHandle;

    private readonly ILogger? _logger;
    private readonly byte _configId;
    // ReSharper disable once CollectionNeverQueried.Local
    private readonly ICollection<Delegate> _delegates = new List<Delegate>();

    private readonly IList<IConnection> _connections = new List<IConnection>();
    private readonly Subject<BleGapEvtT> _gapAdvertisementReports = new();
    private readonly Subject<BleGapEvtT> _gapConnectResponses = new();
    private readonly Subject<BleGattcEvtT> _primaryServiceDiscoveryResponses = new();
    private readonly Subject<BleGattcEvtT> _characteristicDiscoveryResponses = new();
    private readonly Subject<BleGattcEvtT> _descriptorDiscoveryResponses = new();
    private readonly Subject<BleGattcEvtT> _writeResponses = new();
    private readonly Subject<BleGattcEvtT> _readResponses = new();
    private readonly Subject<HvxResponse> _hvxResponses = new();

    public NrfAdapter(string serialPort, uint baudRate = 1000000, ILogger? logger = null)
    {
        _logger = logger;
        _configId = _globalConfigIdCounter++;
        NativeLibrary.SetDllImportResolver(typeof(NrfAdapter).Assembly, ImportResolver);
        AdapterHandle = AdapterInit(serialPort, baudRate);
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

    public void Clear()
    {
        _logger?.LogTrace("Clearing {ConnectionCount} connected devices", _connections.Count);
        for (int index = _connections.Count - 1; index >= 0; index--)
        {
            IConnection connection = _connections[index];
            connection.Dispose();
        }

        _connections.Clear();
    }

    private AdapterT AdapterInit(string serialPort, uint baudRate)
    {
        var m = LoggerMessage.Define<string>(LogLevel.Critical, new EventId(1, "asdads"), "{SerialPort}");
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
                _gapAdvertisementReports.OnNext(bleEvt.evt.GapEvt);
                break;
            case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_CONNECTED:
                _gapConnectResponses.OnNext(bleEvt.evt.GapEvt);
                break;
            case (ushort)BLE_GAP_EVTS.BLE_GAP_EVT_DISCONNECTED:
                _logger?.LogTrace("Device disconnected");
                var removedAny = false;
                for (int i = _connections.Count - 1; i >= 0; i--)
                {
                    if (_connections[i].ConnectionHandle != bleEvt.evt.GapEvt.ConnHandle) continue;
                    _connections[i].Dispose();
                    removedAny = true;
                }
                if (!removedAny)
                    _logger?.LogWarning("Unknown device with handle 0x{Handle:X} disconnected", bleEvt.evt.GapEvt.ConnHandle);
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
                    _logger?.LogTrace("Received hvx {@HvxResponse}", hvxResponse);
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

    public void Dispose()
    {
        _logger?.LogDebug("Disposing of nrf bluetooth adapter");
        Clear();
        sd_rpc.SdRpcClose(AdapterHandle)
            .IsFailed(_logger, "Failed to close nRF BLE Driver");
        _gapAdvertisementReports.Dispose();
        _gapConnectResponses.Dispose();
        _primaryServiceDiscoveryResponses.Dispose();
        _characteristicDiscoveryResponses.Dispose();
        _writeResponses.Dispose();
        AdapterHandle.Dispose();
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

public interface IGattResponse
{
    ushort ConnHandle { get; }
    BLE_GATT_STATUS_CODES Status { get; }
    ushort ErrorHandle { get; }
    ushort AttributeHandle { get; }
}

public readonly record struct HvxResponse(
    ushort ConnHandle,
    BLE_GATT_STATUS_CODES Status,
    ushort ErrorHandle,
    ushort AttributeHandle,
    ushort Handle,
    BLE_GATT_HVX_TYPES Type,
    byte[] Data) : IGattResponse;