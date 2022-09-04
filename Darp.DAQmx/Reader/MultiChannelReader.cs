using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Reader;

public class MultiChannelReader<TTask, TChannel> : IReader<TTask, TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    public MultiChannelReader(TTask task)
    {
        Channels = new ReadOnlyCollection<TChannel>(task.Channels);
        ChannelCount = Channels.Sum(x => x.NumberOfVirtualChannels);
        Task = task;

        if (ChannelCount < 2)
            throw new ArgumentOutOfRangeException(nameof(task),
                $"Expected task to have more than 1 channels, but found {ChannelCount}");
    }

    public TTask Task { get; }
    public IReadOnlyList<TChannel> Channels { get; }
    public int ChannelCount { get; }
}