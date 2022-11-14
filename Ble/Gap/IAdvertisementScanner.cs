using System;
using System.Threading;

namespace Ble.Gap;

public interface IAdvertisementScanner : IObservable<IGapAdvertisement>
{
    bool IsScanning { get; }
    IAdvertisementScanner Start(CancellationToken cancellationToken = default);
    IAdvertisementScanner Stop();
    IAdvertisementScanner SetScanMode(ScanningMode mode);
    IAdvertisementScanner SetScanInterval(float scanIntervalMs);
    IAdvertisementScanner SetScanWindow(float scanWindowMs);
}

public static class AdvertisementScannerExtensions
{
    public static IAdvertisementScanner SetScanParameters(this IAdvertisementScanner scanner,
        float scanIntervalMs,
        float scanWindowMs)
    {
        if (scanWindowMs > scanIntervalMs)
            throw new ArgumentOutOfRangeException(nameof(scanWindowMs),
                $"Expected scanWindow to be smaller than scanInterval ({scanIntervalMs}), but is {scanWindowMs}");
        return scanner.SetScanInterval(scanIntervalMs).SetScanWindow(scanWindowMs);
    }
}