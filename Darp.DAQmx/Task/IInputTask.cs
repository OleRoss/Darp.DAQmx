using Darp.DAQmx.Channel;

namespace Darp.DAQmx.Task;

public interface IInputTask<TTask, TChannel> : ITask<TTask, TChannel>
    where TTask : IInputTask<TTask, TChannel>
    where TChannel : IInputChannel
{

}