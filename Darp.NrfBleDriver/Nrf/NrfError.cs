namespace Darp.NrfBleDriver.Nrf;

internal class NrfErrorConstants
{
    /// Global error base
    internal const int NrfErrorBaseNum = 0x0;
    /// SDM error base
    internal const int NrfErrorSdmBaseNum = 0x1000;
    /// SoC error base
    internal const int NrfErrorSocBaseNum = 0x2000;
    /// STK error base
    internal const int NrfErrorStkBaseNum = 0x3000;
}


public enum NrfError : uint
{
    /// Successful command
    NrfSuccess = NrfErrorConstants.NrfErrorBaseNum + 0,
    /// SVC handler is missing
    NrfErrorSvcHandlerMissing = NrfErrorConstants.NrfErrorBaseNum + 1,
    /// SoftDevice has not been enabled
    NrfErrorSoftdeviceNotEnabled = NrfErrorConstants.NrfErrorBaseNum + 2,
    /// Internal Error
    NrfErrorInternal = NrfErrorConstants.NrfErrorBaseNum + 3,
    /// No Memory for operation
    NrfErrorNoMem = NrfErrorConstants.NrfErrorBaseNum + 4,
    /// Not found
    NrfErrorNotFound = NrfErrorConstants.NrfErrorBaseNum + 5,
    /// Not supported
    NrfErrorNotSupported = NrfErrorConstants.NrfErrorBaseNum + 6,
    /// Invalid Parameter
    NrfErrorInvalidParam = NrfErrorConstants.NrfErrorBaseNum + 7,
    /// Invalid state, operation disallowed in this state
    NrfErrorInvalidState = NrfErrorConstants.NrfErrorBaseNum + 8,
    /// Invalid Length
    NrfErrorInvalidLength = NrfErrorConstants.NrfErrorBaseNum + 9,
    /// Invalid Flags
    NrfErrorInvalidFlags = NrfErrorConstants.NrfErrorBaseNum + 10,
    /// Invalid Data
    NrfErrorInvalidData = NrfErrorConstants.NrfErrorBaseNum + 11,
    /// Invalid Data size
    NrfErrorDataSize = NrfErrorConstants.NrfErrorBaseNum + 12,
    /// Operation timed out
    NrfErrorTimeout = NrfErrorConstants.NrfErrorBaseNum + 13,
    /// Null Pointer
    NrfErrorNull = NrfErrorConstants.NrfErrorBaseNum + 14,
    /// Forbidden Operation
    NrfErrorForbidden = NrfErrorConstants.NrfErrorBaseNum + 15,
    /// Bad Memory Address
    NrfErrorInvalidAddr = NrfErrorConstants.NrfErrorBaseNum + 16,
    /// Busy
    NrfErrorBusy = NrfErrorConstants.NrfErrorBaseNum + 17,
    /// Maximum connection count exceeded.
    NrfErrorConnCount = NrfErrorConstants.NrfErrorBaseNum + 18,
    /// Not enough resources for operation
    NrfErrorResources = NrfErrorConstants.NrfErrorBaseNum + 19
}