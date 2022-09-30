namespace Darp.DAQmx.Channel.CounterInput;

/// <summary>Specifies the states at which signal A and signal B must be while signal Z is high for NI-DAQmx to reset the measurement. If signal Z is never high while signal A and signal B are high, for example, you must choose a phase other than <see cref="F:NationalInstruments.DAQmx.CIEncoderZIndexPhase.AHighBHigh" />.</summary>
/// <remarks>Specifies the states at which signal A and signal B must be while signal Z is high for NI-DAQmx to reset the measurement. If signal Z is never high while signal A and signal B are high, for example, you must choose a phase other than <see cref="F:NationalInstruments.DAQmx.CIEncoderZIndexPhase.AHighBHigh" />.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.CIChannel.EncoderZIndexPhase" />.</remarks>
public enum CIEncoderZIndexPhase
{
    /// <summary>Reset the measurement when signal A and signal B are high.</summary>
    AHighBHigh = 10040, // 0x00002738
    /// <summary>Reset the measurement when signal A is high and signal B is low.</summary>
    AHighBLow = 10041, // 0x00002739
    /// <summary>Reset the measurement when signal A is low and signal B high.</summary>
    ALowBHigh = 10042, // 0x0000273A
    /// <summary>Reset the measurement when signal A and signal B are low.</summary>
    ALowBLow = 10043, // 0x0000273B
}