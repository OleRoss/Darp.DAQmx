using System;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task.Common;
using Darp.DAQmx.Task.Common.Configuration;

namespace Darp.DAQmx.Task.Digital.Configuration.Channel;

public record DigitalInputChannelLinesConfiguration(string ChannelName,
    string DeviceIdentifier,
    int FirstLine,
    int LastLine,
    int PortWidth) : IDigitalInputChannelConfiguration
{
    public string ChannelName { get; set; } = ChannelName;
    public ChannelLineMode LineGrouping { get; init; } = ChannelLineMode.ChannelsPerLine;
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
            return $"{DeviceIdentifier}/{line}";
        }
    }
}