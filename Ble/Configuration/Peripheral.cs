namespace Ble.Configuration;

public sealed class Peripheral
{
    public Peripheral()
    {
        Services = new GenericCollection<GattService, Peripheral>(this);
        Advertisement = new GapAdvertisement(this);
    }

    public GenericCollection<GattService, Peripheral> Services { get; }
    public GapAdvertisement Advertisement { get; }
    
}