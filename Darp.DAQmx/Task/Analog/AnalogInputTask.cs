using System;
using System.Collections.Generic;
using System.Data;
using Darp.DAQmx.NationalInstruments.Functions;
using Darp.DAQmx.Task.Common;
using Darp.DAQmx.Task.Common.Configuration;

namespace Darp.DAQmx.Task.Analog;

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