namespace Darp.DAQmx.NationalInstruments.Enums;

public enum DataFillMode
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