namespace Darp.DAQmx.Channel.CounterInput;

/// <summary>Specifies on which edges to increment or decrement the counter.</summary>
/// <remarks>Specifies on which edges to increment or decrement the counter.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.CIChannel.CountEdgesActiveEdge" />.</remarks>
public enum CIActiveEdge
{
    /// <summary>Falling edge(s).</summary>
    Falling = 10171, // 0x000027BB
    /// <summary>Rising edge(s).</summary>
    Rising = 10280, // 0x00002828
}