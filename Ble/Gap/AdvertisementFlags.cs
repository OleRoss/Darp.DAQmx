using System;

namespace Ble.Gap;

[Flags]
public enum AdvertisementFlags : uint
{
    /// <summary>Specifies no flag.</summary>
    None = 0,
    /// <summary>Specifies Bluetooth LE Limited Discoverable Mode.</summary>
    LimitedDiscoverableMode = 1,
    /// <summary>Specifies Bluetooth LE General Discoverable Mode.</summary>
    GeneralDiscoverableMode = 2,
    /// <summary>Specifies Bluetooth BR/EDR not supported.</summary>
    ClassicNotSupported = 4,
    /// <summary>Specifies simultaneous Bluetooth LE and BR/EDR to same device capable (controller).</summary>
    DualModeControllerCapable = 8,
    /// <summary>Specifies simultaneous Bluetooth LE and BR/EDR to same device capable (host)</summary>
    DualModeHostCapable = 16, // 0x00000010
}