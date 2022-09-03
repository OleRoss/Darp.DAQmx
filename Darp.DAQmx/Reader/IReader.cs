using System.Collections.Generic;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Reader;

public interface IReader<out TTask, out TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    IReadOnlyList<TChannel> Channels { get; }
    TTask Task { get; }
}