namespace Darp.DAQmx.Task;

public class Usb6210
{
    public static readonly AnalogInputChannel A0 = (AnalogInputChannel)0;
    public static readonly AnalogInputChannel A1 = (AnalogInputChannel)1;
    public static readonly AnalogInputChannel A2 = (AnalogInputChannel)2;
    public static readonly AnalogInputChannel A3 = (AnalogInputChannel)3;
    public static readonly DigitalInputChannel P0 = new(0, 4);
    // public static readonly DigitalInputChannel P1 = new DigitalInputChannel(1, 4);
}

public interface IDaqMxChannel
{
    public int Id { get; }
}

public readonly struct AnalogInputChannel : IDaqMxChannel
{
    public int Id { get; }

    private AnalogInputChannel(int channelId)
    {
        Id = channelId;
    }

    public override string ToString() => $"ai{Id}";
    public static implicit operator int(AnalogInputChannel channel) => channel.Id;
    public static explicit operator AnalogInputChannel(int channelId) => new(channelId);
}

public readonly struct DigitalInputChannel : IDaqMxChannel
{
    public int Id { get; }
    public int Width { get; }

    public DigitalInputChannel(int id, int width)
    {
        Id = id;
        Width = width;
    }

    public override string ToString() => $"port{Id}";
    public static implicit operator int(DigitalInputChannel channel) => channel.Id;
}