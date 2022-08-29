using System.Collections.Generic;
using Darp.DAQmx.Task.Configuration.Channel;

namespace Darp.DAQmx.Task.Configuration.Task;

public interface ITaskConfiguration<out TConfiguration>
    where TConfiguration : ITaskConfiguration<TConfiguration>
{
    Device Device { get; }
    string TaskName { get; }
    ICollection<IChannelConfiguration> ChannelConfigurations { get; }
    TConfiguration WithTaskName(string taskName);
}

public readonly struct TaskConfiguration
    : IDigitalInputTaskConfiguration<TaskConfiguration>,
        IAnalogInputTaskConfiguration<TaskConfiguration>
{
    public Device Device { get; }
    public string TaskName { get; }
    public ICollection<IChannelConfiguration> ChannelConfigurations { get; }

    public TaskConfiguration(string taskName, Device deviceIdentifier, ICollection<IChannelConfiguration> channelConfigurations)
    {
        Device = deviceIdentifier;
        TaskName = taskName;
        ChannelConfigurations = channelConfigurations;
    }

    public TaskConfiguration WithTaskName(string taskName) => new(taskName, Device, ChannelConfigurations);
}

public static class TaskConfigurationExtensions
{

}
