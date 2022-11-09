using System;
using System.Linq;
using System.Reactive.Linq;
using Ble.Configuration;
using Ble.Gap;

namespace Ble.Connection;

public static class ConnectionExtensions
{
    public static IObservable<IConnection> Connect(this IObservable<IAdvertisement> observable, Peripheral peripheral)
    {
        return observable
            .Where(peripheral)
            .Select(adv => Observable.FromAsync(async token => await adv.ConnectAsync(peripheral, token)))
            .Concat()
            .Where(x => x is not null)
            .Select(x => x!);
    }

    public static IObservable<IConnection> Connect(this IObservable<IAdvertisement> observable)
    {
        return observable
            .Select(adv => Observable.FromAsync(async token => await adv.ConnectAsync(token)))
            .Concat()
            .Where(x => x is not null)
            .Select(x => x!);
    }

    public static IObservable<IAdvertisement> Where(this IObservable<IAdvertisement> observable, Peripheral peripheral)
    {
        if (peripheral.Advertisement.Services.Count > 0)
        {
            observable = observable.Where(x =>
            {
                var res = peripheral.Advertisement.Services.All(advertisementService =>
                    x.ServiceUuids.Contains(advertisementService));
                if (x.ServiceUuids.Length > 0)
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