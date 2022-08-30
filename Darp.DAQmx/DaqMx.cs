using System;
using System.Collections.Generic;
using System.Text;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task.Common;
using Darp.DAQmx.Task.Common.Configuration;

namespace Darp.DAQmx;

public static class DaqMx
{
    public static TaskConfiguration CreateTaskFromDevice(string deviceIdentifier) => new(
        Guid.NewGuid().ToString(),
        new Device.Device(deviceIdentifier),
        new List<IChannelConfiguration>()
    );

    public static string GetErrorString(int errorCode)
    {
        var errorString = new StringBuilder(256);
        var status = Interop.DAQmxGetErrorString(errorCode, errorString, (uint)(errorString.Capacity + 1));
        return status == 0
            ? errorString.ToString()
            : $"Could not get errorString for {errorCode}. Resulted in {status}";
    }
}