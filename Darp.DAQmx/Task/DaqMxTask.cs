using System;
using System.Data;
using Darp.DAQmx.NationalInstruments.Enums;
using Darp.DAQmx.NationalInstruments.Functions;

namespace Darp.DAQmx.Task;

public class DaqMxTask : IDisposable
{
    private readonly string _taskName;
    private readonly IntPtr _taskHandle;

    public int Channels { get; private set; }

    public DaqMxTask() : this(Guid.NewGuid().ToString()) { }
    public DaqMxTask(string taskName)
    {
        _taskName = taskName;
        int createTaskStatus = Interop.DAQmxCreateTask(taskName, out _taskHandle);
        if (createTaskStatus < 0)
            throw new DaqMxException(createTaskStatus, "Could not create Task");
        Channels = 0;
    }

    public DaqMxTask WithAnalogInputChannel(DaqMxDevice device, AnalogInputChannel inputChannel, string channelName,
        DaqMxInputTerminalConfiguration terminalConfiguration,
        int minValue, int maxValue, DaqMxUnit units)
    {
        int createChannelStatus = Interop.DAQmxCreateAIVoltageChan(_taskHandle,
            $"{device.DeviceName}/{inputChannel.Id}",
            channelName,
            (int) terminalConfiguration,
            minValue,
            maxValue,
            units.Value,
            units.CustomScaleName);
        if (createChannelStatus < 0)
            throw new DaqMxException(createChannelStatus,
                $"Could not create AI Voltage channel '{channelName}' on '{inputChannel}'");
        Channels++;
        return this;
    }

    public DaqMxTask WithDigitalInputChannel(DaqMxDevice device,
        Range range)
    {

        return this;
    }

    public DaqMxTask WithDigitalInputChannel(DaqMxDevice device,
        DigitalInputChannel inputChannel,
        string channelName,
        Range range,
        DaqMxLines lines)
    {
        string physicalChannel = $"{device}/{inputChannel}/line{range.Start}:{range.End}";
        int createChannelStatus = Interop.DAQmxCreateDIChan(_taskHandle,
            physicalChannel,
            channelName,
            (int)lines);
        if (createChannelStatus < 0)
            throw new DaqMxException(createChannelStatus,
                $"Could not create DI Voltage channel '{channelName}' on '{physicalChannel}'");
        Channels++;
        return this;
    }

    public void Dispose()
    {


    }

    public bool ReadAnalogChannelsAsDouble(int numSamplesPerChannel, int timeout, DaqMxFillMode fillMode, Span<double> dataBuffer)
    {
        int requiredSpanSize = numSamplesPerChannel * Channels;
        if (dataBuffer.Length < requiredSpanSize)
            throw new DataException($"Span length too short. (Span length: {dataBuffer.Length}, required length: {requiredSpanSize})");
        int readStatus = Interop.DAQmxReadAnalogF64(_taskHandle,
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
        return true;
    }

    public bool ReadDigitalChannelsAsBits(int numSamplesPerChannel, int timeout, DaqMxFillMode fillMode, Span<double> dataBuffer)
    {
        int requiredSpanSize = numSamplesPerChannel * Channels;
        if (dataBuffer.Length < requiredSpanSize)
            throw new DataException($"Span length too short. (Span length: {dataBuffer.Length}, required length: {requiredSpanSize})");
        int readStatus = Interop.DAQmxReadAnalogF64(_taskHandle,
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
        return true;
    }
}