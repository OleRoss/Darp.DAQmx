namespace Darp.DAQmx.Channel.CounterInput;

/// <summary>Specifies the units to use to return angular position measurements from the channel.</summary>
/// <remarks>Specifies the units to use to return angular position measurements from the channel.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.CIChannel.AngularEncoderUnits" />.</remarks>
public enum CIAngularEncoderUnits
{
    /// <summary>Units a <see href="javascript:launchSharedHelp('mxcncpts.chm::/customScales.html');">custom scale</see> specifies. If you select this value, you must specify a custom scale name.</summary>
    FromCustomScale = 10065, // 0x00002751
    /// <summary>Degrees.</summary>
    Degrees = 10146, // 0x000027A2
    /// <summary>Radians.</summary>
    Radians = 10273, // 0x00002821
    /// <summary>Ticks.</summary>
    Ticks = 10304, // 0x00002840
}