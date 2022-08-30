using Darp.DAQmx.Task.Common;

namespace Darp.DAQmx.Task.Digital.Channel;

public interface IDigitalChannel : IChannel
{
    int Width { get; }
}