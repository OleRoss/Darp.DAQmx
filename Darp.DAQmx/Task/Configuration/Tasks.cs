using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Darp.DAQmx.NationalInstruments.Enums;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task.Configuration.Channel;

namespace Darp.DAQmx.Task.Configuration;

public abstract class DaqMxTask<TTask> : IDisposable
    where TTask : DaqMxTask<TTask>
{
    public IntPtr TaskHandle { get; }
    public int ChannelCount { get; }

    protected DaqMxTask(string taskName, ICollection<IChannelConfiguration> channelConfigurations)
    {
        int createTaskStatus = Interop.DAQmxCreateTask(taskName, out IntPtr taskHandle);
        if (createTaskStatus < 0)
            throw new DaqMxException(createTaskStatus, "Could not create Task");
        TaskHandle = taskHandle;
        ChannelCount = channelConfigurations.Count;
        foreach (IChannelConfiguration channelConfiguration in channelConfigurations)
        {
            int status = channelConfiguration.Create(TaskHandle);
            if (status < 0)
                throw new DaqMxException(status, $"Could not create channel {channelConfiguration} \n");
        }
    }

    public void Dispose()
    {
    }
}

public class DigitalInputTask : DaqMxTask<DigitalInputTask>
{
    private int LinesCount { get; }
    private int PortWidth { get; }
    public DigitalInputTask(string taskName, ICollection<IChannelConfiguration> channelConfigurations)
        : base(taskName, channelConfigurations)
    {
        int[] linesPerChannel = channelConfigurations
            .OfType<IDIChannelConfiguration>()
            .Select(x => x.LastLine - x.FirstLine + 1)
            .ToArray();
        LinesCount = linesPerChannel.Sum();
        PortWidth = channelConfigurations
            .OfType<IDIChannelConfiguration>()
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
    public IEnumerable<byte[]> ReadBitsPerScanNumber(int numSamplesPerChannel, int timeout = 10)
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
        var array = new byte[numSamplesPerChannel][];
        for (var i = 0; i < numSamplesPerChannel; i++)
        {
            var channelRow = new byte[LinesCount];
            dataBuffer[(i*LinesCount)..((i+1)*LinesCount)].CopyTo(channelRow);
            array[i] = channelRow;
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

public class AnalogInputTask : DaqMxTask<AnalogInputTask>
{
    internal AnalogInputTask(string taskName, ICollection<IChannelConfiguration> channelConfigurations)
        : base(taskName, channelConfigurations)
    {
    }

    public void ReadDoubles(int numSamplesPerChannel, DataFillMode fillMode, Span<double> dataBuffer, int timeout = 10)
    {
        int requiredSpanSize = numSamplesPerChannel * ChannelCount;
        if (dataBuffer.Length < requiredSpanSize)
            throw new DataException($"Span length too short. (Span length: {dataBuffer.Length}, required length: {requiredSpanSize})");
        int readStatus = Interop.DAQmxReadAnalogF64(TaskHandle,
            numSamplesPerChannel, timeout, (int) fillMode,
            dataBuffer.GetPinnableReference(), (uint) requiredSpanSize,
            out IntPtr actualNumSamplesPerChannelPtr, IntPtr.Zero);
        if (readStatus < 0)
            throw new DaqMxException(readStatus, "Could not read samples");
        var actualNumSamplesPerChannel = actualNumSamplesPerChannelPtr.ToInt32();
        if (actualNumSamplesPerChannel != numSamplesPerChannel)
            throw new DaqMxException("Could not read requested number of samples");

        // this.TotalSampsPerChanRead += (ulong) int32;
        // this.TotalSampsRead += (ulong) arraySizeInSamps;
    }

    public IEnumerable<double[]> ReadDoublesPerScanNumber(int numSamplesPerChannel, int timeout = 10)
    {
        Span<double> dataBuffer = stackalloc double[numSamplesPerChannel * ChannelCount];
        ReadDoubles(numSamplesPerChannel, DataFillMode.GroupByScanNumber, dataBuffer, timeout);
        var array = new double[numSamplesPerChannel][];
        for (var i = 0; i < numSamplesPerChannel; i++)
        {
            var channelRow = new double[ChannelCount];
            dataBuffer[(i*ChannelCount)..((i+1)*ChannelCount)].CopyTo(channelRow);
            array[i] = channelRow;
        }
        return array;
    }

    public IEnumerable<double[]> ReadDoublesByChannel(int numSamplesPerChannel, int timeout = 10)
    {
        Span<double> dataBuffer = stackalloc double[numSamplesPerChannel * ChannelCount];
        ReadDoubles(numSamplesPerChannel, DataFillMode.GroupByChannel, dataBuffer, timeout);
        var array = new double[ChannelCount][];
        for (var i = 0; i < ChannelCount; i++)
        {
            var channelRow = new double[numSamplesPerChannel];
            dataBuffer[(i*numSamplesPerChannel)..((i+1)*numSamplesPerChannel)].CopyTo(channelRow);
            array[i] = channelRow;
        }
        return array;
    }
}