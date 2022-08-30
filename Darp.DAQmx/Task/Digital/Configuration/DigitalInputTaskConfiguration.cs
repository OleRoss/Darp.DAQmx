using System.Collections.Generic;
using Darp.DAQmx.Task.Common.Configuration;

namespace Darp.DAQmx.Task.Digital.Configuration;

public readonly struct DigitalInputTaskConfiguration
    : IDigitalInputTaskConfiguration<DigitalInputTaskConfiguration>
{
    public Device.Device Device { get; }
    public string TaskName { get; }
    public ICollection<IChannelConfiguration> ChannelConfigurations { get; }

    public DigitalInputTaskConfiguration(string taskName, Device.Device deviceIdentifier, ICollection<IChannelConfiguration> channelConfigurations)
    {
        TaskName = taskName;
        Device = deviceIdentifier;
        ChannelConfigurations = channelConfigurations;
    }

    public DigitalInputTaskConfiguration UsingDevice(string deviceIdentifier) =>
        new(TaskName, new Device.Device(deviceIdentifier), ChannelConfigurations);

    public DigitalInputTaskConfiguration WithTaskName(string taskName) =>
        new(taskName, Device, ChannelConfigurations);

    public DigitalInputTask CreateTask() => new(TaskName, ChannelConfigurations);
}