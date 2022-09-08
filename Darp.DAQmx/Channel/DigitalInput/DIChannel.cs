using System;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;

using static Darp.DAQmx.DaqMxException;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;

namespace Darp.DAQmx.Channel.DigitalInput;

public class DIChannel : IDigitalInputChannel
{
    private readonly IntPtr _taskHandle;
    public string PhysicalChannel { get; }
    public int NumberOfVirtualChannels { get; }
    public string Name { get; }
    public int Port { get; }
    public int FirstLine { get; }
    public int LastLine { get; }

    public DIChannel(IntPtr taskHandle,
        string deviceIdentifier,
        int port,
        int firstLine,
        int lastLine,
        string nameToAssignToChannel,
        ChannelLineGrouping lineGrouping)
    {
        _taskHandle = taskHandle;
        Port = port;
        FirstLine = firstLine;
        LastLine = lastLine;
        string lineStr = firstLine == lastLine
            ? $"line{firstLine}"
            : $"line{firstLine}:{lastLine}";
        PhysicalChannel = $"{deviceIdentifier}/port{port}/{lineStr}";
        NumberOfVirtualChannels = lineGrouping is ChannelLineGrouping.OneChannelForAllLines
            ? 1
            : lastLine - firstLine + 1;
        Name = nameToAssignToChannel;

        ThrowIfFailed(DAQmxCreateDIChan(
            taskHandle,
            PhysicalChannel,
            nameToAssignToChannel,
            lineGrouping));
    }

    public bool InvertLines
    {
        get => ThrowIfFailedOrReturn(DAQmxGetDIInvertLines(_taskHandle, Name, out int data), data == 1);
        set => ThrowIfFailed(DAQmxSetDIInvertLines(_taskHandle, Name, value ? 1 : 0));
    }
    public void ResetInvertLines() => ThrowIfFailed(DAQmxResetDIInvertLines(_taskHandle, Name));
}

public static class DIChannelExtensions
{
    public static DigitalInputTask AddChannel(
        this ChannelCollection<DigitalInputTask, IDigitalInputChannel> channelCollection,
        string deviceIdentifier,
        int portId,
        int line,
        Action<DIChannel>? configuration = null) =>
        channelCollection.AddChannel(deviceIdentifier, portId, line, line, configuration);

    public static DigitalInputTask AddChannel(this ChannelCollection<DigitalInputTask, IDigitalInputChannel> channelCollection,
        string deviceIdentifier,
        int portId,
        int firstLine,
        int lastLine,
        Action<DIChannel>? configuration = null)
    {
        var channel = new DIChannel(channelCollection.Task.Handle,
            deviceIdentifier,
            portId,
            firstLine,
            lastLine,
            "",
            ChannelLineGrouping.OneChannelForEachLine);
        configuration?.Invoke(channel);
        channelCollection.Add(channel);
        return channelCollection.Task;
    }
}