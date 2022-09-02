namespace Darp.DAQmx.Channel.AnalogOutput;

/// <summary>Indicates whether the channel generates voltage,  current, or a waveform.</summary>
/// <remarks>Indicates whether the channel generates voltage,  current, or a waveform.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.AOChannel.OutputType" />.</remarks>
public enum AOOutputType
{
    /// <summary>Current generation.</summary>
    Current = 10134, // 0x00002796
    /// <summary>Voltage generation.</summary>
    Voltage = 10322, // 0x00002852
    /// <summary>Function generation.</summary>
    FunctionGeneration = 14750, // 0x0000399E
}