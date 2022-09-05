using System;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Timing;

namespace Darp.DAQmx.Task;

public interface ITask<TTask> : IDisposable
    where TTask : ITask<TTask>
{
    IntPtr Handle { get; }
    Timing<TTask> Timing { get; }
}

public interface ITask<TTask, TChannel> : ITask<TTask>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    ChannelCollection<TTask, TChannel> Channels { get; }
}