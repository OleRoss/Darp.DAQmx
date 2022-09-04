using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Channel.AnalogInput;

public static class AIChannelExtensions
{
    public static SingleChannelReader<AnalogInputTask, IAnalogInputChannel> GetSingleReader(
        this ChannelCollection<AnalogInputTask, IAnalogInputChannel> channel) => new(channel.Task);
    public static MultiChannelReader<AnalogInputTask, IAnalogInputChannel> GetMultiReader(
        this ChannelCollection<AnalogInputTask, IAnalogInputChannel> channel) => new(channel.Task);
}