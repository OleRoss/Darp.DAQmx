using System;
using Darp.DAQmx.Task;
using static Darp.DAQmx.DaqMxException;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;

namespace Darp.DAQmx.Channel.CounterInput;

public class CIAngularPositionChannel : ICounterInputChannel
{
    private readonly IntPtr _taskHandle;
    public string PhysicalChannel { get; }
    public int NumberOfVirtualChannels => 1;
    public string Name { get; }
    public int CounterId { get; }


    public CIAngularPositionChannel(IntPtr taskHandle,
        string deviceIdentifier,
        int counterId,
        string nameToAssignToChannel)
    {
        _taskHandle = taskHandle;
        PhysicalChannel = $"{deviceIdentifier}/ctr{counterId}";
        CounterId = counterId;
        Name = nameToAssignToChannel;

        ThrowIfFailed(DAQmxCreateCIAngEncoderChan(
            taskHandle,
            PhysicalChannel,
            nameToAssignToChannel,
            CIEncoderDecodingType.X1,
            false ? 1 : 0,
            0,
            CIEncoderZIndexPhase.AHighBHigh,
            CIAngularEncoderUnits.Degrees,
            360,
            0,
            ""));
    }
}

public static class CIAngularPositionChannelExtensions
{
    public static CounterInputTask AddAngularPositionChannel(this ChannelCollection<CounterInputTask, ICounterInputChannel> channelCollection,
        string deviceIdentifier,
        int counterId,
        Action<CIAngularPositionChannel>? configuration = null)
    {
        var channel = new CIAngularPositionChannel(channelCollection.Task.Handle,
            deviceIdentifier,
            counterId,
            Guid.NewGuid().ToString());
        configuration?.Invoke(channel);
        channelCollection.Add(channel);
        return channelCollection.Task;
    }
}