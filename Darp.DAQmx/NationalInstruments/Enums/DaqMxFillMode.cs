namespace Darp.DAQmx.NationalInstruments.Enums;

public enum DaqMxFillMode
{
    /// <summary>
    /// Group by channel (non-interleaved)
    /// </summary>
    GroupByChannel = 0,
    /// <summary>
    /// Group by scan number (interleaved)
    /// </summary>
    GroupByScanNumber = 1,
}