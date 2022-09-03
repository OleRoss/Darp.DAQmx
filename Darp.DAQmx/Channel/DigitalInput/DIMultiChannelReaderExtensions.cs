using System;
using Darp.DAQmx.Channel.AnalogInput;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Reader;
using Darp.DAQmx.Task;

namespace Darp.DAQmx.Channel.DigitalInput;

public static class DIMultiChannelReaderExtensions
{
    private delegate int DaQmxRead<T>(IntPtr taskHandle, int numSampsPerChan, double timeout, DIFillMode fillMode, in T readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    private static void Read<T>(this MultiChannelReader<DigitalInputTask, IDigitalInputChannel> reader,
        DaQmxRead<T> readCallback,
        int numSamplesPerChannel,
        in Span<T> dataBuffer,
        DIFillMode fillMode,
        double timeout)
        where T : struct
    {
        int requiredSpanSize = numSamplesPerChannel * reader.ChannelCount;
        if (dataBuffer.Length < requiredSpanSize)
            throw new ArgumentOutOfRangeException(nameof(dataBuffer),
                $"Span length too short. (Span length: {dataBuffer.Length}, required length: {requiredSpanSize})");
        DaqMxException.ThrowIfFailed(readCallback(reader.Task.Handle,
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

    public static void ReadDigitalU8(this MultiChannelReader<DigitalInputTask, IDigitalInputChannel> reader,
        int numSamplesPerChannel,
        in Span<byte> dataBuffer,
        DIFillMode fillMode,
        double timeout = 10) =>
        reader.Read(Interop.DAQmxReadDigitalU8, numSamplesPerChannel, dataBuffer, fillMode, timeout);
    public static void ReadDigitalU16(this MultiChannelReader<DigitalInputTask, IDigitalInputChannel> reader,
        int numSamplesPerChannel,
        in Span<ushort> dataBuffer,
        DIFillMode fillMode,
        double timeout = 10) =>
        reader.Read(Interop.DAQmxReadDigitalU16, numSamplesPerChannel, dataBuffer, fillMode, timeout);
    public static void ReadDigitalU32(this MultiChannelReader<DigitalInputTask, IDigitalInputChannel> reader,
        int numSamplesPerChannel,
        in Span<uint> dataBuffer,
        DIFillMode fillMode,
        double timeout = 10) =>
        reader.Read(Interop.DAQmxReadDigitalU32, numSamplesPerChannel, dataBuffer, fillMode, timeout);

}