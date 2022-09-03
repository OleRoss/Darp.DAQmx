using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Reader;

public class SingleChannelReader<TTask, TChannel> : IReader<TTask, TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    public SingleChannelReader(TTask task)
    {
        if (task.Channels.Count != 1)
            throw new ArgumentOutOfRangeException(nameof(task),
                $"Expected task to have 1 channel, but found {task.Channels.Count}");
        Task = task;
        Channels = new ReadOnlyCollection<TChannel>(task.Channels);
    }

    public IReadOnlyList<TChannel> Channels { get; }
    public TTask Task { get; }
}