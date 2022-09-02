using System;
using System.Data;
using Darp.DAQmx.Channel.AnalogInput;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Channel;

public static class ReaderExtensions
{
    public static void ReadAnalogF64(this MultiChannelReader<AnalogInputTask, IAnalogInputChannel> reader, int numSamplesPerChannel,
        Span<double> dataBuffer,
        AIFillMode fillMode = AIFillMode.GroupByScanNumber,
        double timeout = 10)
    {
        int requiredSpanSize = numSamplesPerChannel * reader.ChannelCount;
        if (dataBuffer.Length < requiredSpanSize)
            throw new DataException($"Span length too short. (Span length: {dataBuffer.Length}, required length: {requiredSpanSize})");
        DaqMxException.ThrowIfFailed(Interop.DAQmxReadAnalogF64(reader.Task.Handle,
            numSamplesPerChannel,
            timeout,
            (int)fillMode,
            dataBuffer.GetPinnableReference(),
            (uint)requiredSpanSize,
            out int sampsPerChanRead,
            IntPtr.Zero));
        if (sampsPerChanRead != numSamplesPerChannel)
            throw new DaqMxException("Could not read requested number of samples");
    }
}