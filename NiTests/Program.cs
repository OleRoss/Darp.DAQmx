using Darp.DAQmx;
using Darp.DAQmx.NationalInstruments.Enums;
using Darp.DAQmx.Task.Configuration;
using Darp.DAQmx.Task.Configuration.Task;
using Darp.DAQmx.Task.Device;

Console.WriteLine("Hello, World!");

using AnalogInputTask analogTask = DaqMx.CreateTaskFromDevice("Dev1")
    .WithAIVoltageChannel(Usb6210.A0, configuration =>
    {
        configuration.TerminalConfiguration = InputTerminalConfiguration.Differential;
    })
    .WithAIVoltageChannel(Usb6210.A1)
    .WithAIVoltageChannel(Usb6210.A2)
    .WithAIVoltageChannel(Usb6210.A3)
    .CreateTask();

using DigitalInputTask digitalTask = DaqMx.CreateTaskFromDevice("Dev1")
    .WithDILines(2, 3, 4)
    .WithDIChannel(Usb6210.P0, 0)
    .CreateTask();

IEnumerable<double[]> analogRows = analogTask.ReadDoublesByChannel(3);
Console.WriteLine(string.Join('\n', analogRows.Select(x => string.Join(",", x))));

IEnumerable<bool[]> digitalRows = digitalTask.ReadBitsByChannel(3);
Console.WriteLine(string.Join('\n', digitalRows.Select(x => string.Join(",", x))));

Console.WriteLine("Done!");