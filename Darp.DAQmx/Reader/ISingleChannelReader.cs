using Darp.DAQmx.Channel;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Reader;

public interface ISingleChannelReader<out TTask, out TChannel> : IChannelReader<TTask, TChannel>
    where TTask : ITask<TTask, TChannel>
    where TChannel : IChannel
{

}