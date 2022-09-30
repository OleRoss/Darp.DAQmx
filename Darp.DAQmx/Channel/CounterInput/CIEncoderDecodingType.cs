namespace Darp.DAQmx.Channel.CounterInput;

public enum CIEncoderDecodingType
{
    /// <summary>If signal A leads signal B, count the rising edges of signal A. If signal B leads signal A, count the falling edges of signal A.</summary>
    X1 = 10090, // 0x0000276A
    /// <summary>Count the rising and falling edges of signal A.</summary>
    X2 = 10091, // 0x0000276B
    /// <summary>Count the rising and falling edges of signal A and signal B.</summary>
    X4 = 10092, // 0x0000276C
    /// <summary>Increment the count on rising edges of signal A. Decrement the count on rising edges of signal B.</summary>
    TwoPulseCounting = 10313, // 0x00002849
}