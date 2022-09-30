using System.Reflection;
using System.Runtime.InteropServices;

namespace Darp.NrfBleDriver.Nrf;

internal static class InteropHelper
{
    public const string Lib = "nrf-ble-driver-sd_api_v5-mt-4_1_4";

    private static IntPtr _libHandle = IntPtr.Zero;

    public static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName != Lib || _libHandle != IntPtr.Zero)
            return _libHandle;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            NativeLibrary.TryLoad(@"C:\Users\VakuO\PycharmProjects\nordictests\venv\Lib\site-packages\pc_ble_driver_py\lib/nrf-ble-driver-sd_api_v5-mt-4_1_4.dll", assembly, new DllImportSearchPath?(), out _libHandle);
        return _libHandle;
    }
}