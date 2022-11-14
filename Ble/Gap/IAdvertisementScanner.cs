using System;
using System.Threading;

namespace Ble.Gap;

public interface IAdvertisementScanner : IObservable<IGapAdvertisement>
{
    bool IsScanning { get; }
    IAdvertisementScanner Start(ScanningMode mode,
        float scanIntervalMs,
        float scanWindowMs,
        CancellationToken cancellationToken = default);
    IAdvertisementScanner Stop();
}