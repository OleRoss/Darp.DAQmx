using System;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task;

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
        DaqMxException.ThrowIfFailed(Interop.DAQmxCreateAIVoltageChan(
            taskHandle,
            PhysicalChannel,
            nameToAssignToChannel,
            terminalConfiguration,
            minVoltage,
            maxVoltage,
            units,
            customScaleName));
    }

    public AITerminalConfiguration TerminalConfiguration => DaqMxException.ThrowIfFailedOrReturn(
        Interop.DAQmxGetPhysicalChanAITermCfgs(PhysicalChannel, out int terminalConfig), (AITerminalConfiguration)terminalConfig);

    public double MinValue
    {
        get => DaqMxException.ThrowIfFailedOrReturn(Interop.DAQmxGetAIMin(_taskHandle, PhysicalChannel, out double minValue), minValue);
        set => DaqMxException.ThrowIfFailed(Interop.DAQmxSetAIMin(_taskHandle, PhysicalChannel, value));
    }
    public void ResetMinValue() => DaqMxException.ThrowIfFailed(Interop.DAQmxResetAIMin(_taskHandle, PhysicalChannel));
    public double MaxValue
    {
        get => DaqMxException.ThrowIfFailedOrReturn(Interop.DAQmxGetAIMax(_taskHandle, PhysicalChannel, out double maxVoltage), maxVoltage);
        set => DaqMxException.ThrowIfFailed(Interop.DAQmxSetAIMax(_taskHandle, PhysicalChannel, value));
    }
    public void ResetMaxValue() => DaqMxException.ThrowIfFailed(Interop.DAQmxResetAIMax(_taskHandle, PhysicalChannel));

    public AIVoltageUnits Units
    {
        get => DaqMxException.ThrowIfFailedOrReturn(Interop.DAQmxGetAIVoltageUnits(_taskHandle, PhysicalChannel, out int units), (AIVoltageUnits)units);
        set => DaqMxException.ThrowIfFailed(Interop.DAQmxSetAIVoltageUnits(_taskHandle, PhysicalChannel, (int)value));
    }
    public void ResetUnits() => DaqMxException.ThrowIfFailed(Interop.DAQmxResetAIVoltageUnits(_taskHandle, PhysicalChannel));


    public string CustomScaleName
    {
        get {
            var buffer = new char[128];
            return DaqMxException.ThrowIfFailedOrReturn(
                Interop.DAQmxGetAICustomScaleName(_taskHandle, PhysicalChannel, buffer, (uint)buffer.Length),
                new string(buffer));
        }
    }
    public void SetCustomScale(string customScaleName)
    {
        DaqMxException.ThrowIfFailed(Interop.DAQmxSetAIVoltageUnits(_taskHandle, PhysicalChannel, (int)AIVoltageUnits.FromCustomScale));
        DaqMxException.ThrowIfFailed(Interop.DAQmxSetAICustomScaleName(_taskHandle, PhysicalChannel, customScaleName.ToCharArray()));
    }
    public void ResetCustomScale()
    {
        DaqMxException.ThrowIfFailed(Interop.DAQmxResetAIVoltageUnits(_taskHandle, PhysicalChannel));
        DaqMxException.ThrowIfFailed(Interop.DAQmxResetAICustomScaleName(_taskHandle, PhysicalChannel));
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
}