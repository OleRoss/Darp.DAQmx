namespace Darp.Smc;

public enum MessageCommand : byte
{
    SetSamplingPeriod = 0,
    StartSampling = 1,
    StopSampling = 2,
    GetStateAndData = 3,
    GetState = 4,
    GetBme280Data = 5,
    GetStateAndTemp = 6
}