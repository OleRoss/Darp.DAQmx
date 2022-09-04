using System;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Channel.CounterInput;

public static class CISingleChannelReaderExtensions
{
    public static void Read(this SingleChannelReader<CounterInputTask, ICounterInputChannel> reader,
        out double data,
        double timeout = 10)
    {
        DaqMxException.ThrowIfFailed(Interop.DAQmxReadCounterScalarF64(reader.Task.Handle,
            timeout,
            out data,
            IntPtr.Zero));
    }
    public static void ReadUnscaled(this SingleChannelReader<CounterInputTask, ICounterInputChannel> reader,
        out uint data,
        double timeout = 10)
    {
        DaqMxException.ThrowIfFailed(Interop.DAQmxReadCounterScalarU32(reader.Task.Handle,
            timeout,
            out data,
            IntPtr.Zero));
    }
}