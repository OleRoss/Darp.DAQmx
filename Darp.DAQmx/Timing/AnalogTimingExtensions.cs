using System;
using Darp.DAQmx.Task;

using static Darp.DAQmx.DaqMxException;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;

namespace Darp.DAQmx.Timing;

public static class AnalogTimingExtensions
{/// <summary>
    /// 
    /// </summary>
    /// <param name="rate">The sampling rate in samples per second. If you use an external source for the sample clock, set this input to the maximum expected rate of that clock.</param>
    /// <param name="samplesPerChannel">The number of samples to acquire or generate if <paramref name="sampleMode" /> is <see cref="SampleQuantityMode.FiniteSamples" />. If sample mode is <see cref="SampleQuantityMode.ContinuousSamples" />, NI-DAQmx uses this value to determine the buffer size</param>
    /// <param name="sampleMode">The duration of the task. A task is either finite and stops once the specified number of samples have been acquired or generated, or it is continuous and continues to acquire or generate samples until the task is explicitly stopped.</param>
    /// <param name="edge">The edges of sample clock pulses on which to acquire or generate samples.</param>
    /// <returns>The parent task</returns>
    public static TTask ConfigureSampleClock<TTask>(this Timing<TTask> timing, double rate,
        ulong samplesPerChannel,
        SampleQuantityMode sampleMode,
        SampleClockActiveEdge edge = SampleClockActiveEdge.Rising)
        where TTask : ITask<TTask>
    {
        if (sampleMode == SampleQuantityMode.HardwareTimedSinglePoint)
            throw new ArgumentException($"Use {nameof(ConfigureSampleClockHardwareTimed)} instead", nameof(sampleMode));
        ThrowIfFailed(DAQmxCfgSampClkTiming(timing.Task.Handle, "", rate, edge, sampleMode, samplesPerChannel));
        return timing.Task;
    }

    public static TTask ConfigureSampleClockHardwareTimed<TTask>(this Timing<TTask> timing, double rate,
        string source,
        SampleClockActiveEdge edge = SampleClockActiveEdge.Rising)
        where TTask : IAnalogTask<TTask>
    {
        ThrowIfFailed(DAQmxCfgSampClkTiming(timing.Task.Handle, source, rate, edge, SampleQuantityMode.HardwareTimedSinglePoint, 1));
        return timing.Task;
    }
}