using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Reader;

public class MultiChannelReader<TTask, TChannel> : IReader<TTask, TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    public MultiChannelReader(TTask task)
    {
        if (task.Channels.Count < 2)
            throw new ArgumentOutOfRangeException(nameof(task),
                $"Expected task to have more than 1 channels, but found {task.Channels.Count}");
        Task = task;
        Channels = new ReadOnlyCollection<TChannel>(task.Channels);
    }

    public TTask Task { get; }
    public IReadOnlyList<TChannel> Channels { get; }
    public int ChannelCount => Channels.Count;
}