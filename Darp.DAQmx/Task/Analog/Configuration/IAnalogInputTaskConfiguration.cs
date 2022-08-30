using Darp.DAQmx.Task.Common.Configuration;

namespace Darp.DAQmx.Task.Analog.Configuration;

public interface IAnalogInputTaskConfiguration<out TConfiguration>
    : ITaskConfiguration<TConfiguration>
    where TConfiguration : IAnalogInputTaskConfiguration<TConfiguration>
{

}