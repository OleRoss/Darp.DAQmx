using System;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;
using Microsoft.Toolkit.HighPerformance;

namespace Darp.DAQmx.Channel.CounterInput;

public static class CIReaderExtensions
{
    public static void ReadScalar(this ISingleChannelReader<CounterInputTask, ICounterInputChannel> channelReader,
        out double data,
        double timeout = 10)
    {
        DaqMxException.ThrowIfFailed(Interop.DAQmxReadCounterScalarF64(channelReader.Task.Handle,
            timeout,
            out data,
            IntPtr.Zero));
    }
    public static double ReadScalar(this ISingleChannelReader<CounterInputTask, ICounterInputChannel> channelReader,
        double timeout = 10)
    {
        channelReader.ReadScalar(out double scalar);
        return scalar;
    }

    public static void ReadScalarUnscaled(this ISingleChannelReader<CounterInputTask, ICounterInputChannel> channelReader,
        out uint data,
        double timeout = 10)
    {
        DaqMxException.ThrowIfFailed(Interop.DAQmxReadCounterScalarU32(channelReader.Task.Handle,
            timeout,
            out data,
            IntPtr.Zero));
    }
    private delegate int DaQmxRead<T>(IntPtr taskHandle, int numSampsPerChan, double timeout, CIFillMode fillMode, in T readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    private static void Read<T>(this IChannelReader<CounterInputTask, ICounterInputChannel> channelReader,
        DaQmxRead<T> readCallback,
        int numSamplesPerChannel,
        in Span<T> dataBuffer,
        CIFillMode fillMode,
        double timeout)
        where T : struct
    {
        int requiredSpanSize = numSamplesPerChannel * channelReader.ChannelCount;
        if (dataBuffer.Length < requiredSpanSize)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Span length too short. (Span length: {dataBuffer.Length}, required length: {requiredSpanSize})");
        DaqMxException.ThrowIfFailed(readCallback(channelReader.Task.Handle,
            numSamplesPerChannel,
            timeout,
            fillMode,
            dataBuffer.GetPinnableReference(),
            (uint)requiredSpanSize,
            out int sampsPerChanRead,
            IntPtr.Zero));
        if (sampsPerChanRead != numSamplesPerChannel)
            throw new DaqMxException("Could not read requested number of samples");
    }

    public static void ReadCounterF64(this IChannelReader<CounterInputTask, ICounterInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<double> dataBuffer,
        CIFillMode fillMode,
        double timeout = 10)
    {
        channelReader.Read(Interop.DAQmxReadCounterF64Ex, numSamplesPerChannel, dataBuffer, fillMode, timeout);
    }

    public static void ReadCounterU32Ex(this IChannelReader<CounterInputTask, ICounterInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<uint> dataBuffer,
        CIFillMode fillMode,
        double timeout = 10)
    {
        channelReader.Read(Interop.DAQmxReadCounterU32Ex, numSamplesPerChannel, dataBuffer, fillMode, timeout);
    }

    public static void ReadByScanNumber(this IChannelReader<CounterInputTask, ICounterInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span2D<double> dataBuffer,
        double timeout = 10)
    {
        if (dataBuffer.Width < channelReader.ChannelCount)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Width of buffer is too small! Need space for {channelReader.ChannelCount} channels, but only got {dataBuffer.Width}");
        if (dataBuffer.Height < numSamplesPerChannel)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Height of buffer is too small! Need space for {numSamplesPerChannel} samples, but only got {dataBuffer.Height}");
        Span<double> buffer = stackalloc double[(int)dataBuffer.Length];
        channelReader.ReadCounterF64(numSamplesPerChannel, buffer, CIFillMode.GroupByScanNumber, timeout);
        for (var i = 0; i < numSamplesPerChannel; i++)
            buffer[(i * channelReader.ChannelCount)..((i + 1) * channelReader.ChannelCount)].CopyTo(dataBuffer.GetRowSpan(i));
    }

    public static void ReadByChannel(this IChannelReader<CounterInputTask, ICounterInputChannel> channelReader,
        int numSamplesPerChannel,
        Span2D<double> dataBuffer,
        double timeout = 10
        )
    {
        if (dataBuffer.Width < numSamplesPerChannel)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Width of buffer is too small! Need space for {numSamplesPerChannel} samples, but only got {dataBuffer.Width}");
        if (dataBuffer.Height < channelReader.ChannelCount)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Height of buffer is too small! Need space for {channelReader.ChannelCount} channels, but only got {dataBuffer.Height}");
        Span<double> buffer = stackalloc double[(int)dataBuffer.Length];
        channelReader.ReadCounterF64(numSamplesPerChannel, buffer, CIFillMode.GroupByChannel, timeout);
        for (var i = 0; i < channelReader.ChannelCount; i++)
            buffer[(i * numSamplesPerChannel)..((i + 1) * numSamplesPerChannel)].CopyTo(dataBuffer.GetRowSpan(i));
    }

    public static double[,] ReadByChannel(this IChannelReader<CounterInputTask, ICounterInputChannel> channelReader,
        int numSamplesPerChannel,
        double timeout = 10)
    {
        var dataBuffer = new double[channelReader.ChannelCount, numSamplesPerChannel];
        channelReader.ReadByChannel(numSamplesPerChannel, dataBuffer, timeout);
        return dataBuffer;
    }
}