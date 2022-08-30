using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task.Common;
using Darp.DAQmx.Task.Common.Configuration;
using Darp.DAQmx.Task.Digital.Configuration.Channel;

namespace Darp.DAQmx.Task.Digital;

public class DigitalInputTask : DaqMxTask<DigitalInputTask>
{
    private int LinesCount { get; }
    private int PortWidth { get; }
    public DigitalInputTask(string taskName, ICollection<IChannelConfiguration> channelConfigurations)
        : base(taskName, channelConfigurations)
    {
        int[] linesPerChannel = channelConfigurations
            .OfType<IDigitalInputChannelConfiguration>()
            .Select(x => x.LastLine - x.FirstLine + 1)
            .ToArray();
        LinesCount = linesPerChannel.Sum();
        PortWidth = channelConfigurations
            .OfType<IDigitalInputChannelConfiguration>()
            .Max(x => x.PortWidth);
    }

    public void ReadDigitalU8(int numSamplesPerChannel, DataFillMode fillMode, Span<byte> dataBuffer, int timeout = 10)
    {
        int requiredSpanSize = numSamplesPerChannel * LinesCount;
        if (dataBuffer.Length < requiredSpanSize)
            throw new DataException($"Span length too short. (Span length: {dataBuffer.Length}, required length: {requiredSpanSize})");
        int readStatus = Interop.DAQmxReadDigitalU8(TaskHandle,
            numSamplesPerChannel, timeout, (int) fillMode,
            dataBuffer.GetPinnableReference(), (uint) requiredSpanSize,
            out IntPtr actualNumSamplesPerChannelPtr, IntPtr.Zero);
        if (readStatus < 0)
            throw new DaqMxException(readStatus, "Could not read samples");
        var actualNumSamplesPerChannel = actualNumSamplesPerChannelPtr.ToInt32();
        if (actualNumSamplesPerChannel != numSamplesPerChannel)
            throw new DaqMxException("Could not read requested number of samples");
    }
    public void ReadDigitalU16(int numSamplesPerChannel, DataFillMode fillMode, Span<byte> dataBuffer, int timeout = 10)
    {
        int requiredSpanSize = numSamplesPerChannel * LinesCount;
        if (dataBuffer.Length < requiredSpanSize)
            throw new DataException($"Span length too short. (Span length: {dataBuffer.Length}, required length: {requiredSpanSize})");
        int readStatus = Interop.DAQmxReadDigitalU16(TaskHandle,
            numSamplesPerChannel, timeout, (int) fillMode,
            dataBuffer.GetPinnableReference(), (uint) requiredSpanSize,
            out IntPtr actualNumSamplesPerChannelPtr, IntPtr.Zero);
        if (readStatus < 0)
            throw new DaqMxException(readStatus, "Could not read samples");
        var actualNumSamplesPerChannel = actualNumSamplesPerChannelPtr.ToInt32();
        if (actualNumSamplesPerChannel != numSamplesPerChannel)
            throw new DaqMxException("Could not read requested number of samples");
    }
    public void ReadDigitalU32(int numSamplesPerChannel, DataFillMode fillMode, Span<byte> dataBuffer, int timeout = 10)
    {
        int requiredSpanSize = numSamplesPerChannel * LinesCount;
        if (dataBuffer.Length < requiredSpanSize)
            throw new DataException($"Span length too short. (Span length: {dataBuffer.Length}, required length: {requiredSpanSize})");
        int readStatus = Interop.DAQmxReadDigitalU32(TaskHandle,
            numSamplesPerChannel, timeout, (int) fillMode,
            dataBuffer.GetPinnableReference(), (uint) requiredSpanSize,
            out IntPtr actualNumSamplesPerChannelPtr, IntPtr.Zero);
        if (readStatus < 0)
            throw new DaqMxException(readStatus, "Could not read samples");
        var actualNumSamplesPerChannel = actualNumSamplesPerChannelPtr.ToInt32();
        if (actualNumSamplesPerChannel != numSamplesPerChannel)
            throw new DaqMxException("Could not read requested number of samples");
    }
    public IEnumerable<bool[]> ReadBitsPerScanNumber(int numSamplesPerChannel, int timeout = 10)
    {
        Span<byte> dataBuffer = stackalloc byte[numSamplesPerChannel * LinesCount];
        switch (PortWidth)
        {
            case <= 8:
                ReadDigitalU8(numSamplesPerChannel, DataFillMode.GroupByScanNumber, dataBuffer, timeout);
                break;
            case <= 16:
                ReadDigitalU16(numSamplesPerChannel, DataFillMode.GroupByScanNumber, dataBuffer, timeout);
                break;
            default:
                ReadDigitalU32(numSamplesPerChannel, DataFillMode.GroupByScanNumber, dataBuffer, timeout);
                break;
        }
        var array = new bool[numSamplesPerChannel][];
        for (var sampleIndex = 0; sampleIndex < numSamplesPerChannel; sampleIndex++)
        {
            var sampleRow = new bool[LinesCount];
            int offset = sampleIndex * LinesCount;
            for (var channelIndex = 0; channelIndex < LinesCount; channelIndex++)
                sampleRow[channelIndex] = dataBuffer[offset + channelIndex] > 0;
            array[sampleIndex] = sampleRow;
        }
        return array;
    }

    public IEnumerable<bool[]> ReadBitsByChannel(int numSamplesPerChannel, int timeout = 10)
    {
        Span<byte> dataBuffer = stackalloc byte[numSamplesPerChannel * LinesCount];
        switch (PortWidth)
        {
            case <= 8:
                ReadDigitalU8(numSamplesPerChannel, DataFillMode.GroupByChannel, dataBuffer, timeout);
                break;
            case <= 16:
                ReadDigitalU16(numSamplesPerChannel, DataFillMode.GroupByChannel, dataBuffer, timeout);
                break;
            default:
                ReadDigitalU32(numSamplesPerChannel, DataFillMode.GroupByChannel, dataBuffer, timeout);
                break;
        }
        var array = new bool[LinesCount][];
        for (var linesIndex = 0; linesIndex < LinesCount; linesIndex++)
        {
            var channelRow = new bool[numSamplesPerChannel];
            int offset = linesIndex * numSamplesPerChannel;
            for (var sampleIndex = 0; sampleIndex < numSamplesPerChannel; sampleIndex++)
                channelRow[sampleIndex] = dataBuffer[offset + sampleIndex] > 0;
            array[linesIndex] = channelRow;
        }
        return array;
    }
}