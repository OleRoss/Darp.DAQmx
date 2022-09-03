using System;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;
using static Darp.DAQmx.DaqMxException;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;

namespace Darp.DAQmx.Channel.AnalogInput;

public class AIVoltageChannel : IAnalogInputChannel
{
    private readonly IntPtr _taskHandle;
    public string PhysicalChannel { get; }
    public AIVoltageChannel(IntPtr taskHandle,
        string deviceIdentifier,
        int analogInputId,
        string nameToAssignToChannel,
        AITerminalConfiguration terminalConfiguration,
        double minVoltage,
        double maxVoltage,
        AIVoltageUnits units,
        string customScaleName)
    {
        _taskHandle = taskHandle;
        PhysicalChannel = $"{deviceIdentifier}/ai{analogInputId}";
        ThrowIfFailed(DAQmxCreateAIVoltageChan(
            taskHandle,
            PhysicalChannel,
            nameToAssignToChannel,
            terminalConfiguration,
            minVoltage,
            maxVoltage,
            units,
            customScaleName));
    }

    public AITerminalConfiguration TerminalConfiguration => ThrowIfFailedOrReturn(DAQmxGetPhysicalChanAITermCfgs(
        PhysicalChannel, out int terminalConfig), (AITerminalConfiguration)terminalConfig);

    public double MinValue
    {
        get => ThrowIfFailedOrReturn(DAQmxGetAIMin(_taskHandle, PhysicalChannel, out double minValue), minValue);
        set => ThrowIfFailed(DAQmxSetAIMin(_taskHandle, PhysicalChannel, value));
    }
    public void ResetMinValue() => ThrowIfFailed(DAQmxResetAIMin(_taskHandle, PhysicalChannel));
    public double MaxValue
    {
        get => ThrowIfFailedOrReturn(DAQmxGetAIMax(_taskHandle, PhysicalChannel, out double maxVoltage), maxVoltage);
        set => ThrowIfFailed(DAQmxSetAIMax(_taskHandle, PhysicalChannel, value));
    }
    public void ResetMaxValue() => ThrowIfFailed(DAQmxResetAIMax(_taskHandle, PhysicalChannel));

    public AIVoltageUnits Units
    {
        get => ThrowIfFailedOrReturn(DAQmxGetAIVoltageUnits(_taskHandle, PhysicalChannel, out int units), (AIVoltageUnits)units);
        set => ThrowIfFailed(DAQmxSetAIVoltageUnits(_taskHandle, PhysicalChannel, (int)value));
    }
    public void ResetUnits() => ThrowIfFailed(DAQmxResetAIVoltageUnits(_taskHandle, PhysicalChannel));

    public string CustomScaleName => ThrowIfFailedOrReturnString((in byte pointer, uint length) =>
        DAQmxGetAICustomScaleName(_taskHandle, PhysicalChannel, pointer, length));

    public void SetCustomScale(string customScaleName)
    {
        ThrowIfFailed(DAQmxSetAIVoltageUnits(_taskHandle, PhysicalChannel, (int)AIVoltageUnits.FromCustomScale));
        ThrowIfFailed(DAQmxSetAICustomScaleName(_taskHandle, PhysicalChannel, customScaleName.ToCharArray()));
    }
    public void ResetCustomScale()
    {
        ThrowIfFailed(DAQmxResetAIVoltageUnits(_taskHandle, PhysicalChannel));
        ThrowIfFailed(DAQmxResetAICustomScaleName(_taskHandle, PhysicalChannel));
    }

}

/// <summary>Specifies the units to use to return voltage measurements from the channel.</summary>
/// <remarks>Specifies the units to use to return voltage measurements from the channel.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.AIChannel.VoltageUnits" />.</remarks>
public enum AIVoltageUnits
{
    /// <summary>Units a <see href="javascript:launchSharedHelp('mxcncpts.chm::/customScales.html');">custom scale</see> specifies. If you select this value, you must specify a custom scale name.</summary>
    FromCustomScale = 10065, // 0x00002751
    /// <summary>Volts.</summary>
    Volts = 10348, // 0x0000286C
    /// <summary>Units defined by TEDS information associated with the channel.</summary>
    FromTeds = 12516, // 0x000030E4
}

public static class AIVoltageChannelExtensions
{
    public static AnalogInputTask AddVoltageChannel(this ChannelCollection<AnalogInputTask, IAnalogInputChannel> channelCollection,
        string deviceIdentifier,
        int analogInputId,
        Action<AIVoltageChannel>? configuration = null)
    {
        var channel = new AIVoltageChannel(channelCollection.Task.Handle,
            deviceIdentifier,
            analogInputId,
            Guid.NewGuid().ToString(),
            AITerminalConfiguration.Differential,
            -10,
            10,
            AIVoltageUnits.Volts,
            "");
        configuration?.Invoke(channel);
        channelCollection.Add(channel);
        return channelCollection.Task;
    }

    public static SingleChannelReader<AnalogInputTask, IAnalogInputChannel> GetSingleReader(
        this ChannelCollection<AnalogInputTask, IAnalogInputChannel> channel) => new(channel.Task);
    public static MultiChannelReader<AnalogInputTask, IAnalogInputChannel> GetMultiReader(
        this ChannelCollection<AnalogInputTask, IAnalogInputChannel> channel) => new(channel.Task);

}