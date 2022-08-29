using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Darp.DAQmx.NationalInstruments.Functions;

internal partial class Interop
{
    private static IntPtr _libHandle = IntPtr.Zero;
    private const string Lib = "DAQmx";

    static Interop()
    {
        NativeLibrary.SetDllImportResolver(typeof(Interop).Assembly, ImportResolver);
    }

    private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName != "DAQmx" || _libHandle != IntPtr.Zero)
            return _libHandle;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            NativeLibrary.TryLoad("C:/Windows/System32/nicaiu.dll", assembly, new DllImportSearchPath?(), out Interop._libHandle);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                 && !NativeLibrary.TryLoad("/usr/lib/x86_64-linux-gnu/libnidaqmx.so", assembly, new DllImportSearchPath?(), out Interop._libHandle))
            NativeLibrary.TryLoad("/usr/local/natinst/lib/libnidaqmx.so", assembly, new DllImportSearchPath?(), out Interop._libHandle);
        return _libHandle;
    }
}