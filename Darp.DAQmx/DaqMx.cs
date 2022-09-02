using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Darp.DAQmx.NationalInstruments.Functions;

namespace Darp.DAQmx;

public static class DaqMx
{
    // public static TaskConfiguration CreateTaskFromDevice(string deviceIdentifier) => new(
    //     Guid.NewGuid().ToString(),
    //     new Device.Device(deviceIdentifier),
    //     new List<IChannelConfiguration>()
    // );

    public static IReadOnlyList<string> GetDeviceNames() => DaqMxException.ThrowIfFailedOrReturnString(
        (in byte pointer, uint length) => Interop.DAQmxGetSysDevNames(pointer, length)).SplitNi();

    public static IReadOnlyList<Device> GetDevices() => GetDeviceNames()
        .Select(x => new Device(x))
        .ToArray();

    public static string GetErrorString(int errorCode)
    {
        var errorString = new StringBuilder(256);
        var status = Interop.DAQmxGetErrorString(errorCode, errorString, (uint)(errorString.Capacity + 1));
        return status == 0
            ? errorString.ToString()
            : $"Could not get errorString for {errorCode}. Resulted in {status}";
    }
}

public static class Extensions
{
    public static IReadOnlyList<string> SplitNi(this string str) => string.IsNullOrEmpty(str)
        ? Array.Empty<string>()
        : str.Split(',');
}