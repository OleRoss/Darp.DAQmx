using System;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task.Common;
using Darp.DAQmx.Task.Common.Configuration;
using Darp.DAQmx.Task.Digital.Channel;

namespace Darp.DAQmx.Task.Digital.Configuration.Channel;

public record DigitalInputChannelPortLinesConfiguration(string ChannelName,
    DigitalInputChannel Channel,
    string DeviceIdentifier,
    int FirstLine,
    int LastLine,
    ChannelLineMode LineGrouping) : IDigitalInputChannelConfiguration
{
    public string ChannelName { get; set; } = ChannelName;
    int IChannelConfiguration.Create(IntPtr taskHandle) => Interop.DAQmxCreateDIChan(
        taskHandle,
        PhysicalChannel,
        ChannelName,
        (int) LineGrouping);

    public string PhysicalChannel
    {
        get {
            string line = FirstLine == LastLine
                ? $"line{FirstLine}"
                : $"line{FirstLine}:{LastLine}";
            return $"{DeviceIdentifier}/port{Channel.Id}/{line}";
        }
    }

    public int PortWidth { get; } = Channel.Width;
}