using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Ble.Config;
using Ble.Gap;
using Ble.Gatt;

namespace Ble.Reactive;

public static class AdapterExtensions
{
    public static IObservable<IGapAdvertisement> Scan(this IBleAdapter adapter,
        Configuration configuration,
        ScanningMode mode = ScanningMode.Passive,
        float scanIntervalMs = 1000,
        float scanWindowMs = 1000,
        CancellationToken cancellationToken = default)
    {
        return adapter.Scanner()
            .Start(mode, scanIntervalMs, scanWindowMs)
            .Where(configuration);
    }

    public static IObservable<IConnectedPeripheral> ScanAndConnect(this IBleAdapter adapter,
        Configuration configuration,
        ScanningMode mode = ScanningMode.Passive,
        float scanIntervalMs = 1000,
        float scanWindowMs = 1000)
    {
        return adapter.Scanner()
            .Start(mode, scanIntervalMs, scanWindowMs)
            .Where(configuration)
            .Connect(configuration);
    }

    public static Task<IConnectedPeripheral> ScanAndConnectFirstAsync(this IBleAdapter adapter,
        Configuration configuration,
        ScanningMode mode = ScanningMode.Passive,
        float scanIntervalMs = 1000,
        float scanWindowMs = 1000,
        CancellationToken cancellationToken = default)
    {
        return adapter.Scanner()
            .Start(mode, scanIntervalMs, scanWindowMs)
            .Where(configuration)
            .Connect(configuration)
            .FirstAsync()
            .ToTask(cancellationToken);
    }
}