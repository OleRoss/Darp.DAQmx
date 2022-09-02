namespace Darp.DAQmx;

public class SingleChannelReader<TTask>
{
    public SingleChannelReader(TTask task)
    {
        Task = task;
    }

    public TTask Task { get; }
}