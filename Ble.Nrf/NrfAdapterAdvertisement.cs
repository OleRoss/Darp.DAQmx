using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ble.Configuration;
using Ble.Connection;
using Ble.Gap;
using Ble.Nrf.Nrf;
using Ble.Utils;
using Microsoft.Extensions.Logging;
using NrfBleDriver;

namespace Ble.Nrf;

public sealed partial class NrfAdapter
{
    private const ushort ScanTimeout = 0x0;

    private readonly byte[] _data = new byte[100];
    private uint _appRamBase = 0;
    private readonly BleDataT _mAdvReportBuffer = new();
    private bool _connectionInProgress;

    public IObservable<IAdvertisement> Observe(ScanningMode mode,
        float scanIntervalMs,
        float scanWindowMs,
        CancellationToken cancellationToken)
    {
        BleConfigSet(1);

        BleStackInit().ThrowIfFailed("Ble Stack Init failed");
        StartScan(mode, scanIntervalMs, scanWindowMs).ThrowIfFailed("Scan start failed");
        _logger?.LogDebug("Scan started");
        return GapAdvertisementReports
            .TakeWhile(_ => !cancellationToken.IsCancellationRequested)
            .Select(OnAdvertisementReport);
    }

    private uint BleStackInit()
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

    private void BleConfigSet(byte connCfgTag)
    {
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
        ble.SdBleCfgSet(AdapterHandle, (uint)BLE_CONN_CFGS.BLE_CONN_CFG_GATT, bleCfg, _appRamBase)
            .ThrowIfFailed("sd_ble_cfg_set() failed when attempting to set BLE_CONN_CFG_GATT");
    }

    private Advertisement OnAdvertisementReport(BleGapEvtT gapEvt)
    {
        BleGapEvtAdvReportT report = gapEvt.@params.AdvReport;
        IReadOnlyList<(SectionType, byte[])> dataSections = report.ParseAdvertisementReports();
        var type = (AdvertisementType)report.Type;
        var advertisement = new Advertisement(this,
            DateTime.UtcNow,
            (AdvertisementType)report.Type,//.GetAddressType(),
            report.PeerAddr.ToAddress(),
            false,
            type == AdvertisementType.ConnectableDirected || type == AdvertisementType.ConnectableUndirected,
            false,
            report.ScanRsp > 0,
            false,
            report.PeerAddr.ToAddressType(),
            report.Rssi,
            1,//report.TxPower,
            dataSections.GetName(),
            dataSections,
            dataSections.GetManufactureSpecifics(),
            dataSections.GetFlags(),
            dataSections.GetServiceGuids()
        );
        return advertisement;
    }

    private uint StartScan(ScanningMode mode,
        float scanIntervalMs,
        float scanWindowMs)
    {
        if (scanWindowMs > scanIntervalMs)
            throw new ArgumentOutOfRangeException(nameof(scanWindowMs),
                $"Expected scanWindow to be smaller than scanInterval ({scanIntervalMs}), but is {scanWindowMs}");
        if (scanIntervalMs < 2.5f)
            throw new ArgumentOutOfRangeException(nameof(scanIntervalMs),
                $"Expected scanInterval to be greater than or equal to 2.5ms, but is {scanIntervalMs}");
        var scanInterval = (ushort)(scanIntervalMs * 1.6f); // 1 / 0.625
        var scanWindow = (ushort)(scanWindowMs * 1.6f);
        var scanParamsT = new BleGapScanParamsT
        {
            Active = (byte)mode,
            Interval = scanInterval,
            Window = scanWindow,
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
                return ble_gap.SdBleGapScanStart(AdapterHandle, scanParamsT);
            }
        }
    }
    
    public async Task<IConnection?> ConnectAsync(ulong bluetoothId, Peripheral? peripheral, CancellationToken cancellationToken = default)
    {
        _logger?.LogInformation("Attempting connection with {BluetoothId:X}", bluetoothId);
        // Wait until running connections are finished
        while (_connectionInProgress)
        {
            bool cancelled = await Task.Delay(10, cancellationToken).WithoutThrowing();
            if (cancelled) return null;
        }
        _connectionInProgress = true;
        try
        {
            // Determines minimum connection interval in milliseconds
            const ushort minConnectionInterval = 7500 / 1250;
            // Determines maximum connection interval in milliseconds
            const ushort maxConnectionInterval = 7500 / 1250;
            // Slave Latency in number of connection events
            const ushort slaveLatency = 0;
            const ushort timeoutMs = 4000;
            // Determines supervision time-out in units of 10 milliseconds
            const ushort connectionSupervisionTimeout = timeoutMs / 10;
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
                Active = 0,
                Interval = scanInterval,
                Window = scanWindow,
                Timeout = 0,
                AdvDirReport = 0,
                UseWhitelist = 0
            };
            if (ble_gap.SdBleGapConnect(AdapterHandle, addr, scanParam, mConnectionParam, _configId)
                .IsFailed(_logger, "Ble gap connection failed"))
            {
                return null;
            }

            _logger?.LogTrace("Connecting ...");
            BleGapEvtT? evt = await GapConnectResponses.FirstOrDefaultAsync().ToTask(cancellationToken);
            if (evt == null)
            {
                _logger?.LogWarning("Connection timed out");
                return null;
            }

            var connection = new NrfConnection(this, evt.ConnHandle, _logger);
            _connections.Add(connection);
            
            bool success = await connection.DiscoverServicesAsync(peripheral, cancellationToken);
            if (success) return connection;

            _logger?.LogWarning("Service discovery with failed. Ignoring connection...");
            connection.Dispose();
            return null;
        }
        finally
        {
            _connectionInProgress = false;
        }
    }

    public bool Disconnect(IConnection connection)
    {
        _logger?.LogTrace("Disconnecting {@Connection}", connection);
        _connections.Remove(connection);
        return ble_gap.SdBleGapDisconnect(AdapterHandle, connection.ConnectionHandle,
                (byte)BLE_HCI_STATUS_CODES.BLE_HCI_REMOTE_USER_TERMINATED_CONNECTION)
            .IsFailed(_logger, "Disconnection failed");
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

    public static BleUuidT ToBleUuid(this Guid guid)
    {
        byte[] x = guid.ToByteArray();
        var value = BitConverter.ToUInt16(x.AsSpan()[..2]);
        return new BleUuidT
        {
            Type = (byte)BLE_UUID_TYPES.BLE_UUID_TYPE_BLE,
            Uuid = value
        };
    }

    public static DefaultUuid ToDefaultUuid(this Guid guid)
    {
        unsafe
        {
            ushort value = *(ushort*)&guid;
            return (DefaultUuid)value;
        }
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
                serviceGuids.Add(bytes.ToBleGuid());
            }
        }
        return serviceGuids.ToArray();
    }
}