namespace Darp.DAQmx.Task.Channel;

public readonly struct AnalogInputChannel : IChannel
{
    public int Id { get; }

    public AnalogInputChannel(int channelId)
    {
        Id = channelId;
    }

    public override string ToString() => $"ai{Id}";
    public static implicit operator int(AnalogInputChannel channel) => channel.Id;
}