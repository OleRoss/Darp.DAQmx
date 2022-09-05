namespace Darp.DAQmx.Timing;

/// <summary>Specifies on which edge of a clock pulse sampling takes place. This property is useful primarily when the signal you use as the Sample Clock is not a periodic clock.</summary>
/// <remarks>Specifies on which edge of a clock pulse sampling takes place. This property is useful primarily when the signal you use as the Sample Clock is not a periodic clock.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.Timing.SampleClockActiveEdge" />.</remarks>
public enum SampleClockActiveEdge
{
    /// <summary>Falling edge(s).</summary>
    Falling = 10171, // 0x000027BB
    /// <summary>Rising edge(s).</summary>
    Rising = 10280, // 0x00002828
}