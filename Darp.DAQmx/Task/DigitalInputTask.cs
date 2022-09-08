using System;
using Darp.DAQmx.Channel.DigitalInput;

namespace Darp.DAQmx.Task;

public interface IDigitalTask<TTask> : ITask<TTask>
    where TTask : IDigitalTask<TTask>
{

}

public class DigitalInputTask : AbstractTask<DigitalInputTask, IDigitalInputChannel>,
    IDigitalTask<DigitalInputTask>,
    IInputTask<DigitalInputTask, IDigitalInputChannel>
{
    public DigitalInputTask() : base(Guid.NewGuid().ToString()) { }
    public DigitalInputTask(string taskName) : base(taskName) { }
}