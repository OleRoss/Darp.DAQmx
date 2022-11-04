using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Bluetooth;
using Bluetooth.Advertisement;
using NrfBleDriver;
using Serilog;

namespace Darp.NrfBleDriver.Bluetooth;

public class NrfBluetoothAdvertisementScanner : IBluetoothAdvertisementScanner
{
    private const ushort ScanTimeout = 0x0;

    private readonly NrfBluetoothService _service;
    private readonly ILogger? _logger;
    private readonly CancellationToken _token;
    private byte _mode;
    private ushort _scanInterval = 0x00A0;
    private ushort _scanWindow = 0x0050;
    private BleGapScanParamsT? _scanParamsT;
    private readonly BleDataT _mAdvReportBuffer;
    private readonly byte[] _data = new byte[100];
    private uint _appRamBase = 0;
    private readonly ConcurrentQueue<BleAdvertisement> _queue;

    public NrfBluetoothAdvertisementScanner(NrfBluetoothService service, ILogger? logger, CancellationToken token)
    {
        _service = service;
        _logger = logger;
        _token = token;
        _mAdvReportBuffer = new BleDataT();
        _queue = new ConcurrentQueue<BleAdvertisement>();
        BleConfigSet(1).ThrowIfFailed("Config set failed");

        BleStackInit().ThrowIfFailed("Ble Stack Init failed");
    }

    public IBluetoothAdvertisementScanner SetScanningMode(ScanningMode mode)
    {
        _mode = mode switch
        {
            ScanningMode.Active => 1,
            _ => 0
        };
        return this;
    }

    public IBluetoothAdvertisementScanner AddServiceUuid(Guid serviceUuid)
    {
        unsafe
        {
            GCHandle handle = GCHandle.Alloc(serviceUuid.ToByteArray(), GCHandleType.Pinned);
            var bleUuidType = (byte)BLE_UUID_TYPES.BLE_UUID_TYPE_BLE;
            ble.SdBleUuidVsAdd(_service.Adapter, BleUuid128T.__GetOrCreateInstance(handle.AddrOfPinnedObject()), &bleUuidType);
            handle.Free();
        }
        return this;
    }

    public IBluetoothAdvertisementScanner SetMinRssi(short rssi) => throw new NotImplementedException();

    public IBluetoothAdvertisementScanner SetMaxRssi(short rssi) => throw new NotImplementedException();

    public IBluetoothAdvertisementScanner SetSampleInterval(int scanIntervalMs, int scanWindowMs)
    {
        if (scanWindowMs > scanIntervalMs)
            throw new ArgumentOutOfRangeException(nameof(scanWindowMs),
                $"Expected scanWindow to be smaller than scanInterval ({scanIntervalMs}), but is {scanWindowMs}");
        if (scanIntervalMs < 2.5f)
            throw new ArgumentOutOfRangeException(nameof(scanIntervalMs),
                $"Expected scanInterval to be greater than or equal to 2.5ms, but is {scanIntervalMs}");
        _scanInterval = (ushort)(scanIntervalMs * 1.6f); // 1 / 0.625
        _scanWindow = (ushort)(scanWindowMs * 1.6f);
        return this;
    }

    public async IAsyncEnumerable<BleAdvertisement> ScanAsync([EnumeratorCancellation] CancellationToken token,
        bool verbose = true)
    {
        var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(_token, token);
        CancellationToken linkedToken = tokenSource.Token;

        if (StartScan().IsFailed(_logger, "Scan start failed"))
        {
            yield break;
        }
        _logger?.Debug("Scan started");
        Action<BleGapEvtT> action = OnAdvertisementReport;
        _service.OnAdvertisementReceived += action;
        while (!linkedToken.IsCancellationRequested)
        {
            if (!_queue.IsEmpty)
            {
                bool res = _queue.TryDequeue(out BleAdvertisement? bleAdvertisement);
                if (res && bleAdvertisement != null)
                {
                    _logger?.Information("Dequeued advertisement");
                    yield return bleAdvertisement;
                    continue;
                }
            }
            await Task.Delay(100, linkedToken).WithoutThrowing();
        }
    }

