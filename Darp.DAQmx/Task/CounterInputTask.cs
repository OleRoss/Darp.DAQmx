using System;
using Darp.DAQmx.Channel.CounterInput;

namespace Darp.DAQmx.Task;

public interface ICounterTask<TTask> : ITask<TTask>
    where TTask : ICounterTask<TTask>
{

}

public class CounterInputTask : AbstractTask<CounterInputTask, ICounterInputChannel>,
    ICounterTask<CounterInputTask>,
    IInputTask<CounterInputTask, ICounterInputChannel>
{
    public CounterInputTask() : base(Guid.NewGuid().ToString()) { }

    public CounterInputTask(string taskName) : base(taskName) { }
}