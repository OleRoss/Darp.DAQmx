using System;
using Darp.DAQmx.Channel;

namespace Darp.DAQmx.Task;

public interface ITask<TTask, TChannel> : IDisposable
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    IntPtr Handle { get; }
    ChannelCollection<TTask, TChannel> Channels { get; }
}