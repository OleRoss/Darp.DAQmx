using System;
using System.Runtime.InteropServices;
using Darp.DAQmx.Channel.AnalogInput;
using Darp.DAQmx.Channel.AnalogOutput;
using Darp.DAQmx.Channel.CounterOutput;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace Darp.DAQmx.NationalInstruments.Functions;

internal partial class Interop
{
    private static IntPtr _libHandle = IntPtr.Zero;
#if _WINDOWS
    private const string Lib = "C:/Windows/System32/nicaiu.dll";
#else
    private const string Lib = "/usr/lib/x86_64-linux-gnu/libnidaqmx.so";
#endif

    /*static Interop()
    {
        NativeLibrary.SetDllImportResolver(typeof(Interop).Assembly, ImportResolver);
    }

    private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName != "DAQmx" || _libHandle != IntPtr.Zero)
            return _libHandle;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            NativeLibrary.TryLoad("C:/Windows/System32/nicaiu.dll", assembly, new DllImportSearchPath?(), out _libHandle);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                 && !NativeLibrary.TryLoad("/usr/lib/x86_64-linux-gnu/libnidaqmx.so", assembly, new DllImportSearchPath?(), out _libHandle))
            NativeLibrary.TryLoad("/usr/local/natinst/lib/libnidaqmx.so", assembly, new DllImportSearchPath?(), out _libHandle);
        return _libHandle;
    }*/

