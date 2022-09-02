using System;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Channel.AnalogInput;

namespace Darp.DAQmx.Task;

public interface ITask<TTask, TChannel> : IDisposable
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    ChannelCollection<TTask, TChannel> Channels { get; }
    SingleChannelReader<TTask> GetSingleReader();
    MultiChannelReader<AnalogInputTask, IAnalogInputChannel> GetReader();
}