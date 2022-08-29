using Darp.DAQmx.NationalInstruments.Enums;

namespace Darp.DAQmx.Task;

public static class DaqMxTaskExtensions
{
    public static DaqMxTask WithAnalogInputChannel(this DaqMxTask task,
        DaqMxDevice device, AnalogInputChannel inputChannel, string channelName,
        DaqMxInputTerminalConfiguration terminalConfiguration = DaqMxInputTerminalConfiguration.Differential,
        int minValue = -10, int maxValue = 10) =>
        task.WithAnalogInputChannel(
            device, inputChannel, channelName,
            terminalConfiguration, minValue, maxValue, DaqMxUnit.Volts);

    public static double[] ReadAnalogChannelsAsDouble(this DaqMxTask task, int numSamplesPerChannel, int timeout,
        DaqMxFillMode fillMode)
    {
        var doubleArray = new double[task.Channels*numSamplesPerChannel];
        task.ReadAnalogChannelsAsDouble(numSamplesPerChannel, timeout, fillMode, doubleArray);
        return doubleArray;
    }
}