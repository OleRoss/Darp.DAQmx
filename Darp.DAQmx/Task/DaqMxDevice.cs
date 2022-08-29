namespace Darp.DAQmx.Task;

public readonly struct DaqMxDevice
{
    public static readonly DaqMxDevice Dev0 = new("dev0");
    public static readonly DaqMxDevice Dev1 = new("dev1");

    public string DeviceName { get; }

    private DaqMxDevice(string deviceName)
    {
        DeviceName = deviceName;
    }

    public override string ToString() => DeviceName;
}