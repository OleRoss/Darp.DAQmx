using System.Reflection;
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
    private readonly ILogger? _logger;
    public AdapterT Adapter { get; }
    // ReSharper disable once CollectionNeverQueried.Local
    private readonly ICollection<Delegate> _delegates;

    public NrfBluetoothService(string serialPort, uint baudRate = 1000000, ILogger? logger = null)
    {
        _logger = logger;
        _logger?.Information("Using serial port {SerialPort}", serialPort);
        _logger?.Information("Using baud rate {BaudRate}", baudRate);
        _delegates = new List<Delegate>();
        ErrorOr<AdapterT> errorOrAdapter = AdapterInit(serialPort, baudRate);
        if (errorOrAdapter.IsError)
            throw new Exception(string.Join(',', errorOrAdapter.Errors.Select(x => x.Description)));
        Adapter = errorOrAdapter.Value;
    }

    public IBluetoothAdvertisementScanner AdvertisementScanner(CancellationToken token) =>
        new NrfBluetoothAdvertisementScanner(this, _logger, token);

    public Task<IBleDevice?> ConnectDeviceAsync(string id) => throw new NotImplementedException();
    public Task<IBleDevice?> ConnectDeviceAsync(ulong bluetoothId) => throw new NotImplementedException();

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
                _logger?.Debug("Received Advertisement");
                //on_adv_report(bleEvt.evt.GapEvt);
                OnAdvertisementReceived?.Invoke(bleEvt.evt.GapEvt);
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