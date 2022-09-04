using System;
using Darp.DAQmx.Task;
using static Darp.DAQmx.DaqMxException;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;

namespace Darp.DAQmx.Channel.CounterInput;

public class CICountEdgesChannel : ICounterInputChannel
{
    private readonly IntPtr _taskHandle;
    public string PhysicalChannel { get; }
    public int NumberOfVirtualChannels => 1;
    public int CounterId { get; }
    public string Name { get; }

    public CICountEdgesChannel(IntPtr taskHandle,
        string deviceIdentifier,
        int counterId,
        string nameToAssignToChannel,
        CIActiveEdge edge,
        uint initialCount,
        CICountDirection countDirection)
    {
        _taskHandle = taskHandle;
        PhysicalChannel = $"{deviceIdentifier}/ctr{counterId}";
        CounterId = counterId;
        Name = nameToAssignToChannel;

        ThrowIfFailed(DAQmxCreateCICountEdgesChan(
            taskHandle,
            PhysicalChannel,
            nameToAssignToChannel,
            edge,
            initialCount,
            countDirection));
    }
}

public static class CICountEdgesChannelExtensions
{
    public static CounterInputTask AddCountEdgesChannel(this ChannelCollection<CounterInputTask, ICounterInputChannel> channelCollection,
        string deviceIdentifier,
        int analogInputId,
        Action<CICountEdgesChannel>? configuration = null)
    {
        var channel = new CICountEdgesChannel(channelCollection.Task.Handle,
            deviceIdentifier,
            analogInputId,
            Guid.NewGuid().ToString(),
            CIActiveEdge.Rising,
            0,
            CICountDirection.Up);
        configuration?.Invoke(channel);
        channelCollection.Add(channel);
        return channelCollection.Task;
    }
}