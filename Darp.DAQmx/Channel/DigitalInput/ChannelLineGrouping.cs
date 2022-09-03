namespace Darp.DAQmx.Channel.DigitalInput;

/// <summary>Specifies how to group digital lines into one or more <see href="javascript:LaunchMergedHelp('daqhelp.chm','mxcncpts.chm::/virtChanTypes.html');">virtual channels</see>.</summary>
public enum ChannelLineGrouping
{
    /// <summary>A separate <see href="javascript:LaunchMergedHelp('daqhelp.chm','mxcncpts.chm::/virtChanTypes.html');">virtual channel</see> is created for each digital line.</summary>
    OneChannelForEachLine,
    /// <summary>All digital lines are combined into a single <see href="javascript:LaunchMergedHelp('daqhelp.chm','mxcncpts.chm::/virtChanTypes.html');">virtual channel</see>.</summary>
    OneChannelForAllLines,
}