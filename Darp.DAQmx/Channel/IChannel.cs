namespace Darp.DAQmx.Channel;

public interface IChannel
{
    public string PhysicalChannel { get; }
    public int NumberOfVirtualChannels { get; }
    public string Name { get; }
}