    private uint StartScan()
    {
        _scanParamsT = new BleGapScanParamsT
        {
            Active = _mode,                       // Set if active scanning.
            Interval = _scanInterval,
            Window = _scanWindow,
            Timeout = ScanTimeout,
            UseWhitelist = 0,
            AdvDirReport = 0
        };
        unsafe
        {
            fixed (byte* pData = &_data[0])
            {
                _mAdvReportBuffer.PData = pData;
                _mAdvReportBuffer.Len = (ushort) _data.Length;
                return ble_gap.SdBleGapScanStart(_service.Adapter, _scanParamsT);
            }
        }
    }

    private void OnAdvertisementReport(BleGapEvtT gapEvt)
    {
        BleGapEvtAdvReportT report = gapEvt.@params.AdvReport;
        IReadOnlyList<(SectionType, byte[])> dataSections = report.ParseAdvertisementReports();
        var type = (AdvertisementType)report.Type;
        var advertisement = new BleAdvertisement(_service,
            DateTime.UtcNow,
            (AdvertisementType)report.Type,//.GetAddressType(),
            report.PeerAddr.ToAddress(),
            false,
            type == AdvertisementType.ConnectableDirected || type == AdvertisementType.ConnectableUndirected,
            false,
            false,
            false,
            report.PeerAddr.ToAddressType(),
            report.Rssi,
            (short)1,//report.TxPower,
            dataSections.GetName(),
            dataSections,
            dataSections.GetManufactureSpecifics(),
            dataSections.GetFlags(),
            dataSections.GetServiceGuids()
        );
        _queue.Enqueue(advertisement);
        _logger?.Information("Advertisement enqueued");
        // ble_gap.SdBleGapScanStart(_service.Adapter, null).IsFailed(_logger, _ => "Scan restart failed");
    }

    private uint BleStackInit()
    {
        uint errCode = ble.SdBleEnable(_service.Adapter, ref _appRamBase);

        switch (errCode) {
            case NrfError.NRF_SUCCESS:
                break;
            case NrfError.NRF_ERROR_INVALID_STATE:
                _logger?.Error("BLE stack already enabled");
                break;
            default:
                _logger?.Error("Failed to enable BLE stack. Error code: {ErrorCode}", errCode);
                break;
        }

        return errCode;
    }

    private uint BleConfigSet(byte connCfgTag)
    {
        const uint ramStart = 0; // Value is not used by ble-driver

        var bleCfg = default(BleCfgT);
        bleCfg.ConnCfg = new BleConnCfgT
        {
            ConnCfgTag = connCfgTag,
            @params = new BleConnCfgT.Params
            {
                GattConnCfg = new BleGattConnCfgT
                {
                    AttMtu = 150
                }
            }
        };
        if (ble.SdBleCfgSet(_service.Adapter, (uint)BLE_CONN_CFGS.BLE_CONN_CFG_GATT, bleCfg, ramStart)
            .IsFailed(out uint errorCode, _logger, "sd_ble_cfg_set() failed when attempting to set BLE_CONN_CFG_GATT"))
        {
            return errorCode;
        }
        /*
        bleCfg = default;
        // Configure the connection roles.
        bleCfg.GapCfg.RoleCountCfg = new BleGapCfgRoleCountT
        {
            PeriphRoleCount = 10,
            CentralRoleCount = 10,
            CentralSecCount  = 10
        };

        if (ble.SdBleCfgSet(_service.Adapter, (uint)BLE_GAP_CFGS.BLE_GAP_CFG_ROLE_COUNT, bleCfg, ramStart)
            .IsFailed(out uint errorCode, _logger, "sd_ble_cfg_set() failed when attempting to set BLE_GAP_CFG_ROLE_COUNT"))
        {
            return errorCode;
        }*/

        return NrfError.NRF_SUCCESS;
    }
}

public static class Extensions
{
    public static AdvertisementType GetAddressType(this byte type)
    {
        /*
        switch (type.Directed)
        {
            case > 0 when type.Connectable > 0:
                return AdvertisementType.ConnectableDirected;
            case 0 when type.Connectable > 0:
                return AdvertisementType.ConnectableUndirected;
            case 0 when type.Scannable > 0:
                return AdvertisementType.ScannableUndirected;
            case 0 when type.Connectable == 0:
                return AdvertisementType.NonConnectableUndirected;
        }
        if (type.ScanResponse > 0)
            return AdvertisementType.ScanResponse;
        if (type.ExtendedPdu > 0)
            return AdvertisementType.Extended;*/
        return 0;
    }

