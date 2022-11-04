using Bluetooth.Advertisement;
using Bluetooth.Device;
using Bluetooth.Gatt;
using Darp.NrfBleDriver.Bluetooth;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

Console.WriteLine("Hello, World!");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console(LogEventLevel.Verbose)
    .CreateLogger();

Log.Logger.Verbose("aaaa");

Log.Logger.Information("Hi");

NrfBluetoothService.Setup();
Guid guid = Guid.Parse("0000fd4c-0000-1000-8000-00805f9b34fb");
using var bleController = new NrfBluetoothService("COM5", logger:Log.Logger);
var source = new CancellationTokenSource();
source.CancelAfter(40000);
var startTime = DateTime.UtcNow;


var adv = await bleController
    .AdvertisementScanner(CancellationToken.None)
    .SetSampleInterval(1000, 1000)
    .ScanAsync(source.Token)
    .Where(x => x.ServiceUuids.Contains(guid))
    .FirstAsync();

Log.Logger.Information("{Time} - {@Advertisement}", DateTime.UtcNow - startTime, adv);
await Task.Delay(10000);
IBleDevice? device = await adv.ConnectAsync();
//IBleDevice? device = null;
Log.Logger.Information("Device {@Device}", device);
if (device == null) return;
var services = await device.GetServicesAsync(CacheMode.Cached).ToArrayAsync();
Log.Logger.Information("Services {@Services}", services);
return;

await Task.Delay(100);
/*IReadOnlyList<Device> devices = DaqMx.GetDevices();
Device device = devices
    .First(x => x.ProductType is "USB-6210");
Console.WriteLine($"Using device {device}");

var ci = new CounterInputTask()
    .Channels.AddAngularPositionChannel(device, 1);

var aiValues = new double[7,2048];
var boolValuesA = new double[2048];
var boolValuesB = new double[2048];
var torqueTask = new AnalogInputTask()
    .Channels.AddVoltageChannel(device, 0)
    // .Channels.AddVoltageChannel(device, 1)
    // .Channels.AddVoltageChannel(device, 2)
    // .Channels.AddVoltageChannel(device, 3)
    .Channels.AddVoltageChannel(device, 5)
    .Channels.AddVoltageChannel(device, 6, terminalConfiguration: AITerminalConfiguration.Rse)
    .Channels.AddVoltageChannel(device, 7, terminalConfiguration: AITerminalConfiguration.Rse)
    .Timing.ConfigureSampleClock(60_000, 2000, SampleQuantityMode.ContinuousSamples)
    .OnEveryNSamplesRead(2000, (reader, nSamples) =>
    {
        Console.WriteLine("hi");
    });*/
/*var values = new double[2,2048];
var boolValuesA = new double[2048];
var boolValuesB = new double[2048];

using AnalogInputTask torqueTask = new AnalogInputTask()
    .Channels.AddVoltageChannel(device, 6, terminalConfiguration:AITerminalConfiguration.Rse)
    .Channels.AddVoltageChannel(device, 7, terminalConfiguration: AITerminalConfiguration.Rse)
    .Timing.ConfigureSampleClock(100000, 2000, SampleQuantityMode.ContinuousSamples)
    .OnEveryNSamplesRead(2000, (reader, nSamples) =>
    {
        reader.ReadByChannel(nSamples, values);
        values.ToBool(boolValuesA, 0);
        values.ToBool(boolValuesB, 1);

        double[] x = Transform.FFTmagnitude(boolValuesA);
        int rpm = x.ArgMax().FftToRpm(100000, 2048);
        int dir = values.SamplesToDirection(nSamples, 0, 1);
        Console.WriteLine($"{dir*rpm}");
        // Console.WriteLine(string.Join(",", values.GetRow(0).ToArray().Select(x => x > 2)));
        // Console.WriteLine(string.Join(",", values.GetRow(1).ToArray().Select(x => x > 2)));
    });

using AnalogInputTask torqueTask2 = new AnalogInputTask()
    .Channels.AddVoltageChannel(device, 5)
    .Timing.ConfigureSampleClock(1000, 1000, SampleQuantityMode.ContinuousSamples)
    .OnEveryNSamplesRead(1000, (reader, nSamples) =>
    {
        var res = reader.ReadByChannel(nSamples);
        Console.WriteLine(res.GetRowSpan(0).ToArray().Sum()*63*200/1000);
    });
torqueTask2.Start();*/
// torqueTask.Start();

// await Task.Delay(1000000);
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

