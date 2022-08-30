namespace Darp.DAQmx.Task.Analog.Channel;

public readonly struct AnalogChannel : IAnalogChannel
{
    public int Id { get; }

    public AnalogChannel(int channelId)
    {
        Id = channelId;
    }

    public override string ToString() => $"Analog Output {Id}";
    public static implicit operator int(AnalogChannel channel) => channel.Id;
}