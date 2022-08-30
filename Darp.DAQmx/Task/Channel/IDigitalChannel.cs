namespace Darp.DAQmx.Task.Channel;

public interface IDigitalChannel : IChannel
{
    int Width { get; }
}