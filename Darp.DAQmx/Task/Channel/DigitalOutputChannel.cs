namespace Darp.DAQmx.Task.Channel;

public readonly struct DigitalOutputChannel : IDigitalChannel
{
    public int Id { get; }
    public int Width { get; }

    public DigitalOutputChannel(int id, int width)
    {
        Id = id;
        Width = width;
    }

    public override string ToString() => $"port{Id}";
    public static implicit operator int(DigitalOutputChannel channel) => channel.Id;
}