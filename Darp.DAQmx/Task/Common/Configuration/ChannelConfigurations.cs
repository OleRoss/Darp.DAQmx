using System;

namespace Darp.DAQmx.Task.Common.Configuration;

public interface IChannelConfiguration
{
    public string PhysicalChannel { get; }
    string ChannelName { get; }
    internal int Create(IntPtr taskHandle);
}