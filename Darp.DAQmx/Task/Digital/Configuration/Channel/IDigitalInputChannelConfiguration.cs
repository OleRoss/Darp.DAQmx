using Darp.DAQmx.Task.Common;
using Darp.DAQmx.Task.Common.Configuration;

namespace Darp.DAQmx.Task.Digital.Configuration.Channel;

public interface IDigitalInputChannelConfiguration : IChannelConfiguration
{
    int FirstLine { get; }
    int LastLine { get; }
    int PortWidth { get; }
}