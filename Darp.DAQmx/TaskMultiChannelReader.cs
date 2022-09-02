using System.Collections.Generic;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Task;

namespace Darp.DAQmx;

public class MultiChannelReader<TTask, TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    private readonly ICollection<TChannel> _channels;

    public MultiChannelReader(TTask task)
    {
        Task = task;
        _channels = task.Channels;
    }

    public TTask Task { get; }
    public int ChannelCount => _channels.Count;
}