using System;
using Darp.DAQmx.Task.Common;
using Darp.DAQmx.Task.Digital.Channel;
using Darp.DAQmx.Task.Digital.Configuration.Channel;

namespace Darp.DAQmx.Task.Digital.Configuration;

public static class DigitalInputTaskConfigurationExtensions
{
    public static DigitalInputTaskConfiguration WithDIChannel<TConfiguration>(
        this TConfiguration config,
        DigitalInputChannel channel,
        Action<DigitalInputChannelPortLinesConfiguration>? configuration = null)
        where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration>
    {
        var channelConfiguration = new DigitalInputChannelPortLinesConfiguration(
            Guid.NewGuid().ToString(),
            channel,
            config.Device.Identifier,
            0,
            channel.Width,
            ChannelLineMode.ChannelsPerLine
        );
        configuration?.Invoke(channelConfiguration);
        config.ChannelConfigurations.Add(channelConfiguration);
        Console.WriteLine(channelConfiguration);
        return new DigitalInputTaskConfiguration(config.TaskName, config.Device, config.ChannelConfigurations);
    }

    public static DigitalInputTaskConfiguration WithDILines<TConfiguration>(
        this TConfiguration config,
        int firstLine,
        int lastLine,
        int portWidth,
        Action<DigitalInputChannelLinesConfiguration>? configuration = null)
        where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration>
    {
        var channelConfiguration = new DigitalInputChannelLinesConfiguration(
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
        Action<DigitalInputChannelPortLinesConfiguration>? configuration = null)
        where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration>
    {
        var channelConfiguration = new DigitalInputChannelPortLinesConfiguration(
            Guid.NewGuid().ToString(),
            channel,
            config.Device.Identifier,
            firstLine,
            lastLine,
            ChannelLineMode.ChannelsPerLine
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
        Action<DigitalInputChannelPortLinesConfiguration>? configuration = null)
        where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration> =>
        config.WithDIChannel( channel, line, line, configuration);
}