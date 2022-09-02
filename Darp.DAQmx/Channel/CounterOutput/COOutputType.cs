namespace Darp.DAQmx.Channel.CounterOutput;

/// <summary>Indicates how to define pulses generated on the channel.</summary>
/// <remarks>Indicates how to define pulses generated on the channel.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.COChannel.OutputType" />.</remarks>
public enum COOutputType
{
    /// <summary>Generate digital pulses defined by frequency and duty cycle.</summary>
    PulseFrequency = 10119, // 0x00002787
    /// <summary>Generate digital pulses defined by the number of timebase ticks that the pulse is at a low state and the number of timebase ticks that the pulse is at a high state.</summary>
    PulseTicks = 10268, // 0x0000281C
    /// <summary>Generate pulses defined by the time the pulse is at a low state and the time the pulse is at a high state.</summary>
    PulseTime = 10269, // 0x0000281D
}