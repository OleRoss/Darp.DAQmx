using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task.Common;
using Darp.DAQmx.Task.Common.Configuration;
using Darp.DAQmx.Task.Digital.Configuration.Channel;

namespace Darp.DAQmx.Task;

public abstract class DaqMxTask<TTask> : IDisposable
    where TTask : DaqMxTask<TTask>
{
    public IntPtr TaskHandle { get; }
    public int ChannelCount { get; }

    protected DaqMxTask(string taskName, ICollection<IChannelConfiguration> channelConfigurations)
    {
        int createTaskStatus = Interop.DAQmxCreateTask(taskName, out IntPtr taskHandle);
        if (createTaskStatus < 0)
            throw new DaqMxException(createTaskStatus, "Could not create Task");
        TaskHandle = taskHandle;
        ChannelCount = channelConfigurations.Count;
        foreach (IChannelConfiguration channelConfiguration in channelConfigurations)
        {
            int status = channelConfiguration.Create(TaskHandle);
            if (status < 0)
                throw new DaqMxException(status, $"Could not create channel {channelConfiguration} \n");
        }
    }

    public void Dispose()
    {
    }
}



