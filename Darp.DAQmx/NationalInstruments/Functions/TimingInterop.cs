using System;
using System.Runtime.InteropServices;

namespace Darp.DAQmx.NationalInstruments.Functions;

internal partial class Interop
{
    /// (Analog/Counter Timing)
    /// int32 __CFUNC     DAQmxCfgSampClkTiming          (TaskHandle taskHandle, const char source[], float64 rate, int32 activeEdge, int32 sampleMode, uInt64 sampsPerChan);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)] // (Analog/Counter Timing)
    internal static extern int DAQmxCfgSampClkTiming(IntPtr taskHandle, string source, double rate, int activeEdge,
        int sampleMode, ulong sampsPerChan);

    // // (Digital Timing)
    // int32 __CFUNC     DAQmxCfgHandshakingTiming      (TaskHandle taskHandle, int32 sampleMode, uInt64 sampsPerChan);
    // // (Burst Import Clock Timing)
    // int32 __CFUNC     DAQmxCfgBurstHandshakingTimingImportClock(TaskHandle taskHandle, int32 sampleMode, uInt64 sampsPerChan, float64 sampleClkRate, const char sampleClkSrc[], int32 sampleClkActiveEdge, int32 pauseWhen, int32 readyEventActiveLevel);
    // // (Burst Export Clock Timing)
    // int32 __CFUNC     DAQmxCfgBurstHandshakingTimingExportClock(TaskHandle taskHandle, int32 sampleMode, uInt64 sampsPerChan, float64 sampleClkRate, const char sampleClkOutpTerm[], int32 sampleClkPulsePolarity, int32 pauseWhen, int32 readyEventActiveLevel);
    // int32 __CFUNC     DAQmxCfgChangeDetectionTiming  (TaskHandle taskHandle, const char risingEdgeChan[], const char fallingEdgeChan[], int32 sampleMode, uInt64 sampsPerChan);
    // // (Counter Timing)
    // int32 __CFUNC     DAQmxCfgImplicitTiming         (TaskHandle taskHandle, int32 sampleMode, uInt64 sampsPerChan);
    // // (Pipelined Sample Clock Timing)
    // int32 __CFUNC     DAQmxCfgPipelinedSampClkTiming (TaskHandle taskHandle, const char source[], float64 rate, int32 activeEdge, int32 sampleMode, uInt64 sampsPerChan);
    // 
    // int32 __CFUNC_C   DAQmxGetTimingAttribute        (TaskHandle taskHandle, int32 attribute, void *value, ...);
    // int32 __CFUNC_C   DAQmxSetTimingAttribute        (TaskHandle taskHandle, int32 attribute, ...);
    // int32 __CFUNC     DAQmxResetTimingAttribute      (TaskHandle taskHandle, int32 attribute);
    // 
    // int32 __CFUNC_C   DAQmxGetTimingAttributeEx      (TaskHandle taskHandle, const char deviceNames[], int32 attribute, void *value, ...);
    // int32 __CFUNC_C   DAQmxSetTimingAttributeEx      (TaskHandle taskHandle, const char deviceNames[], int32 attribute, ...);
    // int32 __CFUNC     DAQmxResetTimingAttributeEx    (TaskHandle taskHandle, const char deviceNames[], int32 attribute);
}