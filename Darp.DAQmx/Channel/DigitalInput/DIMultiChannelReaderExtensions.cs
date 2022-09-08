using System;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;
using Microsoft.Toolkit.HighPerformance;

namespace Darp.DAQmx.Channel.DigitalInput;

public static class DIMultiChannelReaderExtensions
{
    private delegate int DaQmxRead<T>(IntPtr taskHandle, int numSampsPerChan, double timeout, DIFillMode fillMode, in T readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    private static void Read<T>(this IChannelReader<DigitalInputTask, IDigitalInputChannel> channelReader,
        DaQmxRead<T> readCallback,
        int numSamplesPerChannel,
        in Span<T> dataBuffer,
        DIFillMode fillMode,
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

    public static void ReadU8(this IChannelReader<DigitalInputTask, IDigitalInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<byte> dataBuffer,
        DIFillMode fillMode,
        double timeout = 10) =>
        channelReader.Read(Interop.DAQmxReadDigitalU8, numSamplesPerChannel, dataBuffer, fillMode, timeout);
    public static void ReadU16(this IChannelReader<DigitalInputTask, IDigitalInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<ushort> dataBuffer,
        DIFillMode fillMode,
        double timeout = 10) =>
        channelReader.Read(Interop.DAQmxReadDigitalU16, numSamplesPerChannel, dataBuffer, fillMode, timeout);
    public static void ReadU32(this IChannelReader<DigitalInputTask, IDigitalInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span<uint> dataBuffer,
        DIFillMode fillMode,
        double timeout = 10) =>
        channelReader.Read(Interop.DAQmxReadDigitalU32, numSamplesPerChannel, dataBuffer, fillMode, timeout);

    public static void ReadByScanNumber(this IChannelReader<DigitalInputTask, IDigitalInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span2D<bool> dataBuffer,
        double timeout = 10)
    {
        if (dataBuffer.Width < channelReader.ChannelCount)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Width of buffer is too small! Need space for {channelReader.ChannelCount} channels, but only got {dataBuffer.Width}");
        if (dataBuffer.Height < numSamplesPerChannel)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Height of buffer is too small! Need space for {numSamplesPerChannel} samples, but only got {dataBuffer.Height}");
        Span<uint> buffer = stackalloc uint[(int)dataBuffer.Length];
        channelReader.ReadU32(numSamplesPerChannel, buffer, DIFillMode.GroupByScanNumber, timeout);
        for (var scanNr = 0; scanNr < numSamplesPerChannel; scanNr++)
        {
            int offset = scanNr * channelReader.ChannelCount;
            for (var channelId = 0; channelId < channelReader.ChannelCount; channelId++)
                dataBuffer[scanNr, channelId] = buffer[offset + channelId] > 0;
        }
    }

    public static void ReadByChannel(this IChannelReader<DigitalInputTask, IDigitalInputChannel> channelReader,
        int numSamplesPerChannel,
        in Span2D<bool> dataBuffer,
        double timeout = 10)
    {
        if (dataBuffer.Width < numSamplesPerChannel)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Width of buffer is too small! Need space for {numSamplesPerChannel} samples, but only got {dataBuffer.Width}");
        if (dataBuffer.Height < channelReader.ChannelCount)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Height of buffer is too small! Need space for {channelReader.ChannelCount} channels, but only got {dataBuffer.Height}");
        Span<uint> buffer = stackalloc uint[(int)dataBuffer.Length];
        channelReader.ReadU32(numSamplesPerChannel, buffer, DIFillMode.GroupByChannel, timeout);
        for (var channelId = 0; channelId < channelReader.ChannelCount; channelId++)
        {
            int offset = channelId * numSamplesPerChannel;
            for (var scanNr = 0; scanNr < numSamplesPerChannel; scanNr++)
                dataBuffer[channelId, scanNr] = buffer[offset + scanNr] > 0;
        }
    }

    public static bool[,] ReadByChannel(this IChannelReader<DigitalInputTask, IDigitalInputChannel> channelReader,
        int numSamplesPerChannel,
        double timeout = 10)
    {
        var dataBuffer = new bool[channelReader.ChannelCount, numSamplesPerChannel];
        channelReader.ReadByChannel(numSamplesPerChannel, dataBuffer, timeout);
        return dataBuffer;
    }
}