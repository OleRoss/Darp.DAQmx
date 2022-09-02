namespace Darp.DAQmx.Channel.AnalogInput;

public enum AITerminalConfiguration
{
    /// <summary>Non-Referenced Single-Ended.</summary>
    Nrse = 10078, // 0x0000275E
    /// <summary>Referenced Single-Ended.</summary>
    Rse = 10083, // 0x00002763
    /// <summary>Differential.</summary>
    Differential = 10106, // 0x0000277A
    /// <summary>Pseudodifferential.</summary>
    Pseudodifferential = 12529, // 0x000030F1
}