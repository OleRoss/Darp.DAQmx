using System;
using System.Collections.Generic;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Event;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Timing;
using static Darp.DAQmx.DaqMxException;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;

namespace Darp.DAQmx.Task;

public abstract class AbstractTask<TTask, TChannel> : ITask<TTask, TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{
    /// Collection to store delegates to prevent GC
    private readonly ICollection<DAQmxEveryNSamplesEventCallbackPtr> _callbackPtrs;
    public AbstractTask(string taskName)
    {
        Channels = new ChannelCollection<TTask, TChannel>((TTask)(object)this);
        Timing = new Timing<TTask>((TTask)(object)this);
        ThrowIfFailed(DAQmxCreateTask(taskName, out IntPtr handle));
        Handle = handle;
        _callbackPtrs = new List<DAQmxEveryNSamplesEventCallbackPtr>();
    }
    public IntPtr Handle { get; }
    public ChannelCollection<TTask, TChannel> Channels { get; }
    public Timing<TTask> Timing { get; }
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
        Stop();
        ThrowIfFailed(DAQmxClearTask(Handle));
    }

    public delegate void SampleCallback(IChannelReader<TTask, TChannel> channelReader, int nSamples);
    public TTask OnEveryNSamplesRead(int nSamples, SampleCallback callback)
    {
        IChannelReader<TTask, TChannel> channelReader = Channels.GetReader();

        // It is important to create the delegate like this and save add it to the _callbackPtrs list to prevent garbage collection of the delegate
        DAQmxEveryNSamplesEventCallbackPtr callbackDelegate = (_, _, samples, _) =>
        {
            callback(channelReader, (int) samples);
            return 0;
        };
        _callbackPtrs.Add(callbackDelegate);

        ThrowIfFailed(DAQmxRegisterEveryNSamplesEvent(Handle,
            EveryNSamplesEventType.AcquiredIntoBuffer, (uint) nSamples, 0,
            callbackDelegate, IntPtr.Zero));
        return (TTask)(object)this;
    }
}