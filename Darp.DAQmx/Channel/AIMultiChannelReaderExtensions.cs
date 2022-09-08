using System;
using Darp.DAQmx.Channel.AnalogInput;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;
using Microsoft.Toolkit.HighPerformance;

namespace Darp.DAQmx.Channel;

public static class AIMultiChannelReaderExtensions
{
    private delegate int DaQmxRead<T>(IntPtr taskHandle, int numSampsPerChan, double timeout, AIFillMode fillMode, in T readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    private static void Read<T>(this IChannelReader<AnalogInputTask, IAnalogInputChannel> channelReader,
        DaQmxRead<T> readCallback,
        int numSamplesPerChannel,
        in Span<T> dataBuffer,
        AIFillMode fillMode,
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

    /// <summary>
    /// Reads multiple floating-point samples from a task that contains one or more analog input channels.
    /// </summary>
    /// <param name="channelReader">The reader to be read from</param>
    /// <param name="numSamplesPerChannel">The number of samples to be read</param>
    /// <param name="dataBuffer">The memory to be written to</param>
    /// <param name="fillMode">The fill mode to be used</param>
    /// <param name="timeout">The communication timeout</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the given memory is not big enough</exception>
    /// <exception cref="DaqMxException">Thrown if reading did not work</exception>
    public static void ReadAnalogF64(this IChannelReader<AnalogInputTask, IAnalogInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<double> dataBuffer,
        AIFillMode fillMode,
        double timeout = 10) =>
        channelReader.Read(Interop.DAQmxReadAnalogF64, numSamplesPerChannel, dataBuffer, fillMode, timeout);

    public static void ReadByScanNumber(this IChannelReader<AnalogInputTask, IAnalogInputChannel> channelReader,
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
        channelReader.ReadAnalogF64(numSamplesPerChannel, buffer, AIFillMode.GroupByScanNumber, timeout);
        for (var i = 0; i < numSamplesPerChannel; i++)
            buffer[(i * channelReader.ChannelCount)..((i + 1) * channelReader.ChannelCount)].CopyTo(dataBuffer.GetRowSpan(i));
    }

    public static void ReadByChannel(this IChannelReader<AnalogInputTask, IAnalogInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span2D<double> dataBuffer,
        double timeout = 10)
    {
        if (dataBuffer.Width < numSamplesPerChannel)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Width of buffer is too small! Need space for {numSamplesPerChannel} samples, but only got {dataBuffer.Width}");
        if (dataBuffer.Height < channelReader.ChannelCount)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Height of buffer is too small! Need space for {channelReader.ChannelCount} channels, but only got {dataBuffer.Height}");
        Span<double> buffer = stackalloc double[(int)dataBuffer.Length];
        channelReader.ReadAnalogF64(numSamplesPerChannel, buffer, AIFillMode.GroupByChannel, timeout);
        for (var i = 0; i < channelReader.ChannelCount; i++)
            buffer[(i * numSamplesPerChannel)..((i + 1) * numSamplesPerChannel)].CopyTo(dataBuffer.GetRowSpan(i));
    }

    public static double[,] ReadByChannel(this IChannelReader<AnalogInputTask, IAnalogInputChannel> channelReader,
        int numSamplesPerChannel,
        double timeout = 10)
    {
        var dataBuffer = new double[channelReader.ChannelCount, numSamplesPerChannel];
        channelReader.ReadByChannel(numSamplesPerChannel, dataBuffer, timeout);
        return dataBuffer;
    }

    public static void ReadBinaryU16(this IChannelReader<AnalogInputTask, IAnalogInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<ushort> dataBuffer,
        AIFillMode fillMode,
        double timeout = 10) =>
        channelReader.Read(Interop.DAQmxReadBinaryU16, numSamplesPerChannel, dataBuffer, fillMode, timeout);

    public static void ReadBinaryI16(this IChannelReader<AnalogInputTask, IAnalogInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<short> dataBuffer,
        AIFillMode fillMode,
        double timeout = 10) =>
        channelReader.Read(Interop.DAQmxReadBinaryI16, numSamplesPerChannel, dataBuffer, fillMode, timeout);

    public static void ReadBinaryU32(this IChannelReader<AnalogInputTask, IAnalogInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<uint> dataBuffer,
        AIFillMode fillMode,
        double timeout = 10) =>
        channelReader.Read(Interop.DAQmxReadBinaryU32, numSamplesPerChannel, dataBuffer, fillMode, timeout);

    public static void ReadBinaryI32(this IChannelReader<AnalogInputTask, IAnalogInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<int> dataBuffer,
        AIFillMode fillMode,
        double timeout = 10) =>
        channelReader.Read(Interop.DAQmxReadBinaryI32, numSamplesPerChannel, dataBuffer, fillMode, timeout);
}