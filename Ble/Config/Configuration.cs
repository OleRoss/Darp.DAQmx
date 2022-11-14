namespace Ble.Config;

public sealed class Configuration
{
    public Configuration()
    {
        Services = new GenericCollection<Service, Configuration>(this);
        Advertisement = new Advertisement(this);
    }

    public GenericCollection<Service, Configuration> Services { get; }
    public Advertisement Advertisement { get; }
    
}