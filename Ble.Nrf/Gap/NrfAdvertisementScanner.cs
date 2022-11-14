using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using Ble.Gap;
using Ble.Nrf.Nrf;
using Microsoft.Extensions.Logging;
using NrfBleDriver;

namespace Ble.Nrf.Gap;

public class NrfAdvertisementScanner : IAdvertisementScanner
{
    private const ushort ScanTimeout = 0x0;

    private readonly NrfAdapter _adapter;
    private readonly ILogger? _logger;
    private readonly byte[] _data = new byte[100];
    private readonly BleDataT _mAdvReportBuffer = new();
    private CancellationToken? _cancellation;
    public NrfAdvertisementScanner(NrfAdapter adapter, ILogger? logger)
    {
        _adapter = adapter;
        _logger = logger;
    }
    public bool IsScanning { get; private set; }
    
    public IDisposable Subscribe(IObserver<IGapAdvertisement> observer) => _adapter.GapAdvertisementReports
        .TakeWhile(_ => _cancellation is not null && !_cancellation.Value.IsCancellationRequested)
        .Select(OnAdvertisementReport)
        .Subscribe(observer);

    public IAdvertisementScanner Start(ScanningMode mode,
        float scanIntervalMs,
        float scanWindowMs,
        CancellationToken cancellationToken)
    {
        StartScan(mode, scanIntervalMs, scanWindowMs).ThrowIfFailed("Scan start failed");
        _logger?.LogDebug("Scan started");
        _cancellation = cancellationToken;
        IsScanning = true;
        return this;
    }

    public IAdvertisementScanner Stop()
    {
        _cancellation = null;
        IsScanning = false;
        return this;
    }

    private uint StartScan(ScanningMode mode,
        float scanIntervalMs,
        float scanWindowMs)
    {
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
                return ble_gap.SdBleGapScanStart(_adapter.AdapterHandle, scanParamsT);
            }
        }
    }

    private GapAdvertisement OnAdvertisementReport(BleGapEvtT gapEvt)
    {
        BleGapEvtAdvReportT report = gapEvt.@params.AdvReport;
        IReadOnlyList<(SectionType, byte[])> dataSections = report.ParseAdvertisementReports();
        var type = (AdvertisementType)report.Type;
        var advertisement = new GapAdvertisement(_adapter,
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
}