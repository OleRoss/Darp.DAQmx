using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Reader;

public class ChannelReader<TTask, TChannel> : IChannelReader<TTask, TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    public ChannelReader(TTask task)
    {
        Channels = new ReadOnlyCollection<TChannel>(task.Channels);
        ChannelCount = task.Channels.ChannelCount;
        Task = task;

        if (ChannelCount < 2)
            throw new ArgumentOutOfRangeException(nameof(task),
                $"Expected task to have at least 2 channels, but found {ChannelCount}");
    }

    public TTask Task { get; }
    public IReadOnlyList<TChannel> Channels { get; }
    public int ChannelCount { get; }
}