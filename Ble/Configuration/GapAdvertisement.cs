using System;
using Ble.Utils;

namespace Ble.Configuration;

public class GapAdvertisement
{
    public GapAdvertisement(Peripheral peripheral)
    {
        Services = new GenericCollection<Guid, Peripheral>(peripheral);
    }

    public GenericCollection<Guid, Peripheral> Services { get; }
}

public static class GuidCollectionExtensions
{
    public static TReturn Add<TReturn>(this GenericCollection<Guid, TReturn> guidCollection, string guid)
    {
        return guidCollection.Add(guid.ToBleGuid());
    }

    public static TReturn Add<TReturn>(this GenericCollection<Guid, TReturn> guidCollection, GattService gattService)
    {
        return guidCollection.Add(gattService.ServiceUuid);
    }
}