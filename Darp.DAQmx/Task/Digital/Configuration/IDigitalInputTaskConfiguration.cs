using Darp.DAQmx.Task.Common.Configuration;

namespace Darp.DAQmx.Task.Digital.Configuration;

public interface IDigitalInputTaskConfiguration<out TConfiguration>
    : ITaskConfiguration<TConfiguration>
    where TConfiguration : IDigitalInputTaskConfiguration<TConfiguration>
{

}