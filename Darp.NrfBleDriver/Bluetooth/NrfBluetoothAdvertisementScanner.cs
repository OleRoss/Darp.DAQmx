using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
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
    private readonly ConcurrentQueue<BleAdvertisement> _queue;
    // ReSharper disable once CollectionNeverQueried.Local
    private readonly ICollection<Delegate> _delegates;

    public NrfBluetoothAdvertisementScanner(NrfBluetoothService service, ILogger? logger, CancellationToken token)
    {
        _service = service;
        _logger = logger;
        _token = token;
        _mAdvReportBuffer = new BleDataT();
        _queue = new ConcurrentQueue<BleAdvertisement>();
        _delegates = new List<Delegate>();
        uint errorCode = BleConfigSet(1);
        if (errorCode != NrfError.NRF_SUCCESS)
            throw new Exception($"Config set failed with error code: {errorCode}");

        errorCode = BleStackInit();
        if (errorCode != NrfError.NRF_SUCCESS)
            throw new Exception($"Ble Stack Init failed with error code: {errorCode}");

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

    public IBluetoothAdvertisementScanner AddServiceUuid(Guid serviceUuid) => throw new NotImplementedException();

    public IBluetoothAdvertisementScanner SetMinRssi(short rssi) => throw new NotImplementedException();

    public IBluetoothAdvertisementScanner SetMaxRssi(short rssi) => throw new NotImplementedException();

    public IBluetoothAdvertisementScanner SetSampleInterval(int scanIntervalMs, int scanWindowMs)
    {
        _scanInterval = (ushort)(scanIntervalMs / 0.625f);
        _scanWindow = (ushort)(scanWindowMs / 0.625f);
        return this;
    }

    public async IAsyncEnumerable<BleAdvertisement> ScanAsync([EnumeratorCancellation] CancellationToken token,
        bool verbose = true)
    {
        var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(_token, token);
        CancellationToken linkedToken = tokenSource.Token;

        uint errorCode = StartScan();
        if (errorCode != NrfError.NRF_SUCCESS)
        {
            _logger?.Error("Scan start failed with error code: {ErrorCode}", errorCode);
            yield break;
        }
        _logger?.Debug("Scan started");
        Action<BleGapEvtT> action = OnAdvertisementReport;
        _delegates.Add(action);
        _service.OnAdvertisementReceived += action;
        while (!linkedToken.IsCancellationRequested)
        {
            if (!_queue.IsEmpty)
            {
                bool res = _queue.TryDequeue(out BleAdvertisement? bleAdvertisement);
                if (res && bleAdvertisement != null)
                {
                    yield return bleAdvertisement;
                    continue;
                }
            }

            try
            {
                await Task.Delay(100, linkedToken);
            }
            catch (Exception)
            {
                yield break;
            }
        }
    }

    private uint StartScan()
    {
        _scanParamsT = new BleGapScanParamsT
        {
            Extended = 0,
            ReportIncompleteEvts = 0,
            Active = _mode,                       // Set if active scanning.
            FilterPolicy = BLE_GAP_SCAN_FILTER_POLICIES.BLE_GAP_SCAN_FP_ACCEPT_ALL,
            ScanPhys = BLE_GAP_PHYS.BLE_GAP_PHY_AUTO,
            Interval = _scanInterval,
            Window = _scanWindow,
            Timeout = ScanTimeout,
            ChannelMask = new byte[] {0, 0, 0, 0, 0}
        };
        unsafe
        {
            fixed (byte* pData = &_data[0])
            {
                _mAdvReportBuffer.PData = pData;
                _mAdvReportBuffer.Len = (ushort) _data.Length;
                return ble_gap.SdBleGapScanStart(_service.Adapter, _scanParamsT, _mAdvReportBuffer);
            }
        }
    }

    private void OnAdvertisementReport(BleGapEvtT gapEvt)
    {
        BleGapEvtAdvReportT report = gapEvt.@params.AdvReport;
        if (report.Type.Status == (ushort) BLE_GAP_ADV_DATA_STATUS.BLE_GAP_ADV_DATA_STATUS_INCOMPLETE_MORE_DATA)
        {
            _logger?.Warning("Waiting for more data! Skipping ...");
            return;
        }
        IReadOnlyList<(SectionType, byte[])> dataSections = report.Data.ParseAdvertisementReports();
        var advertisement = new BleAdvertisement(_service,
            DateTime.UtcNow,
            report.Type.GetAddressType(),
            report.PeerAddr.ToAddress(),
            false,
            report.Type.Connectable > 0,
            report.Type.Directed > 0,
            report.Type.ScanResponse > 0,
            report.Type.Scannable > 0,
            report.PeerAddr.ToAddressType(),
            report.Rssi,
            report.TxPower,
            dataSections.GetName(),
            dataSections,
            dataSections.GetManufactureSpecifics(),
            dataSections.GetFlags(),
            dataSections.GetServiceGuids()
        );
        if (advertisement.Name != "")
        {
            int i = 0;
        }
        _queue.Enqueue(advertisement);
        _logger?.Information("Received advertisement {@Advertisement}", report.Type);
        uint scanStart = ble_gap.SdBleGapScanStart(_service.Adapter, null, _mAdvReportBuffer);
        if (scanStart != NrfError.NRF_SUCCESS)
            _logger?.Warning("Scan restart failed with error code: {ErrorCode}", scanStart);
    }

    private uint BleStackInit()
    {
        uint appRamBase = 0;
        uint errCode = ble.SdBleEnable(_service.Adapter, ref appRamBase);

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

        // Configure the connection roles.
        var bleCfg = default(BleCfgT);
        bleCfg.GapCfg.RoleCountCfg = new BleGapCfgRoleCountT
        {
            AdvSetCount = BLE_GAP_ADV_SET.BLE_GAP_ADV_SET_COUNT_DEFAULT,
            PeriphRoleCount = 0,
            CentralRoleCount = 1,
            CentralSecCount  = 0
        };

        uint errorCode = ble.SdBleCfgSet(_service.Adapter, (uint)BLE_GAP_CFGS.BLE_GAP_CFG_ROLE_COUNT, bleCfg, ramStart);
        if (errorCode != NrfError.NRF_SUCCESS)
        {
            _logger?.Error("sd_ble_cfg_set() failed when attempting to set BLE_GAP_CFG_ROLE_COUNT." +
                      " Error code: 0x{ErrorCode:X}", errorCode);
            return errorCode;
        }

        bleCfg = default;
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

        errorCode = ble.SdBleCfgSet(_service.Adapter, (uint)BLE_CONN_CFGS.BLE_CONN_CFG_GATT, bleCfg, ramStart);
        if (errorCode != NrfError.NRF_SUCCESS)
        {
            _logger?.Error("sd_ble_cfg_set() failed when attempting to set BLE_CONN_CFG_GATT. Error code: 0x{ErrorCode:X}", errorCode);
            return errorCode;
        }

        return NrfError.NRF_SUCCESS;
    }
}

public static class Extensions
{
    public static AdvertisementType GetAddressType(this BleGapAdvReportTypeT type)
    {
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
            return AdvertisementType.Extended;
        return 0;
    }

    public static ulong ToAddress(this BleGapAddrT address)
    {
        var builder = new StringBuilder();
        for (int i = address.Addr.Length - 1; i >= 0; --i)
        {
            builder.Append($"{address.Addr[i]:X2}");
        }
        return Convert.ToUInt64(builder.ToString(), 16);
    }

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

    public static unsafe IReadOnlyList<(SectionType, byte[])> ParseAdvertisementReports(this BleDataT advData)
    {
        var list = new List<(SectionType, byte[])>();
        byte  index = 0;
        byte* pData = advData.PData;

        while (index < advData.Len)
        {
            byte fieldLength = pData[index];
            var fieldType   = (SectionType)pData[index + 1];
            if (fieldLength == 0)
                break;

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

    private static Guid GetGuid(byte[] bytes)
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
                serviceGuids.Add(GetGuid(bytes));
            }
        }
        return serviceGuids.ToArray();
    }
}