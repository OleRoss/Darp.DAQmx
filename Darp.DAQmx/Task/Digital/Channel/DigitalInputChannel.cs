namespace Darp.DAQmx.Task.Digital.Channel;

public readonly struct DigitalInputChannel : IDigitalChannel
{
    public int Id { get; }
    public int Width { get; }

    public DigitalInputChannel(int id, int width)
    {
        Id = id;
        Width = width;
    }

    public override string ToString() => $"Digital Input Port {Id} ({Width} lines)";
    public static implicit operator int(DigitalInputChannel channel) => channel.Id;
}