using System;
using Darp.DAQmx.Channel;
using Darp.DAQmx.NationalInstruments.Functions;
using static Darp.DAQmx.DaqMxException;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;

namespace Darp.DAQmx.Task;

public abstract class AbstractTask<TTask, TChannel> : ITask<TTask, TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    public AbstractTask(string taskName)
    {
        Channels = new ChannelCollection<TTask, TChannel>((TTask)(object)this);
        ThrowIfFailed(DAQmxCreateTask(taskName, out IntPtr handle));
        Handle = handle;
    }
    public IntPtr Handle { get; }
    public ChannelCollection<TTask, TChannel> Channels { get; }

    public bool IsTaskDone() => ThrowIfFailedOrReturn(DAQmxIsTaskDone(Handle, out int isTaskDone), isTaskDone == 1);
    public void Control(TaskAction action) => ThrowIfFailed(DAQmxTaskControl(Handle, action));

    public void Start() => ThrowIfFailed(DAQmxStartTask(Handle));
    public void Stop() => ThrowIfFailed(DAQmxStopTask(Handle));

    /// <summary>
    /// Clears the task. Before clearing, this function aborts the task, if necessary, and releases any resources reserved by the task.
    /// You cannot use a task once you clear the task without recreating or reloading the task.
    /// If you use the DAQmxCreateTask function or any of the NI-DAQmx Create Channel functions within a loop,
    /// use this function within the loop after you finish with the task to avoid allocating unnecessary memory.
    /// </summary>
    /// <seealso cref="Interop.DAQmxClearTask"/>
    public void Dispose()
    {
        ThrowIfFailed(DAQmxClearTask(Handle));
    }
}