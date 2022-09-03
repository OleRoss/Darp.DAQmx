using System;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;

using static Darp.DAQmx.DaqMxException;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;

namespace Darp.DAQmx.Channel.DigitalInput;

public class DIChannel : IDigitalInputChannel
{
    public DIChannel(IntPtr taskHandle,
        string deviceIdentifier,
        int port,
        int firstLine,
        int lastLine,
        string nameToAssignToChannel,
        ChannelLineGrouping lineGrouping)
    {
        Port = port;
        FirstLine = firstLine;
        LastLine = lastLine;
        string lineStr = firstLine == lastLine
            ? $"line{firstLine}"
            : $"line{firstLine}:{lastLine}";
        PhysicalChannel = $"{deviceIdentifier}/port{port}/{lineStr}";

        ThrowIfFailed(DAQmxCreateDIChan(
            taskHandle,
            PhysicalChannel,
            nameToAssignToChannel,
            lineGrouping));
    }

    public string PhysicalChannel { get; }
    public int Port { get; }
    public int FirstLine { get; }
    public int LastLine { get; }
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
            Guid.NewGuid().ToString(),
            ChannelLineGrouping.OneChannelForAllLines);
        configuration?.Invoke(channel);
        channelCollection.Add(channel);
        return channelCollection.Task;
    }

    public static SingleChannelReader<DigitalInputTask, IDigitalInputChannel> GetSingleReader(
        this ChannelCollection<DigitalInputTask, IDigitalInputChannel> channel) => new(channel.Task);
    public static MultiChannelReader<DigitalInputTask, IDigitalInputChannel> GetMultiReader(
        this ChannelCollection<DigitalInputTask, IDigitalInputChannel> channel) => new(channel.Task);
}