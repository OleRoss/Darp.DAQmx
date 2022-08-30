using System;
using Darp.DAQmx.Task.Analog.Channel;
using Darp.DAQmx.Task.Analog.Configuration.Channel;
using Darp.DAQmx.Task.Common;

namespace Darp.DAQmx.Task.Analog.Configuration;

public static class AnalogInputTaskConfigurationExtensions
{
    /// <summary>
    /// Measuring Voltage
    /// <para>
    /// Most measurement devices are designed for measuring, or reading, voltage. Two common voltage measurements are DC and AC.
    /// <list type="bullet">
    /// <item> DC voltages are useful for measuring phenomena that change slowly with time, such as temperature, pressure, or strain. </item>
    /// <item> AC voltages, on the other hand, are waveforms that constantly increase, decrease, and reverse polarity. Most powerlines deliver AC voltage. </item>
    /// </list>
    /// </para>
    /// <para>
    /// Default configuration
    /// <list type="table">
    /// <item> <term><see cref="AnalogInputVoltageChannelConfiguration.ChannelName"/>:</term><description>Random guid string</description> </item>
    /// <item> <term><see cref="AnalogInputVoltageChannelConfiguration.MinVoltage"/> [V]:</term><description>-10</description> </item>
    /// <item> <term><see cref="AnalogInputVoltageChannelConfiguration.MaxVoltage"/> [V]:</term><description>10</description> </item>
    /// <item> <term><see cref="AnalogInputVoltageChannelConfiguration.TerminalConfiguration"/>:</term><description><see cref="InputTerminalConfiguration.Differential"/></description> </item>
    /// <item> <term><see cref="AnalogInputVoltageChannelConfiguration.Unit"/>:</term><description><see cref="DaqMxUnit.Volts"/></description> </item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="config">The configuration to target</param>
    /// <param name="channel">Requests the channel, which can be obtained by the provided device</param>
    /// <param name="configuration">Provides the configuration object for overwriting standard values</param>
    /// <returns>Configuration builder</returns>
    public static AnalogInputTaskConfiguration WithAIVoltageChannel<TConfiguration>(
        this TConfiguration config,
        AnalogInputChannel channel,
        Action<AnalogInputVoltageChannelConfiguration>? configuration = null)
        where TConfiguration : IAnalogInputTaskConfiguration<TConfiguration>
    {
        var channelConfiguration = new AnalogInputVoltageChannelConfiguration(
            Guid.NewGuid().ToString(),
            channel,
            config.Device.Identifier,
            InputTerminalConfiguration.Differential,
            -10,
            10,
            DaqMxUnit.Volts
        );
        configuration?.Invoke(channelConfiguration);
        config.ChannelConfigurations.Add(channelConfiguration);
        Console.WriteLine(channelConfiguration);
        return new AnalogInputTaskConfiguration(config.TaskName, config.Device, config.ChannelConfigurations);
    }

    public static AnalogInputTaskConfiguration UsingDevice(
        this AnalogInputTaskConfiguration analogInputConfiguration,
        string deviceIdentifier)
        => new(analogInputConfiguration.TaskName, new Device.Device(deviceIdentifier), analogInputConfiguration.ChannelConfigurations);
}