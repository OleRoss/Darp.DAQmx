using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Channel.CounterInput;

public static class CIChannelExtensions
{
    public static SingleChannelReader<CounterInputTask, ICounterInputChannel> GetSingleReader(
        this ChannelCollection<CounterInputTask, ICounterInputChannel> channel) => new(channel.Task);
    public static MultiChannelReader<CounterInputTask, ICounterInputChannel> GetMultiReader(
        this ChannelCollection<CounterInputTask, ICounterInputChannel> channel) => new(channel.Task);
}