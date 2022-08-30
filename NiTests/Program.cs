using Darp.DAQmx;
using Darp.DAQmx.Device;
using Darp.DAQmx.Task.Analog;
using Darp.DAQmx.Task.Analog.Configuration;
using Darp.DAQmx.Task.Digital;
using Darp.DAQmx.Task.Digital.Configuration;

Console.WriteLine("Hello, World!");

using AnalogInputTask analogTask = DaqMx.CreateTaskFromDevice("Dev1")
    .WithAIVoltageChannel(Usb6001.AI0, configuration =>
    {
        configuration.TerminalConfiguration = InputTerminalConfiguration.Differential;
    })
    .WithAIVoltageChannel(Usb6001.AI1)
    .WithAIVoltageChannel(Usb6001.AI2)
    .WithAIVoltageChannel(Usb6001.AI3)
    .CreateTask();

using DigitalInputTask digitalTask = DaqMx.CreateTaskFromDevice("Dev1")
    .WithDIChannel(Usb6001.P0, 0, 2)
    .CreateTask();

IEnumerable<double[]> analogRows = analogTask.ReadDoublesByChannel(3);
Console.WriteLine(string.Join('\n', analogRows.Select(x => string.Join(",", x))));

IEnumerable<bool[]> digitalRows = digitalTask.ReadBitsByChannel(3);
Console.WriteLine(string.Join('\n', digitalRows.Select(x => string.Join(",", x))));

Console.WriteLine("Done!");