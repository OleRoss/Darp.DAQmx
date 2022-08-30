using System;
using Darp.DAQmx.NationalInstruments.Enums;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task.Channel;

namespace Darp.DAQmx.Task.Configuration.Channel;

public record AIVoltageChannelConfiguration(string ChannelName,
    AnalogInputChannel Channel,
    string DeviceIdentifier,
    InputTerminalConfiguration TerminalConfiguration,
    double MinVoltage,
    double MaxVoltage,
    DaqMxUnit Unit) : IChannelConfiguration
{
    public string PhysicalChannel => $"{DeviceIdentifier}/{Channel}";
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