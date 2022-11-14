using System;

namespace Ble.Config;

public class Advertisement
{
    public Advertisement(Configuration configuration)
    {
        Services = new GenericCollection<Guid, Configuration>(configuration);
    }

    public GenericCollection<Guid, Configuration> Services { get; }
}