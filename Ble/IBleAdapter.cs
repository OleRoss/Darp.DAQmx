using System;
using System.Threading;
using System.Threading.Tasks;
using Ble.Configuration;
using Ble.Connection;
using Ble.Gap;

namespace Ble;

public interface IBleAdapter : IDisposable
{
    Task<IConnection?> ConnectAsync(ulong bluetoothId, Peripheral? peripheral, CancellationToken cancellationToken = default);

    Task<IConnection?> ConnectAsync(ulong bluetoothId, CancellationToken cancellationToken) =>
        ConnectAsync(bluetoothId, null, cancellationToken);
    IObservable<IAdvertisement> Observe(ScanningMode mode,
        float scanIntervalMs,
        float scanWindowMs,
        CancellationToken cancellationToken = default);
    void Clear();
}