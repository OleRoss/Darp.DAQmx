using System;
using System.Collections.Generic;
using Darp.DAQmx.NationalInstruments.Enums;
using Darp.DAQmx.Task.Configuration.Channel;

namespace Darp.DAQmx.Task.Configuration.Task;

public interface IDigitalInputTaskConfiguration<out TConfiguration>
    : ITaskConfiguration<TConfiguration>
    where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration>
{

}

public readonly struct DigitalInputTaskConfiguration
    : IDigitalInputTaskConfiguration<DigitalInputTaskConfiguration>
{
    public Device Device { get; }
    public string TaskName { get; }
    public ICollection<IChannelConfiguration> ChannelConfigurations { get; }

    public DigitalInputTaskConfiguration(string taskName, Device deviceIdentifier, ICollection<IChannelConfiguration> channelConfigurations)
    {
        TaskName = taskName;
        Device = deviceIdentifier;
        ChannelConfigurations = channelConfigurations;
    }

    public DigitalInputTaskConfiguration UsingDevice(string deviceIdentifier) =>
        new(TaskName, new Device(deviceIdentifier), ChannelConfigurations);

    public DigitalInputTaskConfiguration WithTaskName(string taskName) =>
        new(taskName, Device, ChannelConfigurations);

    public DigitalInputTask CreateTask() => new(TaskName, ChannelConfigurations);
}

public static class DigitalInputTaskConfigurationExtensions
{
    public static DigitalInputTaskConfiguration WithDIChannel<TConfiguration>(
        this TConfiguration config,
        DigitalInputChannel channel,
        Action<DIChannelPortLinesConfiguration>? configuration = null)
        where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration>
    {
        var channelConfiguration = new DIChannelPortLinesConfiguration(
            Guid.NewGuid().ToString(),
            channel,
            config.Device.Identifier,
            0,
            channel.Width,
            DaqMxLines.ChannelsPerLine
        );
        configuration?.Invoke(channelConfiguration);
        config.ChannelConfigurations.Add(channelConfiguration);
        Console.WriteLine(channelConfiguration);
        return new DigitalInputTaskConfiguration(config.TaskName, config.Device, config.ChannelConfigurations);
    }

    public static DigitalInputTaskConfiguration WithDIChannel<TConfiguration>(
        this TConfiguration config,
        int firstLine,
        int lastLine,
        int portWidth,
        Action<DIChannelLinesConfiguration>? configuration = null)
        where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration>
    {
        var channelConfiguration = new DIChannelLinesConfiguration(
            Guid.NewGuid().ToString(),
            config.Device.Identifier,
            firstLine,
            lastLine,
            portWidth
        );
        configuration?.Invoke(channelConfiguration);
        config.ChannelConfigurations.Add(channelConfiguration);
        Console.WriteLine(channelConfiguration);
        return new DigitalInputTaskConfiguration(config.TaskName, config.Device, config.ChannelConfigurations);
    }

    public static DigitalInputTaskConfiguration WithDIChannel<TConfiguration>(
        this TConfiguration config,
        DigitalInputChannel channel,
        int firstLine,
        int lastLine,
        Action<DIChannelPortLinesConfiguration>? configuration = null)
        where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration>
    {
        var channelConfiguration = new DIChannelPortLinesConfiguration(
            Guid.NewGuid().ToString(),
            channel,
            config.Device.Identifier,
            firstLine,
            lastLine,
            DaqMxLines.ChannelsPerLine
        );
        configuration?.Invoke(channelConfiguration);
        config.ChannelConfigurations.Add(channelConfiguration);
        Console.WriteLine(channelConfiguration);
        return new DigitalInputTaskConfiguration(config.TaskName, config.Device, config.ChannelConfigurations);
    }

    public static DigitalInputTaskConfiguration WithDIChannel<TConfiguration>(
        this TConfiguration config,
        DigitalInputChannel channel,
        int line,
        Action<DIChannelPortLinesConfiguration>? configuration = null)
        where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration> =>
        config.WithDIChannel( channel, line, line, configuration);
}