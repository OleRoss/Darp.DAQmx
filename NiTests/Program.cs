using System.IO.Ports;
using Darp.Smc;

Console.WriteLine("Hello, World!");


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
/*IReadOnlyList<Device> devices = DaqMx.GetDevices();
Device deviceOne = devices
    .First(x => x.ProductType is "USB-6210");
AnalogInputTask analogTask = new AnalogInputTask()
    .Channels.AddVoltageChannel(deviceOne, 0)
    .Channels.AddVoltageChannel(deviceOne, 1)
    .Channels.AddVoltageChannel(deviceOne, 2)
    .Channels.AddVoltageChannel(deviceOne, 3);

var aiMultiReader = analogTask.Channels.GetMultiReader();
Span2D<double> doubleArray = new double[4, 5];
aiMultiReader.ReadByChannel(3, doubleArray);
for (var i = 0; i < doubleArray.Height; i++)
    Console.WriteLine(string.Join(',', doubleArray.GetRow(i).ToArray()));

DigitalInputTask digitalTask = new DigitalInputTask()
    .Channels.AddChannel(deviceOne, 0, 1, 3);

var diMultiArray = digitalTask.Channels.GetMultiReader();
Span2D<bool> byteArray = new bool[5, 3];
diMultiArray.ReadByScanNumber(4, byteArray);
for (var i = 0; i < byteArray.Height; i++)
    Console.WriteLine(string.Join(',', byteArray.GetRow(i).ToArray()));

CounterInputTask counterTask = new CounterInputTask()
    .Channels.AddCountEdgesChannel(deviceOne, 0);

var ciSingleReader = counterTask.Channels.GetSingleReader();
ciSingleReader.ReadScalar(out double doubleValue);
Console.WriteLine(doubleValue);
*/

Console.WriteLine("Done!");

