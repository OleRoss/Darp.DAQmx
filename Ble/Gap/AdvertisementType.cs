namespace Ble.Gap;

public enum AdvertisementType
{
    /// <summary>The advertisement is undirected and indicates that the device is connectable and scannable. This advertisement type can carry data.</summary>
    ConnectableUndirected = 0,
    /// <summary>The advertisement is directed and indicates that the device is connectable but not scannable. This advertisement type cannot carry data.</summary>
    ConnectableDirected = 1,
    /// <summary>The advertisement is undirected and indicates that the device is scannable but not connectable. This advertisement type can carry data.</summary>
    ScannableUndirected = 2,
    /// <summary>The advertisement is undirected and indicates that the device is not connectable nor scannable. This advertisement type can carry data.</summary>
    NonConnectableUndirected = 3,
    /// <summary>This advertisement is a scan response to a scan request issued for a scannable advertisement. This advertisement type can carry data.</summary>
    ScanResponse = 4,
    /// <summary>This advertisement is a 5.0 extended advertisement. This advertisement type may have different properties, and is not necessarily directed, connected, scannable, nor a scan response.</summary>
    Extended = 5,
}