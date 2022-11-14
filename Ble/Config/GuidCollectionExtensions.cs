using System;
using Ble.Uuid;

namespace Ble.Config;

public static class GuidCollectionExtensions
{
    public static TReturn Add<TReturn>(this GenericCollection<Guid, TReturn> guidCollection, string guid)
    {
        return guidCollection.Add(guid.ToBleGuid());
    }

    public static TReturn Add<TReturn>(this GenericCollection<Guid, TReturn> guidCollection, Service service)
    {
        return guidCollection.Add(service.ServiceUuid);
    }
}