    public static ulong ToAddress(this BleGapAddrT address) => address.Addr.ToAddress();

    public static string ToAddressString(this byte[] addressBytes)
    {
        var builder = new StringBuilder();
        for (int i = addressBytes.Length - 1; i >= 0; --i) builder.Append($"{addressBytes[i]:X2}");
        return builder.ToString();
    }

    public static ulong ToAddress(this byte[] addressBytes) => Convert.ToUInt64(addressBytes.ToAddressString(), 16);

    public static AddressType ToAddressType(this BleGapAddrT address)
    {
        return address.AddrType switch
        {
            BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_PUBLIC => AddressType.Public,
            BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_RANDOM_STATIC
                or BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_RANDOM_PRIVATE_RESOLVABLE
                or BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_RANDOM_PRIVATE_NON_RESOLVABLE => AddressType.Random,
            _ => AddressType.Unspecified
        };
    }

    public static unsafe IReadOnlyList<(SectionType, byte[])> ParseAdvertisementReports(this BleGapEvtAdvReportT advData)
    {
        var list = new List<(SectionType, byte[])>();
        byte  index = 0;
        byte[] pData = advData.Data;
        
        while (index < advData.Data.Length)
        {
            byte fieldLength = pData[index];
            if (fieldLength == 0)
                break;
            var fieldType   = (SectionType)pData[index + 1];

            var bytes = new byte[fieldLength - 1];
            for (var i = 0; i < fieldLength - 1; i++) bytes[i] = pData[index + 2 + i];

            list.Add((fieldType, bytes));
            index += (byte)(fieldLength + 1);
        }
        return list;
    }

    public static AdvertisementFlags GetFlags(this IEnumerable<(SectionType, byte[])> dataSections)
    {
        foreach ((SectionType sectionType, byte[]? bytes) in dataSections)
        {
            if (sectionType != SectionType.Flags)
                continue;
            return (AdvertisementFlags) bytes[0];
        }
        return AdvertisementFlags.None;
    }

    public static string GetName(this IEnumerable<(SectionType, byte[])> dataSections)
    {
        foreach ((SectionType sectionType, byte[]? bytes) in dataSections)
        {
            if (sectionType == SectionType.CompleteLocalName)
                return Encoding.ASCII.GetString(bytes);
            if (sectionType == SectionType.ShortenedLocalName)
                return Encoding.ASCII.GetString(bytes);
        }
        return string.Empty;
    }

    public static IReadOnlyDictionary<CompanyId, byte[]> GetManufactureSpecifics(
        this IEnumerable<(SectionType, byte[])> dataSections)
    {
        var manufactures = new Dictionary<CompanyId, byte[]>();
        foreach ((SectionType sectionType, byte[]? bytes) in dataSections)
        {
            if (sectionType != SectionType.ManufacturerSpecificData)
                continue;
            manufactures[(CompanyId)BitConverter.ToUInt16(bytes.AsSpan()[..2])] = bytes[2..];
        }
        return manufactures;
    }

    public static Guid ToGuid(this byte[] bytes)
    {
        if (bytes.Length == 16)
            return new Guid(bytes);
        byte[] guidBytes = { 0,0,0,0,0,0,0,16,128,0,0,128,95,155,52,251 };
        if (bytes.Length >= 2)
        {
            guidBytes[0] = bytes[0];
            guidBytes[1] = bytes[1];
        }
        if (bytes.Length >= 4)
        {
            guidBytes[2] = bytes[2];
            guidBytes[3] = bytes[3];
        }
        return new Guid(guidBytes);
    }

    public static Guid[] GetServiceGuids(this IEnumerable<(SectionType, byte[])> dataSections)
    {
        var serviceGuids = new List<Guid>();
        foreach ((SectionType sectionType, byte[]? bytes) in dataSections)
        {
            if (sectionType is SectionType.CompleteService16BitUuids
                or SectionType.CompleteService32BitUuids
                or SectionType.CompleteService128BitUuids
                or SectionType.IncompleteService16BitUuids
                or SectionType.IncompleteService32BitUuids
                or SectionType.IncompleteService128BitUuids)
            {
                serviceGuids.Add(bytes.ToGuid());
            }
        }
        return serviceGuids.ToArray();
    }
}