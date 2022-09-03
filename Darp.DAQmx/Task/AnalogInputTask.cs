using System;
using Darp.DAQmx.Channel.AnalogInput;

namespace Darp.DAQmx.Task;

public class AnalogInputTask : AbstractTask<AnalogInputTask, IAnalogInputChannel>,
    IInputTask<AnalogInputTask, IAnalogInputChannel>
{
    public AnalogInputTask() : base(Guid.NewGuid().ToString()) { }
    public AnalogInputTask(string taskName) : base(taskName) { }
}