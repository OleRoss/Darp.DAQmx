using System;
using System.Runtime.InteropServices;

namespace Darp.DAQmx.NationalInstruments.Functions;

internal partial class Interop
{
    //[DllImport(lib, CallingConvention = CallingConvention.StdCall)]
    //internal static extern int DAQmxAddGlobalChansToTask(string taskName, out IntPtr taskHandle);

    /// int32 __CFUNC     DAQmxLoadTask                  (const char taskName[], TaskHandle *taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxLoadTask(string taskName, out IntPtr taskHandle);

    /// int32 __CFUNC     DAQmxCreateTask                (const char taskName[], TaskHandle *taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxCreateTask(string taskName, out IntPtr taskHandle);

    // Channel Names must be valid channels already available in MAX. They are not created.
    // int32 __CFUNC     DAQmxAddGlobalChansToTask      (TaskHandle taskHandle, const char channelNames[]);

    /// int32 __CFUNC     DAQmxStartTask                 (TaskHandle taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxStartTask(IntPtr taskHandle);

    /// int32 __CFUNC     DAQmxStopTask                  (TaskHandle taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxStopTask(IntPtr taskHandle);

    /// int32 __CFUNC     DAQmxClearTask                 (TaskHandle taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxClearTask(IntPtr taskHandle);

    // int32 __CFUNC     DAQmxWaitUntilTaskDone         (TaskHandle taskHandle, float64 timeToWait);
    // int32 __CFUNC     DAQmxWaitForValidTimestamp     (TaskHandle taskHandle, int32 timestampEvent, float64 timeout, CVIAbsoluteTime* timestamp);
    // int32 __CFUNC     DAQmxIsTaskDone                (TaskHandle taskHandle, bool32 *isTaskDone);
    // 
    // int32 __CFUNC     DAQmxTaskControl               (TaskHandle taskHandle, int32 action);
    // 
    // int32 __CFUNC     DAQmxGetNthTaskChannel         (TaskHandle taskHandle, uInt32 index, char buffer[], int32 bufferSize);
    // 
    // int32 __CFUNC     DAQmxGetNthTaskDevice          (TaskHandle taskHandle, uInt32 index, char buffer[], int32 bufferSize);
    // 
    // int32 __CFUNC_C   DAQmxGetTaskAttribute          (TaskHandle taskHandle, int32 attribute, void *value, ...);
    // 
    // typedef int32 (CVICALLBACK *DAQmxEveryNSamplesEventCallbackPtr)(TaskHandle taskHandle, int32 everyNsamplesEventType, uInt32 nSamples, void *callbackData);
    // typedef int32 (CVICALLBACK *DAQmxDoneEventCallbackPtr)(TaskHandle taskHandle, int32 status, void *callbackData);
    // typedef int32 (CVICALLBACK *DAQmxSignalEventCallbackPtr)(TaskHandle taskHandle, int32 signalID, void *callbackData);
    // 
    // int32 __CFUNC     DAQmxRegisterEveryNSamplesEvent(TaskHandle task, int32 everyNsamplesEventType, uInt32 nSamples, uInt32 options, DAQmxEveryNSamplesEventCallbackPtr callbackFunction, void *callbackData);
    // int32 __CFUNC     DAQmxRegisterDoneEvent         (TaskHandle task, uInt32 options, DAQmxDoneEventCallbackPtr callbackFunction, void *callbackData);
    // int32 __CFUNC     DAQmxRegisterSignalEvent       (TaskHandle task, int32 signalID, uInt32 options, DAQmxSignalEventCallbackPtr callbackFunction, void *callbackData);
}