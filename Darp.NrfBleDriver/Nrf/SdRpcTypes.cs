namespace Darp.NrfBleDriver.Nrf;

/// Flow control modes
public enum SdRpcFlowControlT
{
    FlowControlNone,
    FlowControlHardware
}

/// Parity modes
public enum SdRpcParityT
{
    SdRpcParityNone,
    SdRpcParityEven
}

/// Levels of severity that a log message can be associated with
public enum SdRpcLogSeverityT
{
    Trace,
    Debug,
    Info,
    Warning,
    Error,
    Fatal
}

/// Error codes that an error callback can be associated with
public enum SdRpcAppStatusT
{
    PktSendMaxRetriesReached,
    PktUnexpected,
    PktEncodeError,
    PktDecodeError,
    PktSendError,
    IoResourcesUnavailable,
    ResetPerformed,
    ConnectionActive
}

