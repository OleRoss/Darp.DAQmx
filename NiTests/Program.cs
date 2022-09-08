using Darp.DAQmx;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Channel.AnalogInput;
using Darp.DAQmx.Channel.CounterInput;
using Darp.DAQmx.Channel.DigitalInput;
using Darp.DAQmx.Event;
using Darp.DAQmx.Task;
using Darp.DAQmx.Timing;
using FftSharp;
using Microsoft.Toolkit.HighPerformance;

Console.WriteLine("Hello, World!");

IReadOnlyList<Device> devices = DaqMx.GetDevices();
Device device = devices
    .First(x => x.ProductType is "USB-6210");
Console.WriteLine($"Using device {device}");
using AnalogInputTask torqueTask = new AnalogInputTask()
    .Channels.AddVoltageChannel(device, 6, terminalConfiguration:AITerminalConfiguration.Rse, configuration:channel =>
    {
        channel.ADCTimingMode = ADCTimingMode.HighSpeed;
    })
    .Channels.AddVoltageChannel(device, 7, terminalConfiguration: AITerminalConfiguration.Rse, configuration:channel =>
    {
        channel.ADCTimingMode = ADCTimingMode.HighSpeed;
    })
    .Timing.ConfigureSampleClock(1000, 100, SampleQuantityMode.ContinuousSamples)
    .OnEveryNSamplesRead(100, (reader, samples) =>
    {
        Span2D<double> values = new double[2,512];
        reader.ReadByChannel(samples, values);
        double[] buffer = values.GetRowSpan(0).ToArray().Select<double, double>(x => x > 2 ? 1 : 0).ToArray();

        // Console.WriteLine(string.Join(",", values.GetRowSpan(0).ToArray()));
        double[] x = Transform.FFTmagnitude(buffer);
        double diff = Transform.FFTfreqPeriod(100000, 512);
        double max = 0;
        int maxI = -1;
        for (var n = 0; n < x.Length; n++)
        {
            if (x[n] > max)
            {
                max = x[n];
                maxI = n;
            }
        }
        Console.WriteLine($"{maxI} - {diff * maxI / 60}");
        // Console.WriteLine(string.Join(",", values.GetRow(0).ToArray().Select(x => x > 2)));
        // Console.WriteLine(string.Join(",", values.GetRow(1).ToArray().Select(x => x > 2)));
    });
torqueTask.Start();

await Task.Delay(100000);
/*var bytes = new double[4, 1000];
using AnalogInputTask analogTask = new AnalogInputTask()
    .Channels.AddVoltageChannel(deviceOne, 0)
    .Channels.AddVoltageChannel(deviceOne, 1)
    .Channels.AddVoltageChannel(deviceOne, 2)
    .Channels.AddVoltageChannel(deviceOne, 3)
    .Timing.ConfigureSampleClock(60000, 1000, SampleQuantityMode.ContinuousSamples)
    .OnEveryNSamplesRead(1000, (reader, samples) =>
    {
        reader.ReadByChannel(samples, bytes);
        Span2D<double> span = bytes;
        for (var i = 0; i < span.Height; i++)
            Console.Write($"{span.GetRow(i).ToArray().Sum() / 1000},");
        Console.WriteLine();
    });

analogTask.Start();

await Task.Delay(10000);

analogTask.Stop();
await Task.Delay(10);*/
/*
var aiMultiReader = analogTask.Channels.GetMultiReader();
Span2D<double> doubleArray = new double[4, 5];
aiMultiReader.ReadByChannel(3, doubleArray);
for (var i = 0; i < doubleArray.Height; i++)
    Console.WriteLine(string.Join(',', doubleArray.GetRow(i).ToArray()));

DigitalInputTask digitalTask = new DigitalInputTask()
    .Channels.AddChannel(deviceOne, 0, 1, 3);

var diMultiArray = digitalTask.Channels.GetMultiReader();
Span2D<bool> byteArray = new bool[5, 3];
diMultiArray.ReadByScanNumber( 4, byteArray);
for (var i = 0; i < byteArray.Height; i++)
    Console.WriteLine(string.Join(',', byteArray.GetRow(i).ToArray()));

CounterInputTask counterTask = new CounterInputTask()
    .Channels.AddCountEdgesChannel(deviceOne, 0);

var ciSingleReader = counterTask.Channels.GetSingleReader();
ciSingleReader.ReadScalar(out double doubleValue);
Console.WriteLine(doubleValue);
/*
using var serialPort = new SerialPort
{
    PortName = "COM7",
    DtrEnable = true,
    BaudRate = 115200
};
serialPort.Open();
await serialPort.WaitUntilInitializedAsync();

Console.WriteLine(await serialPort.GetStateAsync());
Console.WriteLine(await serialPort.GetStateAndTempAsync());
Console.WriteLine(await serialPort.GetBme280DataAsync());
await serialPort.StartSamplingAsync();
serialPort.SubscribeToSamples(Console.WriteLine);
await Task.Delay(1000);
await serialPort.StopSamplingAsync();
*/

Console.WriteLine("Done!");

