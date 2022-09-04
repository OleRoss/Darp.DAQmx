namespace Darp.DAQmx.Channel.CounterInput;

public interface ICounterChannel : IChannel
{
    int CounterId { get; }
}