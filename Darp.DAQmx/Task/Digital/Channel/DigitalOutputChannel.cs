namespace Darp.DAQmx.Task.Digital.Channel;

public readonly struct DigitalOutputChannel : IDigitalChannel
{
    public int Id { get; }
    public int Width { get; }

    public DigitalOutputChannel(int id, int width)
    {
        Id = id;
        Width = width;
    }

    public override string ToString() => $"Digital Output Port {Id} ({Width} lines";
    public static implicit operator int(DigitalOutputChannel channel) => channel.Id;
}