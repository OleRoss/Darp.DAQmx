namespace Darp.DAQmx.Channel.CounterInput;

/// <summary>Specifies whether to increment or decrement the counter on each edge.</summary>
/// <remarks>Specifies whether to increment or decrement the counter on each edge.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.CIChannel.CountEdgesCountDirection" />.</remarks>
public enum CICountDirection
{
    /// <summary>Decrement counter.</summary>
    Down = 10124, // 0x0000278C
    /// <summary>Increment counter.</summary>
    Up = 10128, // 0x00002790
    /// <summary>The state of a digital line controls the count direction. Each counter has a <see href="javascript:launchSharedHelp('mxdevconsid.chm::/counteroverview.html');">default count direction terminal</see>.</summary>
    ExternallyControlled = 10326, // 0x00002856
}