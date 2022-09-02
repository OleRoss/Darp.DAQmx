using System;
using System.Runtime.InteropServices;

namespace Darp.DAQmx.NationalInstruments.Functions;

internal partial class Interop
{
    // /// int32 __CFUNC     DAQmxReadAnalogF64             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, float64 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    // [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    // public static extern int DAQmxReadAnalogF64(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode,
    //     in double readArray, uint arraySizeInSamps, out IntPtr sampsPerChanRead, IntPtr reserved);

    // int32 __CFUNC     DAQmxReadAnalogScalarF64       (TaskHandle taskHandle, float64 timeout, float64 *value, bool32 *reserved);


    // int32 __CFUNC     DAQmxReadBinaryI16             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, int16 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    // 
    // int32 __CFUNC     DAQmxReadBinaryU16             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt16 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    // 
    // int32 __CFUNC     DAQmxReadBinaryI32             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, int32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    // 
    // int32 __CFUNC     DAQmxReadBinaryU32             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);

    /// int32 __CFUNC     DAQmxReadDigitalU8             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt8 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern int DAQmxReadDigitalU8(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode,
        in byte readArray, uint arraySizeInSamps, out IntPtr sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadDigitalU16            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt16 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern int DAQmxReadDigitalU16(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode,
        in ushort readArray, uint arraySizeInSamps, out IntPtr sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadDigitalU32            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern int DAQmxReadDigitalU32(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode,
        in uint readArray, uint arraySizeInSamps, out IntPtr sampsPerChanRead, IntPtr reserved);

    // int32 __CFUNC     DAQmxReadDigitalScalarU32      (TaskHandle taskHandle, float64 timeout, uInt32 *value, bool32 *reserved);
    // int32 __CFUNC     DAQmxReadDigitalLines          (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt8 readArray[], uInt32 arraySizeInBytes, int32 *sampsPerChanRead, int32 *numBytesPerSamp, bool32 *reserved);

    // int32 __CFUNC     DAQmxReadCounterF64            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, float64 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    /// int32 __CFUNC     DAQmxReadCounterU32            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern int DAQmxReadCounterU32(IntPtr taskHandle, int numSampsPerChan, double timeout, in uint readArray, uint arraySizeInSamps, out IntPtr sampsPerChanRead, IntPtr reserved);

    // int32 __CFUNC     DAQmxReadCounterF64Ex          (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, float64 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    /// int32 __CFUNC     DAQmxReadCounterU32Ex          (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern int DAQmxReadCounterU32Ex(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in uint readArray, uint arraySizeInSamps, out IntPtr sampsPerChanRead, IntPtr reserved);
    // int32 __CFUNC     DAQmxReadCounterScalarF64      (TaskHandle taskHandle, float64 timeout, float64 *value, bool32 *reserved);
    // int32 __CFUNC     DAQmxReadCounterScalarU32      (TaskHandle taskHandle, float64 timeout, uInt32 *value, bool32 *reserved);
    // 
    // 
    // 
    // int32 __CFUNC     DAQmxReadCtrFreq               (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 interleaved, float64 readArrayFrequency[], float64 readArrayDutyCycle[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    // int32 __CFUNC     DAQmxReadCtrTime               (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 interleaved, float64 readArrayHighTime[], float64 readArrayLowTime[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    // int32 __CFUNC     DAQmxReadCtrTicks              (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 interleaved, uInt32 readArrayHighTicks[], uInt32 readArrayLowTicks[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    // 
    // int32 __CFUNC     DAQmxReadCtrFreqScalar         (TaskHandle taskHandle, float64 timeout, float64 *frequency, float64 *dutyCycle, bool32 *reserved);
    // int32 __CFUNC     DAQmxReadCtrTimeScalar         (TaskHandle taskHandle, float64 timeout, float64 *highTime, float64 *lowTime, bool32 *reserved);
    // int32 __CFUNC     DAQmxReadCtrTicksScalar        (TaskHandle taskHandle, float64 timeout, uInt32 *highTicks, uInt32 *lowTicks, bool32 *reserved);

    // int32 __CFUNC     DAQmxReadRaw                   (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, void *readArray, uInt32 arraySizeInBytes, int32 *sampsRead, int32 *numBytesPerSamp, bool32 *reserved);

    // int32 __CFUNC     DAQmxGetNthTaskReadChannel     (TaskHandle taskHandle, uInt32 index, char buffer[], int32 bufferSize);

    // int32 __CFUNC_C   DAQmxGetReadAttribute          (TaskHandle taskHandle, int32 attribute, void *value, ...);
    // int32 __CFUNC_C   DAQmxSetReadAttribute          (TaskHandle taskHandle, int32 attribute, ...);
    // int32 __CFUNC     DAQmxResetReadAttribute        (TaskHandle taskHandle, int32 attribute);

    /// int32 __CFUNC     DAQmxConfigureLogging          (TaskHandle taskHandle, const char filePath[], int32 loggingMode, const char groupName[], int32 operation);


    // int32 __CFUNC     DAQmxStartNewFile              (TaskHandle taskHandle, const char filePath[]);
}