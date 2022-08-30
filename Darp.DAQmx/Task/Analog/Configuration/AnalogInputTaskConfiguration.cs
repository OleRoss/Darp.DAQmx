using System.Collections.Generic;
using Darp.DAQmx.Task.Common.Configuration;

namespace Darp.DAQmx.Task.Analog.Configuration;

public readonly struct AnalogInputTaskConfiguration
    : IAnalogInputTaskConfiguration<AnalogInputTaskConfiguration>
{
    public ICollection<IChannelConfiguration> ChannelConfigurations { get; }
    public Device.Device Device { get; }
    public string TaskName { get; }

    public AnalogInputTaskConfiguration(string taskName,
        Device.Device deviceIdentifier,
        ICollection<IChannelConfiguration> channelConfigurations)
    {
        TaskName = taskName;
        Device = deviceIdentifier;
        ChannelConfigurations = channelConfigurations;
    }

    public AnalogInputTask CreateTask() => new(TaskName, ChannelConfigurations);

    public AnalogInputTaskConfiguration WithTaskName(string taskName) =>
        new(taskName, Device, ChannelConfigurations);
}