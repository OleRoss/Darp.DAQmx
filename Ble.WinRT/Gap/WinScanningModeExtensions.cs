using System;
using Windows.Devices.Bluetooth.Advertisement;
using Ble.Gap;

namespace Ble.WinRT.Gap;

public static class WinScanningModeExtensions
{
    public static ScanningMode ToScanningMode(this BluetoothLEScanningMode scanningMode) => scanningMode switch
    {
        BluetoothLEScanningMode.Passive => ScanningMode.Passive,
        BluetoothLEScanningMode.Active => ScanningMode.Active,
        BluetoothLEScanningMode.None => ScanningMode.None,
        _ => throw new ArgumentOutOfRangeException(nameof(scanningMode))
    };

    public static BluetoothLEScanningMode ToWinScanningMode(this ScanningMode scanningMode) => scanningMode switch
    {
        ScanningMode.Passive => BluetoothLEScanningMode.Passive,
        ScanningMode.Active => BluetoothLEScanningMode.Active,
        ScanningMode.None => BluetoothLEScanningMode.None,
        _ => throw new ArgumentOutOfRangeException(nameof(scanningMode))
    };

    public static int ToInt(this BluetoothLEScanningMode scanningMode) => scanningMode switch
    {
        BluetoothLEScanningMode.Passive => 0,
        BluetoothLEScanningMode.Active => 1,
        BluetoothLEScanningMode.None => 2,
        _ => throw new ArgumentOutOfRangeException(nameof(scanningMode))
    };
}