using System;
using Darp.DAQmx.Channel.DigitalInput;

namespace Darp.DAQmx.Task;

public class DigitalInputTask : AbstractTask<DigitalInputTask, IDigitalInputChannel>,
    IInputTask<DigitalInputTask, IDigitalInputChannel>
{
    public DigitalInputTask() : base(Guid.NewGuid().ToString()) { }
    public DigitalInputTask(string taskName) : base(taskName) { }
}