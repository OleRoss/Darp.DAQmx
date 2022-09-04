using System;
using Darp.DAQmx.Channel.CounterInput;

namespace Darp.DAQmx.Task;

public class CounterInputTask : AbstractTask<CounterInputTask, ICounterInputChannel>,
    IInputTask<CounterInputTask, ICounterInputChannel>
{
    public CounterInputTask() : base(Guid.NewGuid().ToString()) { }

    public CounterInputTask(string taskName) : base(taskName) { }
}