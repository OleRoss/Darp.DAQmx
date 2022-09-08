using Darp.DAQmx.Task;

namespace Darp.DAQmx.Timing;

public class Timing<TTask> where TTask : ITask<TTask>
{
    public TTask Task { get; }

    public Timing(TTask task) => Task = task;
}