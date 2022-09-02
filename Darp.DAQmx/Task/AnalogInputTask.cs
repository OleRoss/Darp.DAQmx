using System;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Channel.AnalogInput;
using Darp.DAQmx.NationalInstruments.Functions;

namespace Darp.DAQmx.Task;

public class AnalogInputTask : IInputTask<AnalogInputTask, IAnalogInputChannel>
{
    public AnalogInputTask() : this(Guid.NewGuid().ToString()) {}
    public AnalogInputTask(string taskName)
    {
        Channels = new ChannelCollection<AnalogInputTask, IAnalogInputChannel>(this);
        DaqMxException.ThrowIfFailed(Interop.DAQmxCreateTask(taskName, out IntPtr handle));
        Handle = handle;
    }

    public ChannelCollection<AnalogInputTask, IAnalogInputChannel> Channels { get; }
    public SingleChannelReader<AnalogInputTask> GetSingleReader() => new(this);
    public MultiChannelReader<AnalogInputTask, IAnalogInputChannel> GetReader() => new(this);

    public IntPtr Handle { get; }

    public void Dispose()
    {
    }
}