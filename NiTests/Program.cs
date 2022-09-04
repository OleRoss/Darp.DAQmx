using Darp.DAQmx;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Channel.AnalogInput;
using Darp.DAQmx.Channel.CounterInput;
using Darp.DAQmx.Channel.DigitalInput;
using Darp.DAQmx.Task;
using Microsoft.Toolkit.HighPerformance;

Console.WriteLine("Hello, World!");



// AnalogInputTask : InputTask<IAnalogChannel>
// IInputTask : ITask
// Device device = Device.Find("Dev1");

/*
{
    Channels: [
        Voltage: {
            PhysicalChannel: "Dev1/ao1",
            MinVoltage: -10,
            MaxVoltage: 10
        }
    ]
}
*/

IReadOnlyList<Device> devices = DaqMx.GetDevices();
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


// var channels = analogTask.Channels;
// 
// Span<double> doubleSpan = stackalloc double[100];
// Span2D<double> doubles = Span2D<double>.DangerousCreate(ref doubleSpan[0], 10, 10, 0);
// 
// using DigitalInputTask digitalTask = DaqMx.CreateTaskFromDevice("Dev1")
//     .WithDIChannel(Usb6001.P0, 0, 2)
//     .CreateTask();
// 
// using CounterInputTask counterTask = DaqMx.CreateTaskFromDevice("Dev1")
//     .WithCountEdges(Usb6001.PFI0)
//     .CreateTask();
// 
// IEnumerable<double[]> analogRows = analogTask.ReadDoublesByChannel(3);
// Console.WriteLine(string.Join('\n', analogRows.Select(x => string.Join(",", x))));
// 
// IEnumerable<bool[]> digitalRows = digitalTask.ReadBitsByChannel(3);
// Console.WriteLine(string.Join('\n', digitalRows.Select(x => string.Join(",", x))));

Console.WriteLine("Done!");

