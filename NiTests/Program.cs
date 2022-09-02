using Darp.DAQmx;
using Darp.DAQmx.Channel;
using Darp.DAQmx.Channel.AnalogInput;
using Darp.DAQmx.Channel.AnalogOutput;
using Darp.DAQmx.Task;

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
var analogTask = new AnalogInputTask();

try
{
    analogTask
        .Channels.AddVoltageChannel("Dev3", 0)
        .Channels.AddVoltageChannel("Dev3", 1)
        .Channels.AddVoltageChannel("Dev3", 2)
        .Channels.AddVoltageChannel("Dev3", 3);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

MultiChannelReader<AnalogInputTask, IAnalogInputChannel> singleChannelReader = analogTask.GetReader();
var doubleArray = new double[100];
singleChannelReader.ReadAnalogF64(2, doubleArray);
Console.WriteLine(string.Join('\n', doubleArray.Select(x => string.Join(',', x))));

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

