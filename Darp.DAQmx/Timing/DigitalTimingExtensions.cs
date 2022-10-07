using Darp.DAQmx.Task;
using static Darp.DAQmx.DaqMxException;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;

namespace Darp.DAQmx.Timing;

public static class DigitalTimingExtensions
{
    public static TTask ConfigureChangeDetection<TTask>(this Timing<TTask> timing,
        string deviceId,
        int portId,
        int firstLine,
        int lastLine,
        ulong samplesPerChannel)
        where TTask : IDigitalTask<TTask>
    {
        string physicalChannel = $"{deviceId}/port{portId}/line{firstLine}:{lastLine}";
        ThrowIfFailed(DAQmxCfgChangeDetectionTiming(timing.Task.Handle,
            physicalChannel,
            physicalChannel,
            SampleQuantityMode.ContinuousSamples,
            samplesPerChannel));
        return timing.Task;
    }
}