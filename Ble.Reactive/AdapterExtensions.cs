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
        CancellationToken cancellationToken = default)
    {
        return adapter.Scanner()
            .Start()
            .Where(configuration);
    }

    public static IObservable<IConnectedPeripheral> ScanAndConnect(this IBleAdapter adapter, Configuration configuration)
    {
        return adapter.Scanner()
            .Start()
            .Where(configuration)
            .Connect(configuration);
    }

    public static Task<IConnectedPeripheral> ScanAndConnectFirstAsync(this IBleAdapter adapter,
        Configuration configuration,
        CancellationToken cancellationToken = default)
    {
        return adapter.Scanner()
            .Start()
            .Where(configuration)
            .Connect(configuration)
            .FirstAsync()
            .ToTask(cancellationToken);
    }
}