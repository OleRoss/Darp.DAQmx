using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Foundation;
using Ble.Gap;
using Ble.Uuid;
using Bluetooth.WinRT.Advertisement;
using Microsoft.Extensions.Logging;

namespace Ble.WinRT.Gap;

public sealed class WinAdvertisementScanner : IAdvertisementScanner
{
    private readonly IBleAdapter _bleAdapter;
    private readonly ILogger? _logger;
    private readonly CancellationToken _globalToken;
    private bool _verbose;

    private BluetoothLEAdvertisementWatcher? _watcher;
    private IObservable<GapAdvertisement>? _observable;
    private BetterScanner? _betterScanner;
    private CancellationToken _scanToken;

    public WinAdvertisementScanner(IBleAdapter bleAdapter, ILogger? logger)
    {
        _bleAdapter = bleAdapter;
        _logger = logger;
        _globalToken = default;
    }

    public bool IsScanning => _watcher is not null && _observable is not null;

    private GapAdvertisement OnAdvertisementReport(
        IEventPattern<BluetoothLEAdvertisementWatcher, BluetoothLEAdvertisementReceivedEventArgs> gapEvt)
    {
        BluetoothLEAdvertisementReceivedEventArgs eventArgs = gapEvt.EventArgs;
            (SectionType, byte[])[] dataSections = eventArgs.Advertisement.DataSections
                .Where(data =>
                {
                    bool defined = Enum.IsDefined(typeof(SectionType), data.DataType);
                    if (!defined && _verbose)
                        _logger?.LogWarning("Data section {@Data} has invalid section data type {SectionType}",
                            data.Data.ToArray(), data.DataType);
                    return defined;
                })
                .Select(section => ((SectionType)section.DataType, section.Data.ToArray()))
                .ToArray();
            Dictionary<CompanyUuid, byte[]> manufacturerData = eventArgs.Advertisement.ManufacturerData
                .Where(data =>
                {
                    bool defined = Enum.IsDefined(typeof(CompanyUuid), data.CompanyId);
                    if (!defined && _verbose)
                        _logger?.LogWarning("ManufacturerData {@Data} has invalid CompanyId 0x{CompanyId:x2}",
                            new { Data = data.Data.ToArray(), data.CompanyId }, data.CompanyId);
                    return defined;
                })
                .ToDictionary(data => (CompanyUuid)data.CompanyId, data => data.Data.ToArray());

            return new GapAdvertisement(
                _bleAdapter,
                eventArgs.Timestamp.UtcDateTime,
                (AdvertisementType)eventArgs.AdvertisementType,
                eventArgs.BluetoothAddress,
                eventArgs.IsAnonymous,
                eventArgs.IsConnectable,
                eventArgs.IsDirected,
                eventArgs.IsScanResponse,
                eventArgs.IsScannable,
                (AddressType)eventArgs.BluetoothAddressType,
                eventArgs.RawSignalStrengthInDBm,
                eventArgs.TransmitPowerLevelInDBm,
                eventArgs.Advertisement.LocalName,
                dataSections,
                manufacturerData,
                (AdvertisementFlags)(eventArgs.Advertisement.Flags ?? 0),
                eventArgs.Advertisement.ServiceUuids.ToArray());
    }

    public IAdvertisementScanner Start(ScanningMode mode,
        float scanIntervalMs,
        float scanWindowMs,
        CancellationToken cancellationToken)
    {
        var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(_globalToken, cancellationToken);
        _scanToken = tokenSource.Token;

        _watcher = new BluetoothLEAdvertisementWatcher
        {
            ScanningMode = mode.ToWinScanningMode()
        };
        // Subscription needed for the watcher to start (observable will only subscribe later)
        _watcher.Received += (_, _) => { };
        _observable = Observable.FromEventPattern<
                TypedEventHandler<BluetoothLEAdvertisementWatcher, BluetoothLEAdvertisementReceivedEventArgs>,
                BluetoothLEAdvertisementWatcher,
                BluetoothLEAdvertisementReceivedEventArgs>(
                addHandler => _watcher.Received += addHandler,
                removeHandler => _watcher.Received -= removeHandler)
            .Select(OnAdvertisementReport);
        if (mode is ScanningMode.Active or ScanningMode.Passive)
        {
            var interval = (ushort)(scanIntervalMs / 0.625f);
            var window = (ushort)(scanWindowMs / 0.625f);
            int type = mode switch
            {
                ScanningMode.Passive => 0,
                ScanningMode.Active => 1,
                _ => throw new ArgumentOutOfRangeException($"Invalid scanning mode {mode} for using custom interval")
            };
            _betterScanner = new BetterScanner(_logger);
            _ = _betterScanner.StartScanner(type, interval, window);
        }

        try
        {
            _watcher.Start();
            if (_watcher.Status is BluetoothLEAdvertisementWatcherStatus.Aborted)
            {
                _logger?.LogError(
                    "Watcher status is '{Status}' but should be 'Created'.\nTry restarting the bluetooth adapter!",
                    _watcher.Status);
                return this;
            }
        }
        catch (Exception e)
        {
            _logger?.LogCritical(e, "Could not start advertisement watcher because of {ErrorName}", e.GetType().Name);
            return this;
        }

        return this;
    }

    public IDisposable Subscribe(IObserver<IGapAdvertisement> observer)
    {
        if (_observable is null) return Disposable.Empty;
        return _observable
            .TakeWhile(_ => !_scanToken.IsCancellationRequested)
            .Subscribe(observer);
    }


    public IAdvertisementScanner Stop()
    {
        _watcher?.Stop();
        _betterScanner?.Close();
        _scanToken = default;
        _observable = null;
        _watcher = null;
        _betterScanner = null;
        return this;
    }
}