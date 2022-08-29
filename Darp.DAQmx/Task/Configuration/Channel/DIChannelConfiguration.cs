using System;
using Darp.DAQmx.NationalInstruments.Enums;
using Darp.DAQmx.NationalInstruments.Functions;

namespace Darp.DAQmx.Task.Configuration.Channel;

public interface IDIChannelConfiguration : IChannelConfiguration
{
    int FirstLine { get; }
    int LastLine { get; }
    int PortWidth { get; }
}

public record DIChannelLinesConfiguration(string ChannelName,
    string DeviceIdentifier,
    int FirstLine,
    int LastLine,
    int PortWidth) : IDIChannelConfiguration
{
    public string ChannelName { get; set; } = ChannelName;
    public DaqMxLines LineGrouping { get; init; } = DaqMxLines.ChannelsPerLine;
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

public record DIChannelPortLinesConfiguration(string ChannelName,
    DigitalInputChannel Channel,
    string DeviceIdentifier,
    int FirstLine,
    int LastLine,
    DaqMxLines LineGrouping) : IDIChannelConfiguration
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
            return $"{DeviceIdentifier}/{Channel}/{line}";
        }
    }

    public int PortWidth { get; } = Channel.Width;
}