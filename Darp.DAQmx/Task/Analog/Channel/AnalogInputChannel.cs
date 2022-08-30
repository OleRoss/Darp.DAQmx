namespace Darp.DAQmx.Task.Analog.Channel;

public readonly struct AnalogInputChannel : IAnalogChannel
{
    public int Id { get; }

    public AnalogInputChannel(int channelId)
    {
        Id = channelId;
    }

    public override string ToString() => $"Analog Input {Id}";
    public static implicit operator int(AnalogInputChannel channel) => channel.Id;
}