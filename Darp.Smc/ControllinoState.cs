namespace Darp.Smc;

public enum ControllinoState : byte
{
    Error = 0,
    Initialized = 1,
    Running = 2,
    Stopped = 3,
    NotInitialized = 4,
    Idle = 5
}