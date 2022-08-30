using System.Collections.Generic;

namespace Darp.DAQmx.Task.Common.Configuration;

public interface ITaskConfiguration<out TConfiguration>
    where TConfiguration : ITaskConfiguration<TConfiguration>
{
    Device.Device Device { get; }
    string TaskName { get; }
    ICollection<IChannelConfiguration> ChannelConfigurations { get; }
    TConfiguration WithTaskName(string taskName);
}