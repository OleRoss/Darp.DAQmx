using System;
using Darp.DAQmx.Channel.AnalogInput;

namespace Darp.DAQmx.Task;

public interface IAnalogTask<TTask> : ITask<TTask>
    where TTask : IAnalogTask<TTask>
{

}

public class AnalogInputTask : AbstractTask<AnalogInputTask, IAnalogInputChannel>,
    IAnalogTask<AnalogInputTask>,
    IInputTask<AnalogInputTask, IAnalogInputChannel>
{
    public AnalogInputTask() : base(Guid.NewGuid().ToString()) { }
    public AnalogInputTask(string taskName) : base(taskName) { }
}