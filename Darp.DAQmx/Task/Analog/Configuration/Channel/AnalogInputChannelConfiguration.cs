using System;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task.Analog.Channel;
using Darp.DAQmx.Task.Common;
using Darp.DAQmx.Task.Common.Configuration;

namespace Darp.DAQmx.Task.Analog.Configuration.Channel;

public record AnalogInputVoltageChannelConfiguration(string ChannelName,
    AnalogInputChannel Channel,
    string DeviceIdentifier,
    InputTerminalConfiguration TerminalConfiguration,
    double MinVoltage,
    double MaxVoltage,
    DaqMxUnit Unit) : IChannelConfiguration
{
    public string PhysicalChannel => $"{DeviceIdentifier}/ai{Channel.Id}";
    public string ChannelName { get; set; } = ChannelName;
    public InputTerminalConfiguration TerminalConfiguration { get; set; } = TerminalConfiguration;
    public double MinVoltage { get; set; } = MinVoltage;
    public double MaxVoltage { get; set; } = MaxVoltage;
    public DaqMxUnit Unit { get; set; } = Unit;

    int IChannelConfiguration.Create(IntPtr taskHandle) => Interop.DAQmxCreateAIVoltageChan(
        taskHandle,
        PhysicalChannel,
        ChannelName,
        (int) TerminalConfiguration,
        MinVoltage,
        MaxVoltage,
        Unit.Value,
        Unit.CustomScaleName);
}