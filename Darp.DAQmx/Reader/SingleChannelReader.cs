using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Reader;

public class SingleChannelReader<TTask, TChannel> : ISingleChannelReader<TTask, TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    public SingleChannelReader(TTask task)
    {
        Task = task;
        Channels = new ReadOnlyCollection<TChannel>(task.Channels);
        ChannelCount = task.Channels.ChannelCount;

        if (ChannelCount != 1)
            throw new ArgumentOutOfRangeException(nameof(task),
                $"Expected task to have 1 channel, but found {ChannelCount}");
    }

    public IReadOnlyList<TChannel> Channels { get; }
    public TTask Task { get; }
    public int ChannelCount { get; }
}