    /// int32 __CFUNC DAQmxGetPhysicalChanAITermCfgs(const char physicalChannel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetPhysicalChanAITermCfgs(string physicalChannel, out int data);
//********** Channel **********

//*** Set/Get functions for DAQmx_AI_Max ***
    /// int32 __CFUNC DAQmxGetAIMax(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIMax(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIMax(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIMax(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIMax(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIMax(IntPtr taskHandle, string channel);

//*** Set/Get functions for DAQmx_AI_Min ***
    /// int32 __CFUNC DAQmxGetAIMin(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIMin(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIMin(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIMin(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIMin(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIMin(IntPtr taskHandle, string channel);

//*** Set/Get functions for DAQmx_AI_CustomScaleName ***
    /// int32 __CFUNC DAQmxGetAICustomScaleName(TaskHandle taskHandle, const char channel[], char *data, uInt32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAICustomScaleName(IntPtr taskHandle, string channel, in char[] data, uint bufferSize);
    /// int32 __CFUNC DAQmxSetAICustomScaleName(TaskHandle taskHandle, const char channel[], const char *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAICustomScaleName(IntPtr taskHandle, string channel, char[] data);
    /// int32 __CFUNC DAQmxResetAICustomScaleName(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAICustomScaleName(IntPtr taskHandle, string channel);

//*** Set/Get functions for DAQmx_AI_MeasType ***
// Uses value set AIMeasurementType
    /// int32 __CFUNC DAQmxGetAIMeasType(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIMeasType(IntPtr taskHandle, string channel, out int data);

//*** Set/Get functions for DAQmx_AI_Voltage_Units ***
// Uses value set VoltageUnits1
    /// int32 __CFUNC DAQmxGetAIVoltageUnits(TaskHandle taskHandle, const char channel[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIVoltageUnits(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetAIVoltageUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIVoltageUnits(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetAIVoltageUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIVoltageUnits(IntPtr taskHandle, string channel);

/******************************************************/
/***                 Read Data                      ***/
/******************************************************/


/// int32 __CFUNC     DAQmxReadAnalogF64             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, float64 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadAnalogF64(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in double readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadAnalogScalarF64       (TaskHandle taskHandle, float64 timeout, float64 *value, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadAnalogScalarF64(IntPtr taskHandle, double timeout, out double value, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadBinaryI16             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, int16 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadBinaryI16(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in short readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadBinaryU16             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt16 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadBinaryU16(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in ushort readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadBinaryI32             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, int32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadBinaryI32(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in int readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadBinaryU32             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadBinaryU32(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in uint readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadDigitalU8             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt8 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadDigitalU8(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in byte readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadDigitalU16            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt16 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadDigitalU16(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in ushort readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadDigitalU32            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadDigitalU32(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in uint readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadDigitalScalarU32      (TaskHandle taskHandle, float64 timeout, uInt32 *value, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadDigitalScalarU32(IntPtr taskHandle, double timeout, out uint value, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadDigitalLines          (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt8 readArray[], uInt32 arraySizeInBytes, int32 *sampsPerChanRead, int32 *numBytesPerSamp, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadDigitalLines(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in byte readArray, uint arraySizeInBytes, out int sampsPerChanRead, out int numBytesPerSamp, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCounterF64            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, float64 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCounterF64(IntPtr taskHandle, int numSampsPerChan, double timeout, in double readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCounterU32            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCounterU32(IntPtr taskHandle, int numSampsPerChan, double timeout, in uint readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCounterF64Ex          (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, float64 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCounterF64Ex(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in double readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCounterU32Ex          (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCounterU32Ex(IntPtr taskHandle, int numSampsPerChan, double timeout, int fillMode, in uint readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCounterScalarF64      (TaskHandle taskHandle, float64 timeout, float64 *value, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCounterScalarF64(IntPtr taskHandle, double timeout, out double value, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCounterScalarU32      (TaskHandle taskHandle, float64 timeout, uInt32 *value, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCounterScalarU32(IntPtr taskHandle, double timeout, out uint value, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCtrFreq               (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 interleaved, float64 readArrayFrequency[], float64 readArrayDutyCycle[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCtrFreq(IntPtr taskHandle, int numSampsPerChan, double timeout, int interleaved, in double readArrayFrequency, in double readArrayDutyCycle, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCtrTime               (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 interleaved, float64 readArrayHighTime[], float64 readArrayLowTime[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCtrTime(IntPtr taskHandle, int numSampsPerChan, double timeout, int interleaved, in double readArrayHighTime, in double readArrayLowTime, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCtrTicks              (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 interleaved, uInt32 readArrayHighTicks[], uInt32 readArrayLowTicks[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCtrTicks(IntPtr taskHandle, int numSampsPerChan, double timeout, int interleaved, in uint readArrayHighTicks, in uint readArrayLowTicks, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCtrFreqScalar         (TaskHandle taskHandle, float64 timeout, float64 *frequency, float64 *dutyCycle, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCtrFreqScalar(IntPtr taskHandle, double timeout, out double frequency, out double dutyCycle, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCtrTimeScalar         (TaskHandle taskHandle, float64 timeout, float64 *highTime, float64 *lowTime, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCtrTimeScalar(IntPtr taskHandle, double timeout, out double highTime, out double lowTime, IntPtr reserved);
/// int32 __CFUNC     DAQmxReadCtrTicksScalar        (TaskHandle taskHandle, float64 timeout, uInt32 *highTicks, uInt32 *lowTicks, bool32 *reserved);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCtrTicksScalar(IntPtr taskHandle, double timeout, out uint highTicks, out uint lowTicks, IntPtr reserved);
// /// int32 __CFUNC     DAQmxReadRaw                   (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, void *readArray, uInt32 arraySizeInBytes, int32 *sampsRead, int32 *numBytesPerSamp, bool32 *reserved);
// [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int DAQmxReadRaw(IntPtr taskHandle, int numSampsPerChan, double timeout, void *readArray, uint arraySizeInBytes, out int sampsRead, out int numBytesPerSamp, IntPtr reserved);
// /// int32 __CFUNC     DAQmxGetNthTaskReadChannel     (TaskHandle taskHandle, uInt32 index, char buffer[], int32 bufferSize);
// [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int DAQmxGetNthTaskReadChannel(IntPtr taskHandle, uint index, in char[] buffer, int bufferSize);
// 
// /// int32 __CFUNC_C   DAQmxGetReadAttribute          (TaskHandle taskHandle, int32 attribute, void *value, ...);
// [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int DAQmxGetReadAttribute(IntPtr taskHandle, int attribute, void *value, ...);
// /// int32 __CFUNC_C   DAQmxSetReadAttribute          (TaskHandle taskHandle, int32 attribute, ...);
// [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int DAQmxSetReadAttribute(IntPtr taskHandle, int attribute, ...);
// /// int32 __CFUNC     DAQmxResetReadAttribute        (TaskHandle taskHandle, int32 attribute);
// [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int DAQmxResetReadAttribute(IntPtr taskHandle, int attribute);

/// int32 __CFUNC     DAQmxConfigureLogging          (TaskHandle taskHandle, const char filePath[], int32 loggingMode, const char groupName[], int32 operation);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxConfigureLogging(IntPtr taskHandle, string filePath, int loggingMode, string groupName, int operation);
/// int32 __CFUNC     DAQmxStartNewFile              (TaskHandle taskHandle, const char filePath[]);
[DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxStartNewFile(IntPtr taskHandle, string filePath);


//********** System **********
    ///*** Set/Get functions for DAQmx_Sys_GlobalChans ***
    ///int32 __CFUNC DAQmxGetSysGlobalChans(char *data, uInt32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetSysGlobalChans(ref char data, uint bufferSize);
    ///*** Set/Get functions for DAQmx_Sys_Scales ***
    /// int32 __CFUNC DAQmxGetSysScales(char *data, uInt32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetSysScales(ref char data, uint bufferSize);
    //*** Set/Get functions for DAQmx_Sys_Tasks ***
    /// int32 __CFUNC DAQmxGetSysTasks(char *data, uInt32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetSysTasks(ref char data, uint bufferSize);
    ///*** Set/Get functions for DAQmx_Sys_DevNames ***
    /// int32 __CFUNC DAQmxGetSysDevNames(char *data, uInt32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetSysDevNames(in byte data, uint bufferSize);
    ///*** Set/Get functions for DAQmx_Sys_NIDAQMajorVersion ***
    /// int32 __CFUNC DAQmxGetSysNIDAQMajorVersion(uInt32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetSysNIDAQMajorVersion(out uint data);
    ///*** Set/Get functions for DAQmx_Sys_NIDAQMinorVersion ***
    /// int32 __CFUNC DAQmxGetSysNIDAQMinorVersion(uInt32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetSysNIDAQMinorVersion(out uint data);
    ///*** Set/Get functions for DAQmx_Sys_NIDAQUpdateVersion ***
    /// int32 __CFUNC DAQmxGetSysNIDAQUpdateVersion(uInt32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetSysNIDAQUpdateVersion(out uint data);


    //********** Device **********
    //*** Set/Get functions for DAQmx_Dev_IsSimulated ***
    /// int32 __CFUNC DAQmxGetDevIsSimulated(const char device[], bool32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevIsSimulated(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_ProductCategory ***
    /// Uses value set ProductCategory
    /// int32 __CFUNC DAQmxGetDevProductCategory(const char device[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevProductCategory(string device, out ProductCategory data);
    /// *** Set/Get functions for DAQmx_Dev_ProductType ***
    /// int32 __CFUNC DAQmxGetDevProductType(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevProductType(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_ProductNum ***
    /// int32 __CFUNC DAQmxGetDevProductNum(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevProductNum(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_SerialNum ***
    /// int32 __CFUNC DAQmxGetDevSerialNum(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevSerialNum(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_Accessory_ProductTypes ***
    /// int32 __CFUNC DAQmxGetDevAccessoryProductTypes(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAccessoryProductTypes(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_Accessory_ProductNums ***
    /// int32 __CFUNC DAQmxGetDevAccessoryProductNums(const char device[], uInt32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAccessoryProductNums(string device, uint[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_Accessory_SerialNums ***
    /// int32 __CFUNC DAQmxGetDevAccessorySerialNums(const char device[], uInt32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAccessorySerialNums(string device, uint[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Carrier_SerialNum ***
    /// int32 __CFUNC DAQmxGetCarrierSerialNum(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetCarrierSerialNum(string device, out uint data);
    /// *** Set/Get functions for DAQmx_FieldDAQ_DevName ***
    /// int32 __CFUNC DAQmxGetFieldDAQDevName(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetFieldDAQDevName(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_FieldDAQ_BankDevNames ***
    /// int32 __CFUNC DAQmxGetFieldDAQBankDevNames(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetFieldDAQBankDevNames(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_Chassis_ModuleDevNames ***
    /// int32 __CFUNC DAQmxGetDevChassisModuleDevNames(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevChassisModuleDevNames(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_AnlgTrigSupported ***
    /// int32 __CFUNC DAQmxGetDevAnlgTrigSupported(const char device[], bool32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAnlgTrigSupported(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_DigTrigSupported ***
    /// int32 __CFUNC DAQmxGetDevDigTrigSupported(const char device[], bool32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDigTrigSupported(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_TimeTrigSupported ***
    /// int32 __CFUNC DAQmxGetDevTimeTrigSupported(const char device[], bool32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevTimeTrigSupported(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_AI_PhysicalChans ***
    /// int32 __CFUNC DAQmxGetDevAIPhysicalChans(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIPhysicalChans(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_AI_SupportedMeasTypes ***
    /// Uses value set AIMeasurementType
    /// int32 __CFUNC DAQmxGetDevAISupportedMeasTypes(const char device[], int32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAISupportedMeasTypes(string device, in AIMeasurementType data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_MaxSingleChanRate ***
    /// int32 __CFUNC DAQmxGetDevAIMaxSingleChanRate(const char device[], float64 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIMaxSingleChanRate(string device, out double data);
    /// *** Set/Get functions for DAQmx_Dev_AI_MaxMultiChanRate ***
    /// int32 __CFUNC DAQmxGetDevAIMaxMultiChanRate(const char device[], float64 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIMaxMultiChanRate(string device, out double data);
    /// *** Set/Get functions for DAQmx_Dev_AI_MinRate ***
    /// int32 __CFUNC DAQmxGetDevAIMinRate(const char device[], float64 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIMinRate(string device, out double data);
    /// *** Set/Get functions for DAQmx_Dev_AI_SimultaneousSamplingSupported ***
    /// int32 __CFUNC DAQmxGetDevAISimultaneousSamplingSupported(const char device[], bool32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAISimultaneousSamplingSupported(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_AI_NumSampTimingEngines ***
    /// int32 __CFUNC DAQmxGetDevAINumSampTimingEngines(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAINumSampTimingEngines(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_AI_SampModes ***
    /// // Uses value set AcquisitionType
    /// int32 __CFUNC DAQmxGetDevAISampModes(const char device[], int32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAISampModes(string device, int[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_NumSyncPulseSrcs ***
    /// int32 __CFUNC DAQmxGetDevAINumSyncPulseSrcs(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAINumSyncPulseSrcs(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_AI_TrigUsage ***
    /// // Uses bits from enum TriggerUsageTypeBits
    /// int32 __CFUNC DAQmxGetDevAITrigUsage(const char device[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAITrigUsage(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_AI_VoltageRngs ***
    /// int32 __CFUNC DAQmxGetDevAIVoltageRngs(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIVoltageRngs(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_VoltageIntExcitDiscreteVals ***
    /// int32 __CFUNC DAQmxGetDevAIVoltageIntExcitDiscreteVals(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIVoltageIntExcitDiscreteVals(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_VoltageIntExcitRangeVals ***
    /// int32 __CFUNC DAQmxGetDevAIVoltageIntExcitRangeVals(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIVoltageIntExcitRangeVals(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_ChargeRngs ***
    /// int32 __CFUNC DAQmxGetDevAIChargeRngs(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIChargeRngs(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_CurrentRngs ***
    /// int32 __CFUNC DAQmxGetDevAICurrentRngs(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAICurrentRngs(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_CurrentIntExcitDiscreteVals ***
    /// int32 __CFUNC DAQmxGetDevAICurrentIntExcitDiscreteVals(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAICurrentIntExcitDiscreteVals(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_BridgeRngs ***
    /// int32 __CFUNC DAQmxGetDevAIBridgeRngs(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIBridgeRngs(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_ResistanceRngs ***
    /// int32 __CFUNC DAQmxGetDevAIResistanceRngs(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIResistanceRngs(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_FreqRngs ***
    /// int32 __CFUNC DAQmxGetDevAIFreqRngs(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIFreqRngs(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_Gains ***
    /// int32 __CFUNC DAQmxGetDevAIGains(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIGains(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_Couplings ***
    /// // Uses bits from enum CouplingTypeBits
    /// int32 __CFUNC DAQmxGetDevAICouplings(const char device[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAICouplings(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_AI_LowpassCutoffFreqDiscreteVals ***
    /// int32 __CFUNC DAQmxGetDevAILowpassCutoffFreqDiscreteVals(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAILowpassCutoffFreqDiscreteVals(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_LowpassCutoffFreqRangeVals ***
    /// int32 __CFUNC DAQmxGetDevAILowpassCutoffFreqRangeVals(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAILowpassCutoffFreqRangeVals(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_AI_DigFltr_Types ***
    /// // Uses value set FilterType2
    /// int32 __CFUNC DAQmxGetAIDigFltrTypes(const char device[], int32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIDigFltrTypes(string device, int[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_DigFltr_LowpassCutoffFreqDiscreteVals ***
    /// int32 __CFUNC DAQmxGetDevAIDigFltrLowpassCutoffFreqDiscreteVals(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIDigFltrLowpassCutoffFreqDiscreteVals(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AI_DigFltr_LowpassCutoffFreqRangeVals ***
    /// int32 __CFUNC DAQmxGetDevAIDigFltrLowpassCutoffFreqRangeVals(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAIDigFltrLowpassCutoffFreqRangeVals(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AO_PhysicalChans ***
    /// int32 __CFUNC DAQmxGetDevAOPhysicalChans(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOPhysicalChans(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_AO_SupportedOutputTypes ***
    /// // Uses value set AOOutputChannelType
    /// int32 __CFUNC DAQmxGetDevAOSupportedOutputTypes(const char device[], int32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOSupportedOutputTypes(string device, in AOOutputType data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AO_MaxRate ***
    /// int32 __CFUNC DAQmxGetDevAOMaxRate(const char device[], float64 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOMaxRate(string device, out double data);
    /// *** Set/Get functions for DAQmx_Dev_AO_MinRate ***
    /// int32 __CFUNC DAQmxGetDevAOMinRate(const char device[], float64 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOMinRate(string device, out double data);
    /// *** Set/Get functions for DAQmx_Dev_AO_SampClkSupported ***
    /// int32 __CFUNC DAQmxGetDevAOSampClkSupported(const char device[], bool32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOSampClkSupported(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_AO_NumSampTimingEngines ***
    /// int32 __CFUNC DAQmxGetDevAONumSampTimingEngines(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOSampClkSupported(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_AO_SampModes ***
    /// // Uses value set AcquisitionType
    /// int32 __CFUNC DAQmxGetDevAOSampModes(const char device[], int32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOSampModes(string device, int[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AO_NumSyncPulseSrcs ***
    /// int32 __CFUNC DAQmxGetDevAONumSyncPulseSrcs(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAONumSyncPulseSrcs(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_AO_TrigUsage ***
    /// // Uses bits from enum TriggerUsageTypeBits
    /// int32 __CFUNC DAQmxGetDevAOTrigUsage(const char device[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOTrigUsage(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_AO_VoltageRngs ***
    /// int32 __CFUNC DAQmxGetDevAOVoltageRngs(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOVoltageRngs(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AO_CurrentRngs ***
    /// int32 __CFUNC DAQmxGetDevAOCurrentRngs(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOCurrentRngs(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_AO_Gains ***
    /// int32 __CFUNC DAQmxGetDevAOGains(const char device[], float64 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevAOGains(string device, double[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_DI_Lines ***
    /// int32 __CFUNC DAQmxGetDevDILines(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDILines(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_DI_Ports ***
    /// int32 __CFUNC DAQmxGetDevDIPorts(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDIPorts(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_DI_MaxRate ***
    /// int32 __CFUNC DAQmxGetDevDIMaxRate(const char device[], float64 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDIMaxRate(string device, out double data);
    /// *** Set/Get functions for DAQmx_Dev_DI_NumSampTimingEngines ***
    /// int32 __CFUNC DAQmxGetDevDINumSampTimingEngines(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDINumSampTimingEngines(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_DI_TrigUsage ***
    /// // Uses bits from enum TriggerUsageTypeBits
    /// int32 __CFUNC DAQmxGetDevDITrigUsage(const char device[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDITrigUsage(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_DO_Lines ***
    /// int32 __CFUNC DAQmxGetDevDOLines(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDOLines(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_DO_Ports ***
    /// int32 __CFUNC DAQmxGetDevDOPorts(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDOPorts(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_DO_MaxRate ***
    /// int32 __CFUNC DAQmxGetDevDOMaxRate(const char device[], float64 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDOMaxRate(string device, out double data);
    /// *** Set/Get functions for DAQmx_Dev_DO_NumSampTimingEngines ***
    /// int32 __CFUNC DAQmxGetDevDONumSampTimingEngines(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDONumSampTimingEngines(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_DO_TrigUsage ***
    /// // Uses bits from enum TriggerUsageTypeBits
    /// int32 __CFUNC DAQmxGetDevDOTrigUsage(const char device[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevDOTrigUsage(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_CI_PhysicalChans ***
    /// int32 __CFUNC DAQmxGetDevCIPhysicalChans(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCIPhysicalChans(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_CI_SupportedMeasTypes ***
    /// // Uses value set CIMeasurementType
    /// int32 __CFUNC DAQmxGetDevCISupportedMeasTypes(const char device[], int32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCISupportedMeasTypes(string device, int[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_CI_TrigUsage ***
    /// // Uses bits from enum TriggerUsageTypeBits
    /// int32 __CFUNC DAQmxGetDevCITrigUsage(const char device[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCITrigUsage(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_CI_SampClkSupported ***
    /// int32 __CFUNC DAQmxGetDevCISampClkSupported(const char device[], bool32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCISampClkSupported(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_CI_SampModes ***
    /// // Uses value set AcquisitionType
    /// int32 __CFUNC DAQmxGetDevCISampModes(const char device[], int32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCISampModes(string device, int[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_CI_MaxSize ***
    /// int32 __CFUNC DAQmxGetDevCIMaxSize(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCIMaxSize(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_CI_MaxTimebase ***
    /// int32 __CFUNC DAQmxGetDevCIMaxTimebase(const char device[], float64 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCIMaxTimebase(string device, out double data);
    /// *** Set/Get functions for DAQmx_Dev_CO_PhysicalChans ***
    /// int32 __CFUNC DAQmxGetDevCOPhysicalChans(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCOPhysicalChans(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_CO_SupportedOutputTypes ***
    /// // Uses value set COOutputType
    /// int32 __CFUNC DAQmxGetDevCOSupportedOutputTypes(const char device[], int32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCOSupportedOutputTypes(string device, in COOutputType data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_CO_SampClkSupported ***
    /// int32 __CFUNC DAQmxGetDevCOSampClkSupported(const char device[], bool32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCOSampClkSupported(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_CO_SampModes ***
    /// // Uses value set AcquisitionType
    /// int32 __CFUNC DAQmxGetDevCOSampModes(const char device[], int32 *data, uInt32 arraySizeInElements)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int D(string device, int[] data, uint arraySizeInElements);
    /// *** Set/Get functions for DAQmx_Dev_CO_TrigUsage ***
    /// // Uses bits from enum TriggerUsageTypeBits
    /// int32 __CFUNC DAQmxGetDevCOTrigUsage(const char device[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCOTrigUsage(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_CO_MaxSize ***
    /// int32 __CFUNC DAQmxGetDevCOMaxSize(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCOMaxSize(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_CO_MaxTimebase ***
    /// int32 __CFUNC DAQmxGetDevCOMaxTimebase(const char device[], float64 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCOMaxTimebase(string device, out double data);
    /// *** Set/Get functions for DAQmx_Dev_TEDS_HWTEDSSupported ***
    /// int32 __CFUNC DAQmxGetDevTEDSHWTEDSSupported(const char device[], bool32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevTEDSHWTEDSSupported(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_NumDMAChans ***
    /// int32 __CFUNC DAQmxGetDevNumDMAChans(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevNumDMAChans(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_BusType ***
    /// // Uses value set BusType
    /// int32 __CFUNC DAQmxGetDevBusType(const char device[], int32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevBusType(string device, out int data);
    /// *** Set/Get functions for DAQmx_Dev_PCI_BusNum ***
    /// int32 __CFUNC DAQmxGetDevPCIBusNum(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevPCIBusNum(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_PCI_DevNum ***
    /// int32 __CFUNC DAQmxGetDevPCIDevNum(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevPCIDevNum(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_PXI_ChassisNum ***
    /// int32 __CFUNC DAQmxGetDevPXIChassisNum(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevPXIChassisNum(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_PXI_SlotNum ***
    /// int32 __CFUNC DAQmxGetDevPXISlotNum(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevPXISlotNum(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_CompactDAQ_ChassisDevName ***
    /// int32 __CFUNC DAQmxGetDevCompactDAQChassisDevName(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCompactDAQChassisDevName(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_CompactDAQ_SlotNum ***
    /// int32 __CFUNC DAQmxGetDevCompactDAQSlotNum(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCompactDAQSlotNum(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_CompactRIO_ChassisDevName ***
    /// int32 __CFUNC DAQmxGetDevCompactRIOChassisDevName(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCompactRIOChassisDevName(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_CompactRIO_SlotNum ***
    /// int32 __CFUNC DAQmxGetDevCompactRIOSlotNum(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevCompactRIOSlotNum(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_TCPIP_Hostname ***
    /// int32 __CFUNC DAQmxGetDevTCPIPHostname(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevTCPIPHostname(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_TCPIP_EthernetIP ***
    /// int32 __CFUNC DAQmxGetDevTCPIPEthernetIP(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevTCPIPEthernetIP(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_TCPIP_WirelessIP ***
    /// int32 __CFUNC DAQmxGetDevTCPIPWirelessIP(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevTCPIPWirelessIP(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_Terminals ***
    /// int32 __CFUNC DAQmxGetDevTerminals(const char device[], char *data, uInt32 bufferSize)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevTerminals(string device, in byte data, uint bufferSize);
    /// *** Set/Get functions for DAQmx_Dev_NumTimeTrigs ***
    /// int32 __CFUNC DAQmxGetDevNumTimeTrigs(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevNumTimeTrigs(string device, out uint data);
    /// *** Set/Get functions for DAQmx_Dev_NumTimestampEngines ***
    /// int32 __CFUNC DAQmxGetDevNumTimestampEngines(const char device[], uInt32 *data)
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDevNumTimestampEngines(string device, out uint data);
}