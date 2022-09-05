namespace Darp.DAQmx.Timing;

/// <summary>Specifies if a task acquires or generates a finite number of samples or if it continuously acquires or generates samples.</summary>
/// <remarks>Specifies if a task acquires or generates a finite number of samples or if it continuously acquires or generates samples.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.Timing.SampleQuantityMode" />.</remarks>
public enum SampleQuantityMode
{
    /// <summary>Acquire or generate samples until you stop the task.</summary>
    ContinuousSamples = 10123, // 0x0000278B
    /// <summary>Acquire or generate a finite number of samples.</summary>
    FiniteSamples = 10178, // 0x000027C2
    /// <summary>Acquire or generate samples continuously using hardware timing without a buffer. <see href="javascript:launchSharedHelp('mxcncpts.chm::/HWTSPSampleMode.html');">Hardware timed single point</see> sample mode is supported only for the sample clock and change detection timing types.</summary>
    HardwareTimedSinglePoint = 12522, // 0x000030EA
}