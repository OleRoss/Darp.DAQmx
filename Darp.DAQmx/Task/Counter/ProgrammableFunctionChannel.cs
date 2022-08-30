using Darp.DAQmx.Task.Common;

namespace Darp.DAQmx.Task.Counter;

public readonly struct ProgrammableFunctionChannel : IChannel
{
    public int Id { get; }

    public ProgrammableFunctionChannel(int channelId)
    {
        Id = channelId;
    }

    public override string ToString() => $"Analog Output {Id}";
    public static implicit operator int(ProgrammableFunctionChannel channel) => channel.Id;
}