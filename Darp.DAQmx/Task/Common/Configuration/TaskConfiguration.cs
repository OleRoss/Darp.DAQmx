using System.Collections.Generic;
using Darp.DAQmx.Task.Analog.Configuration;
using Darp.DAQmx.Task.Digital.Configuration;

namespace Darp.DAQmx.Task.Common.Configuration;

public readonly struct TaskConfiguration
    : IDigitalInputTaskConfiguration<TaskConfiguration>,
        IAnalogInputTaskConfiguration<TaskConfiguration>
{
    public Device.Device Device { get; }
    public string TaskName { get; }
    public ICollection<IChannelConfiguration> ChannelConfigurations { get; }

    public TaskConfiguration(string taskName, Device.Device deviceIdentifier, ICollection<IChannelConfiguration> channelConfigurations)
    {
        Device = deviceIdentifier;
        TaskName = taskName;
        ChannelConfigurations = channelConfigurations;
    }

    public TaskConfiguration WithTaskName(string taskName) => new(taskName, Device, ChannelConfigurations);
}
