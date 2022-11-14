using System.Reactive.Linq;
using Ble.Config;
using Ble.Gap;
using Ble.Gatt;

namespace Ble.Reactive;

public static class AdvertisementObservableExtensions
{
    public static IObservable<IConnectedPeripheral> Connect(this IObservable<IGapAdvertisement> observable, Configuration configuration)
    {
        return observable
            .Where(configuration)
            .Select(adv => Observable.FromAsync(async token => await adv.ConnectAsync(configuration, token)))
            .Concat()
            .Where(x => x is not null)
            .Select(x => x!);
    }

    public static IObservable<IConnectedPeripheral> Connect(this IObservable<IGapAdvertisement> observable)
    {
        return observable
            .Select(adv => Observable.FromAsync(async token => await adv.ConnectAsync(token)))
            .Concat()
            .Where(x => x is not null)
            .Select(x => x!);
    }

    public static IObservable<IGapAdvertisement> Where(this IObservable<IGapAdvertisement> observable, Configuration configuration)
    {
        if (configuration.Advertisement.Services.Count > 0)
        {
            observable = observable.Where(x =>
            {
                var res = configuration.Advertisement.Services.All(advertisementService =>
                    x.Services.Contains(advertisementService));
                if (x.Services.Length > 0)
                {
                    int i = 0;
                }
                if (res)
                {
                    int i = 0;
                }

                return res;
            });
        }
        return observable;
    }
}