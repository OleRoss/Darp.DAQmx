namespace Darp.DAQmx.Task.Analog.Configuration;

public enum InputTerminalConfiguration
{
    /// <summary>
    /// Measures relative to the AI SENSITIVE input
    /// </summary>
    NRse = 10078, // 0x0000275E
    /// <summary>
    /// Measures relative to the AI ground
    /// </summary>
    Rse = 10083, // 0x00002763
    /// <summary>
    /// Measures the difference between two AI signals
    /// </summary>
    Differential = 10106, // 0x0000277A
    PseudoDifferential = 12529, // 0x000030F1
}