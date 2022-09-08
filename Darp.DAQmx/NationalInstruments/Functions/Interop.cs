using System;
using System.Runtime.InteropServices;
using Darp.DAQmx.Channel.AnalogInput;
using Darp.DAQmx.Channel.AnalogOutput;
using Darp.DAQmx.Channel.CounterInput;
using Darp.DAQmx.Channel.CounterOutput;
using Darp.DAQmx.Channel.DigitalInput;
using Darp.DAQmx.Event;
using Darp.DAQmx.Task;
using Darp.DAQmx.Timing;

// ReSharper disable InconsistentNaming

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace Darp.DAQmx.NationalInstruments.Functions;

internal static partial class Interop
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


/******************************************************/
/***                 Read Data                      ***/
/******************************************************/


    /// int32 __CFUNC     DAQmxReadAnalogF64             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, float64 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadAnalogF64(IntPtr taskHandle, int numSampsPerChan, double timeout, AIFillMode fillMode, in double readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadAnalogScalarF64       (TaskHandle taskHandle, float64 timeout, float64 *value, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadAnalogScalarF64(IntPtr taskHandle, double timeout, out double value, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadBinaryI16             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, int16 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadBinaryI16(IntPtr taskHandle, int numSampsPerChan, double timeout, AIFillMode fillMode, in short readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadBinaryU16             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt16 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadBinaryU16(IntPtr taskHandle, int numSampsPerChan, double timeout, AIFillMode fillMode, in ushort readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadBinaryI32             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, int32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadBinaryI32(IntPtr taskHandle, int numSampsPerChan, double timeout, AIFillMode fillMode, in int readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadBinaryU32             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadBinaryU32(IntPtr taskHandle, int numSampsPerChan, double timeout, AIFillMode fillMode, in uint readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadDigitalU8             (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt8 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadDigitalU8(IntPtr taskHandle, int numSampsPerChan, double timeout, DIFillMode fillMode, in byte readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadDigitalU16            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt16 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadDigitalU16(IntPtr taskHandle, int numSampsPerChan, double timeout, DIFillMode fillMode, in ushort readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadDigitalU32            (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadDigitalU32(IntPtr taskHandle, int numSampsPerChan, double timeout, DIFillMode fillMode, in uint readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
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
    internal static extern int DAQmxReadCounterF64Ex(IntPtr taskHandle, int numSampsPerChan, double timeout, CIFillMode fillMode, in double readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
    /// int32 __CFUNC     DAQmxReadCounterU32Ex          (TaskHandle taskHandle, int32 numSampsPerChan, float64 timeout, bool32 fillMode, uInt32 readArray[], uInt32 arraySizeInSamps, int32 *sampsPerChanRead, bool32 *reserved);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxReadCounterU32Ex(IntPtr taskHandle, int numSampsPerChan, double timeout, CIFillMode fillMode, in uint readArray, uint arraySizeInSamps, out int sampsPerChanRead, IntPtr reserved);
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
    internal static extern int DAQmxGetDevCOSampModes(string device, int[] data, uint arraySizeInElements);
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

    /******************************************************/
/***         Task Configuration/Control             ***/
/******************************************************/


    /// int32 __CFUNC     DAQmxLoadTask                  (const char taskName[], TaskHandle *taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxLoadTask(string taskName, out IntPtr taskHandle);
    /// int32 __CFUNC     DAQmxCreateTask                (const char taskName[], TaskHandle *taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxCreateTask(string taskName, out IntPtr taskHandle);
    /// Channel Names must be valid channels already available in MAX. They are not created.
    /// int32 __CFUNC     DAQmxAddGlobalChansToTask      (TaskHandle taskHandle, const char channelNames[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxAddGlobalChansToTask(IntPtr taskHandle, string channelNames);

    /// int32 __CFUNC     DAQmxStartTask                 (TaskHandle taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxStartTask(IntPtr taskHandle);
    /// int32 __CFUNC     DAQmxStopTask                  (TaskHandle taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxStopTask(IntPtr taskHandle);

    /// int32 __CFUNC     DAQmxClearTask                 (TaskHandle taskHandle);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxClearTask(IntPtr taskHandle);

    /// int32 __CFUNC     DAQmxWaitUntilTaskDone         (TaskHandle taskHandle, float64 timeToWait);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxWaitUntilTaskDone(IntPtr taskHandle, double timeToWait);
    // /// int32 __CFUNC     DAQmxWaitForValidTimestamp     (TaskHandle taskHandle, int32 timestampEvent, float64 timeout, CVIAbsoluteTime* timestamp);
    // [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    // internal static extern int DAQmxWaitForValidTimestamp(IntPtr taskHandle, int timestampEvent, double timeout, CVIAbsoluteTime* timestamp);
    /// int32 __CFUNC     DAQmxIsTaskDone                (TaskHandle taskHandle, bool32 *isTaskDone);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxIsTaskDone(IntPtr taskHandle, out int isTaskDone);

    /// int32 __CFUNC     DAQmxTaskControl               (TaskHandle taskHandle, int32 action);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxTaskControl(IntPtr taskHandle, TaskAction action);

    /// int32 __CFUNC     DAQmxGetNthTaskChannel         (TaskHandle taskHandle, uInt32 index, char buffer[], int32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetNthTaskChannel(IntPtr taskHandle, uint index, in byte buffer, int bufferSize);

    /// int32 __CFUNC     DAQmxGetNthTaskDevice(TaskHandle taskHandle, uInt32 index, char buffer[], int32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetNthTaskDevice(IntPtr taskHandle, uint index, in byte buffer, int bufferSize);

    // /// int32 __CFUNC_C   DAQmxGetTaskAttribute(TaskHandle taskHandle, int32 attribute, void *value, ...);
    // [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    // internal static extern int DAQmxGetTaskAttribute(IntPtr taskHandle, int attribute, void *value, ...);

    /// typedef int32 (CVICALLBACK *DAQmxEveryNSamplesEventCallbackPtr)(TaskHandle taskHandle, int32 everyNsamplesEventType, uInt32 nSamples, void *callbackData);
    internal delegate int DAQmxEveryNSamplesEventCallbackPtr(IntPtr taskHandle, EveryNSamplesEventType everyNsamplesEventType, uint nSamples, IntPtr callbackData);
    /// typedef int32 (CVICALLBACK *DAQmxDoneEventCallbackPtr)(TaskHandle taskHandle, int32 status, void *callbackData);
    internal delegate int DAQmxDoneEventCallbackPtr(IntPtr taskHandle, int status, IntPtr callbackData);
    /// typedef int32 (CVICALLBACK *DAQmxSignalEventCallbackPtr)(TaskHandle taskHandle, int32 signalID, void *callbackData);
    internal delegate int DAQmxSignalEventCallbackPtr(IntPtr taskHandle, int signalID, IntPtr callbackData);

    /// int32 __CFUNC     DAQmxRegisterEveryNSamplesEvent(TaskHandle task, int32 everyNsamplesEventType, uInt32 nSamples, uInt32 options, DAQmxEveryNSamplesEventCallbackPtr callbackFunction, void *callbackData);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxRegisterEveryNSamplesEvent(IntPtr task, EveryNSamplesEventType everyNsamplesEventType, uint nSamples, uint options, DAQmxEveryNSamplesEventCallbackPtr callbackFunction, IntPtr callbackData);
    /// int32 __CFUNC     DAQmxRegisterDoneEvent         (TaskHandle task, uInt32 options, DAQmxDoneEventCallbackPtr callbackFunction, void *callbackData);i
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxRegisterDoneEvent(IntPtr task, uint options, DAQmxDoneEventCallbackPtr callbackFunction, IntPtr callbackData);
    /// int32 __CFUNC     DAQmxRegisterSignalEvent       (TaskHandle task, int32 signalID, uInt32 options, DAQmxSignalEventCallbackPtr callbackFunction, void *callbackData);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxRegisterSignalEvent(IntPtr task, int signalID, uint options, DAQmxSignalEventCallbackPtr callbackFunction, IntPtr callbackData);
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
    internal static extern int DAQmxGetAICustomScaleName(IntPtr taskHandle, string channel, in byte data, uint bufferSize);
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
    internal static extern int DAQmxGetAIVoltageUnits(IntPtr taskHandle, string channel, out AIVoltageUnits data);
    /// int32 __CFUNC DAQmxSetAIVoltageUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIVoltageUnits(IntPtr taskHandle, string channel, AIVoltageUnits data);
    /// int32 __CFUNC DAQmxResetAIVoltageUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIVoltageUnits(IntPtr taskHandle, string channel);
///*** Set/Get functions for DAQmx_AI_Voltage_dBRef ***
   /// int32 __CFUNC DAQmxGetAIVoltagedBRef(TaskHandle taskHandle, const char channel[], float64 *data);
   [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
   internal static extern int DAQmxGetAIVoltagedBRef(IntPtr taskHandle, string channel, out double data);
   /// int32 __CFUNC DAQmxSetAIVoltagedBRef(TaskHandle taskHandle, const char channel[], float64 data);
   [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
   internal static extern int DAQmxSetAIVoltagedBRef(IntPtr taskHandle, string channel, double data);
   /// int32 __CFUNC DAQmxResetAIVoltagedBRef(TaskHandle taskHandle, const char channel[]);
   [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
   internal static extern int DAQmxResetAIVoltagedBRef(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Voltage_ACRMS_Units ***
// Uses value set VoltageUnits1
    /// int32 __CFUNC DAQmxGetAIVoltageACRMSUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIVoltageACRMSUnits(IntPtr taskHandle, string channel, out AIVoltageUnits data);
    /// int32 __CFUNC DAQmxSetAIVoltageACRMSUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIVoltageACRMSUnits(IntPtr taskHandle, string channel, AIVoltageUnits data);
    /// int32 __CFUNC DAQmxResetAIVoltageACRMSUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIVoltageACRMSUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Temp_Units ***
// Uses value set TemperatureUnits1
    /// int32 __CFUNC DAQmxGetAITempUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAITempUnits(IntPtr taskHandle, string channel, out AITempUnits data);
    /// int32 __CFUNC DAQmxSetAITempUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAITempUnits(IntPtr taskHandle, string channel, AITempUnits data);
    /// int32 __CFUNC DAQmxResetAITempUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAITempUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Thrmcpl_Type ***
// Uses value set ThermocoupleType1
    /// int32 __CFUNC DAQmxGetAIThrmcplType(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIThrmcplType(IntPtr taskHandle, string channel, out AIThermoCoupleType data);
    /// int32 __CFUNC DAQmxSetAIThrmcplType(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIThrmcplType(IntPtr taskHandle, string channel, AIThermoCoupleType data);
    /// int32 __CFUNC DAQmxResetAIThrmcplType(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIThrmcplType(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Thrmcpl_ScaleType ***
// Uses value set ScaleType2
    /// int32 __CFUNC DAQmxGetAIThrmcplScaleType(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIThrmcplScaleType(IntPtr taskHandle, string channel, out AIThermoCoupleScaleType data);
    /// int32 __CFUNC DAQmxSetAIThrmcplScaleType(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIThrmcplScaleType(IntPtr taskHandle, string channel, AIThermoCoupleScaleType data);
    /// int32 __CFUNC DAQmxResetAIThrmcplScaleType(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIThrmcplScaleType(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Thrmcpl_CJCSrc ***
// Uses value set CJCSource1
    /// int32 __CFUNC DAQmxGetAIThrmcplCJCSrc(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIThrmcplCJCSrc(IntPtr taskHandle, string channel, out AIThermoCoupleCJCSource data);
//*** Set/Get functions for DAQmx_AI_Thrmcpl_CJCVal ***
    /// int32 __CFUNC DAQmxGetAIThrmcplCJCVal(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIThrmcplCJCVal(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIThrmcplCJCVal(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIThrmcplCJCVal(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIThrmcplCJCVal(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIThrmcplCJCVal(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Thrmcpl_CJCChan ***
    /// int32 __CFUNC DAQmxGetAIThrmcplCJCChan(TaskHandle taskHandle, const char channel[], char *data, uint32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIThrmcplCJCChan(IntPtr taskHandle, string channel, in byte data, uint bufferSize);
//*** Set/Get functions for DAQmx_AI_RTD_Type ***
// Uses value set RTDType1
    /// int32 __CFUNC DAQmxGetAIRTDType(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRTDType(IntPtr taskHandle, string channel, out AIResistanceTemperatureDetectorType data);
    /// int32 __CFUNC DAQmxSetAIRTDType(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRTDType(IntPtr taskHandle, string channel, AIResistanceTemperatureDetectorType data);
    /// int32 __CFUNC DAQmxResetAIRTDType(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRTDType(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_RTD_R0 ***
    /// int32 __CFUNC DAQmxGetAIRTDR0(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRTDR0(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIRTDR0(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRTDR0(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIRTDR0(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRTDR0(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_RTD_A ***
    /// int32 __CFUNC DAQmxGetAIRTDA(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRTDA(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIRTDA(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRTDA(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIRTDA(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRTDA(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_RTD_B ***
    /// int32 __CFUNC DAQmxGetAIRTDB(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRTDB(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIRTDB(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRTDB(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIRTDB(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRTDB(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_RTD_C ***
    /// int32 __CFUNC DAQmxGetAIRTDC(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRTDC(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIRTDC(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRTDC(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIRTDC(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRTDC(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Thrmstr_A ***
    /// int32 __CFUNC DAQmxGetAIThrmstrA(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIThrmstrA(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIThrmstrA(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIThrmstrA(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIThrmstrA(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIThrmstrA(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Thrmstr_B ***
    /// int32 __CFUNC DAQmxGetAIThrmstrB(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIThrmstrB(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIThrmstrB(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIThrmstrB(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIThrmstrB(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIThrmstrB(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Thrmstr_C ***
    /// int32 __CFUNC DAQmxGetAIThrmstrC(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIThrmstrC(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIThrmstrC(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIThrmstrC(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIThrmstrC(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIThrmstrC(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Thrmstr_R1 ***
    /// int32 __CFUNC DAQmxGetAIThrmstrR1(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIThrmstrR1(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIThrmstrR1(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIThrmstrR1(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIThrmstrR1(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIThrmstrR1(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_ForceReadFromChan ***
    /// int32 __CFUNC DAQmxGetAIForceReadFromChan(TaskHandle taskHandle, const char channel[], bool32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIForceReadFromChan(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetAIForceReadFromChan(TaskHandle taskHandle, const char channel[], bool32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIForceReadFromChan(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetAIForceReadFromChan(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIForceReadFromChan(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Current_Units ***
// Uses value set CurrentUnits1
    /// int32 __CFUNC DAQmxGetAICurrentUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAICurrentUnits(IntPtr taskHandle, string channel, out AICurrentUnits data);
    /// int32 __CFUNC DAQmxSetAICurrentUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAICurrentUnits(IntPtr taskHandle, string channel, AICurrentUnits data);
    /// int32 __CFUNC DAQmxResetAICurrentUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAICurrentUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Current_ACRMS_Units ***
// Uses value set CurrentUnits1
    /// int32 __CFUNC DAQmxGetAICurrentACRMSUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAICurrentACRMSUnits(IntPtr taskHandle, string channel, out AICurrentUnits data);
    /// int32 __CFUNC DAQmxSetAICurrentACRMSUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAICurrentACRMSUnits(IntPtr taskHandle, string channel, AICurrentUnits data);
    /// int32 __CFUNC DAQmxResetAICurrentACRMSUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAICurrentACRMSUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Strain_Units ***
// Uses value set StrainUnits1
    /// int32 __CFUNC DAQmxGetAIStrainUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIStrainUnits(IntPtr taskHandle, string channel, out AIStrainUnits data);
    /// int32 __CFUNC DAQmxSetAIStrainUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIStrainUnits(IntPtr taskHandle, string channel, AIStrainUnits data);
    /// int32 __CFUNC DAQmxResetAIStrainUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIStrainUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_StrainGage_ForceReadFromChan ***
    /// int32 __CFUNC DAQmxGetAIStrainGageForceReadFromChan(TaskHandle taskHandle, const char channel[], bool32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIStrainGageForceReadFromChan(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetAIStrainGageForceReadFromChan(TaskHandle taskHandle, const char channel[], bool32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIStrainGageForceReadFromChan(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetAIStrainGageForceReadFromChan(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIStrainGageForceReadFromChan(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_StrainGage_GageFactor ***
    /// int32 __CFUNC DAQmxGetAIStrainGageGageFactor(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIStrainGageGageFactor(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIStrainGageGageFactor(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIStrainGageGageFactor(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIStrainGageGageFactor(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIStrainGageGageFactor(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_StrainGage_PoissonRatio ***
    /// int32 __CFUNC DAQmxGetAIStrainGagePoissonRatio(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIStrainGagePoissonRatio(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIStrainGagePoissonRatio(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIStrainGagePoissonRatio(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIStrainGagePoissonRatio(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIStrainGagePoissonRatio(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_StrainGage_Cfg ***
// Uses value set StrainGageBridgeType1
    /// int32 __CFUNC DAQmxGetAIStrainGageCfg(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIStrainGageCfg(IntPtr taskHandle, string channel, out AIStrainGageConfiguration data);
    /// int32 __CFUNC DAQmxSetAIStrainGageCfg(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIStrainGageCfg(IntPtr taskHandle, string channel, AIStrainGageConfiguration data);
    /// int32 __CFUNC DAQmxResetAIStrainGageCfg(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIStrainGageCfg(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_RosetteStrainGage_RosetteType ***
// Uses value set StrainGageRosetteType
    /// int32 __CFUNC DAQmxGetAIRosetteStrainGageRosetteType(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRosetteStrainGageRosetteType(IntPtr taskHandle, string channel, out AIStrainGageRosetteType data);
//*** Set/Get functions for DAQmx_AI_RosetteStrainGage_Orientation ***
    /// int32 __CFUNC DAQmxGetAIRosetteStrainGageOrientation(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRosetteStrainGageOrientation(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIRosetteStrainGageOrientation(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRosetteStrainGageOrientation(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIRosetteStrainGageOrientation(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRosetteStrainGageOrientation(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_RosetteStrainGage_StrainChans ***
    /// int32 __CFUNC DAQmxGetAIRosetteStrainGageStrainChans(TaskHandle taskHandle, const char channel[], char *data, uint32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int Asd(IntPtr taskHandle, string channel, in byte data, uint bufferSize);
//*** Set/Get functions for DAQmx_AI_RosetteStrainGage_RosetteMeasType ***
// Uses value set StrainGageRosetteMeasurementType
    /// int32 __CFUNC DAQmxGetAIRosetteStrainGageRosetteMeasType(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRosetteStrainGageRosetteMeasType(IntPtr taskHandle, string channel, out AIStrainGageRosetteMeasurementType data);
    /// int32 __CFUNC DAQmxSetAIRosetteStrainGageRosetteMeasType(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRosetteStrainGageRosetteMeasType(IntPtr taskHandle, string channel, AIStrainGageRosetteMeasurementType data);
    /// int32 __CFUNC DAQmxResetAIRosetteStrainGageRosetteMeasType(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRosetteStrainGageRosetteMeasType(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Resistance_Units ***
// Uses value set ResistanceUnits1
    /// int32 __CFUNC DAQmxGetAIResistanceUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIResistanceUnits(IntPtr taskHandle, string channel, out AIResistanceUnits data);
    /// int32 __CFUNC DAQmxSetAIResistanceUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIResistanceUnits(IntPtr taskHandle, string channel, AIResistanceUnits data);
    /// int32 __CFUNC DAQmxResetAIResistanceUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIResistanceUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Freq_Units ***
// Uses value set FrequencyUnits
    /// int32 __CFUNC DAQmxGetAIFreqUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIFreqUnits(IntPtr taskHandle, string channel, out AIFrequencyUnits data);
    /// int32 __CFUNC DAQmxSetAIFreqUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIFreqUnits(IntPtr taskHandle, string channel, AIFrequencyUnits data);
    /// int32 __CFUNC DAQmxResetAIFreqUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIFreqUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Freq_ThreshVoltage ***
    /// int32 __CFUNC DAQmxGetAIFreqThreshVoltage(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIFreqThreshVoltage(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIFreqThreshVoltage(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIFreqThreshVoltage(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIFreqThreshVoltage(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIFreqThreshVoltage(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_Freq_Hyst ***
    /// int32 __CFUNC DAQmxGetAIFreqHyst(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIFreqHyst(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIFreqHyst(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIFreqHyst(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIFreqHyst(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIFreqHyst(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_LVDT_Units ***
// Uses value set LengthUnits2
    /// int32 __CFUNC DAQmxGetAILVDTUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAILVDTUnits(IntPtr taskHandle, string channel, out AILinearVariableDifferentialTransformerUnits data);
    /// int32 __CFUNC DAQmxSetAILVDTUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAILVDTUnits(IntPtr taskHandle, string channel, AILinearVariableDifferentialTransformerUnits data);
    /// int32 __CFUNC DAQmxResetAILVDTUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAILVDTUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_LVDT_Sensitivity ***
    /// int32 __CFUNC DAQmxGetAILVDTSensitivity(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAILVDTSensitivity(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAILVDTSensitivity(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAILVDTSensitivity(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAILVDTSensitivity(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAILVDTSensitivity(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_LVDT_SensitivityUnits ***
// Uses value set LVDTSensitivityUnits1
    /// int32 __CFUNC DAQmxGetAILVDTSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAILVDTSensitivityUnits(IntPtr taskHandle, string channel, out AILinearVariableDifferentialTransformerSensitivityUnits data);
    /// int32 __CFUNC DAQmxSetAILVDTSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAILVDTSensitivityUnits(IntPtr taskHandle, string channel, AILinearVariableDifferentialTransformerSensitivityUnits data);
    /// int32 __CFUNC DAQmxResetAILVDTSensitivityUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAILVDTSensitivityUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_RVDT_Units ***
// Uses value set AngleUnits1
    /// int32 __CFUNC DAQmxGetAIRVDTUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRVDTUnits(IntPtr taskHandle, string channel, out AIRotaryVariableDifferentialTransformerUnits data);
    /// int32 __CFUNC DAQmxSetAIRVDTUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRVDTUnits(IntPtr taskHandle, string channel, AIRotaryVariableDifferentialTransformerUnits data);
    /// int32 __CFUNC DAQmxResetAIRVDTUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRVDTUnits(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_RVDT_Sensitivity ***
    /// int32 __CFUNC DAQmxGetAIRVDTSensitivity(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRVDTSensitivity(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetAIRVDTSensitivity(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRVDTSensitivity(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetAIRVDTSensitivity(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRVDTSensitivity(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_RVDT_SensitivityUnits ***
// Uses value set RVDTSensitivityUnits1
    /// int32 __CFUNC DAQmxGetAIRVDTSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIRVDTSensitivityUnits(IntPtr taskHandle, string channel, out AIRotaryVariableDifferentialTransformerSensitivityUnits data);
    /// int32 __CFUNC DAQmxSetAIRVDTSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIRVDTSensitivityUnits(IntPtr taskHandle, string channel, AIRotaryVariableDifferentialTransformerSensitivityUnits data);
    /// int32 __CFUNC DAQmxResetAIRVDTSensitivityUnits(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIRVDTSensitivityUnits(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_EddyCurrentProxProbe_Units ***
// // Uses value set LengthUnits2
//     /// int32 __CFUNC DAQmxGetAIEddyCurrentProxProbeUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIEddyCurrentProxProbeUnits(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIEddyCurrentProxProbeUnits(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_EddyCurrentProxProbe_Sensitivity ***
//     /// int32 __CFUNC DAQmxGetAIEddyCurrentProxProbeSensitivity(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIEddyCurrentProxProbeSensitivity(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIEddyCurrentProxProbeSensitivity(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_EddyCurrentProxProbe_SensitivityUnits ***
// // Uses value set EddyCurrentProxProbeSensitivityUnits
//     /// int32 __CFUNC DAQmxGetAIEddyCurrentProxProbeSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIEddyCurrentProxProbeSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIEddyCurrentProxProbeSensitivityUnits(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_SoundPressure_MaxSoundPressureLvl ***
//     /// int32 __CFUNC DAQmxGetAISoundPressureMaxSoundPressureLvl(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAISoundPressureMaxSoundPressureLvl(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAISoundPressureMaxSoundPressureLvl(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_SoundPressure_Units ***
// // Uses value set SoundPressureUnits1
//     /// int32 __CFUNC DAQmxGetAISoundPressureUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAISoundPressureUnits(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAISoundPressureUnits(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_SoundPressure_dBRef ***
//     /// int32 __CFUNC DAQmxGetAISoundPressuredBRef(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAISoundPressuredBRef(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAISoundPressuredBRef(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Microphone_Sensitivity ***
//     /// int32 __CFUNC DAQmxGetAIMicrophoneSensitivity(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIMicrophoneSensitivity(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIMicrophoneSensitivity(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Accel_Units ***
// // Uses value set AccelUnits2
//     /// int32 __CFUNC DAQmxGetAIAccelUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIAccelUnits(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIAccelUnits(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Accel_dBRef ***
//     /// int32 __CFUNC DAQmxGetAIAcceldBRef(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIAcceldBRef(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIAcceldBRef(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Accel_4WireDCVoltage_Sensitivity ***
//    /// int32 __CFUNC DAQmxGetAIAccel4WireDCVoltageSensitivity(TaskHandle taskHandle, const char channel[], float64 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//    /// int32 __CFUNC DAQmxSetAIAccel4WireDCVoltageSensitivity(TaskHandle taskHandle, const char channel[], float64 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIAccel4WireDCVoltageSensitivity(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Accel_4WireDCVoltage_SensitivityUnits ***
//// Uses value set AccelSensitivityUnits1
//    /// int32 __CFUNC DAQmxGetAIAccel4WireDCVoltageSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIAccel4WireDCVoltageSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIAccel4WireDCVoltageSensitivityUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Accel_Sensitivity ***
//    /// int32 __CFUNC DAQmxGetAIAccelSensitivity(TaskHandle taskHandle, const char channel[], float64 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//    /// int32 __CFUNC DAQmxSetAIAccelSensitivity(TaskHandle taskHandle, const char channel[], float64 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIAccelSensitivity(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Accel_SensitivityUnits ***
//// Uses value set AccelSensitivityUnits1
//    /// int32 __CFUNC DAQmxGetAIAccelSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIAccelSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIAccelSensitivityUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Accel_Charge_Sensitivity ***
//    /// int32 __CFUNC DAQmxGetAIAccelChargeSensitivity(TaskHandle taskHandle, const char channel[], float64 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//    /// int32 __CFUNC DAQmxSetAIAccelChargeSensitivity(TaskHandle taskHandle, const char channel[], float64 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIAccelChargeSensitivity(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Accel_Charge_SensitivityUnits ***
//// Uses value set AccelChargeSensitivityUnits
//    /// int32 __CFUNC DAQmxGetAIAccelChargeSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIAccelChargeSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIAccelChargeSensitivityUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Velocity_Units ***
//// Uses value set VelocityUnits
//    /// int32 __CFUNC DAQmxGetAIVelocityUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIVelocityUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIVelocityUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Velocity_IEPESensor_dBRef ***
//    /// int32 __CFUNC DAQmxGetAIVelocityIEPESensordBRef(TaskHandle taskHandle, const char channel[], float64 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//    /// int32 __CFUNC DAQmxSetAIVelocityIEPESensordBRef(TaskHandle taskHandle, const char channel[], float64 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIVelocityIEPESensordBRef(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Velocity_IEPESensor_Sensitivity ***
//    /// int32 __CFUNC DAQmxGetAIVelocityIEPESensorSensitivity(TaskHandle taskHandle, const char channel[], float64 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//    /// int32 __CFUNC DAQmxSetAIVelocityIEPESensorSensitivity(TaskHandle taskHandle, const char channel[], float64 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIVelocityIEPESensorSensitivity(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Velocity_IEPESensor_SensitivityUnits ***
//// Uses value set VelocityIEPESensorSensitivityUnits
//    /// int32 __CFUNC DAQmxGetAIVelocityIEPESensorSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIVelocityIEPESensorSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIVelocityIEPESensorSensitivityUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Force_Units ***
//// Uses value set ForceUnits
//    /// int32 __CFUNC DAQmxGetAIForceUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIForceUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIForceUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Force_IEPESensor_Sensitivity ***
//    /// int32 __CFUNC DAQmxGetAIForceIEPESensorSensitivity(TaskHandle taskHandle, const char channel[], float64 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//    /// int32 __CFUNC DAQmxSetAIForceIEPESensorSensitivity(TaskHandle taskHandle, const char channel[], float64 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIForceIEPESensorSensitivity(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Force_IEPESensor_SensitivityUnits ***
//// Uses value set ForceIEPESensorSensitivityUnits
//    /// int32 __CFUNC DAQmxGetAIForceIEPESensorSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIForceIEPESensorSensitivityUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIForceIEPESensorSensitivityUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Pressure_Units ***
//// Uses value set PressureUnits
//    /// int32 __CFUNC DAQmxGetAIPressureUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIPressureUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIPressureUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Torque_Units ***
//// Uses value set TorqueUnits
//    /// int32 __CFUNC DAQmxGetAITorqueUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAITorqueUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAITorqueUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Bridge_Units ***
//// Uses value set BridgeUnits
//    /// int32 __CFUNC DAQmxGetAIBridgeUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIBridgeUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIBridgeUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Bridge_ElectricalUnits ***
//// Uses value set BridgeElectricalUnits
//    /// int32 __CFUNC DAQmxGetAIBridgeElectricalUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIBridgeElectricalUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIBridgeElectricalUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Bridge_PhysicalUnits ***
//// Uses value set BridgePhysicalUnits
//    /// int32 __CFUNC DAQmxGetAIBridgePhysicalUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIBridgePhysicalUnits(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIBridgePhysicalUnits(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Bridge_ScaleType ***
//// Uses value set ScaleType4
//    /// int32 __CFUNC DAQmxGetAIBridgeScaleType(TaskHandle taskHandle, const char channel[], int32 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxSetAIBridgeScaleType(TaskHandle taskHandle, const char channel[], int32 data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
//    /// int32 __CFUNC DAQmxResetAIBridgeScaleType(TaskHandle taskHandle, const char channel[]);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//    internal static extern int Asd(IntPtr taskHandle, string channel);
////*** Set/Get functions for DAQmx_AI_Bridge_TwoPointLin_First_ElectricalVal ***
//    /// int32 __CFUNC DAQmxGetAIBridgeTwoPointLinFirstElectricalVal(TaskHandle taskHandle, const char channel[], float64 *data);
//    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeTwoPointLinFirstElectricalVal(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeTwoPointLinFirstElectricalVal(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_TwoPointLin_First_PhysicalVal ***
//     /// int32 __CFUNC DAQmxGetAIBridgeTwoPointLinFirstPhysicalVal(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeTwoPointLinFirstPhysicalVal(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxResetAIBridgeTwoPointLinFirstPhysicalVal(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_TwoPointLin_Second_ElectricalVal ***
//     /// int32 __CFUNC DAQmxGetAIBridgeTwoPointLinSecondElectricalVal(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeTwoPointLinSecondElectricalVal(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeTwoPointLinSecondElectricalVal(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_TwoPointLin_Second_PhysicalVal ***
//     /// int32 __CFUNC DAQmxGetAIBridgeTwoPointLinSecondPhysicalVal(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeTwoPointLinSecondPhysicalVal(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeTwoPointLinSecondPhysicalVal(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_Table_ElectricalVals ***
//     /// int32 __CFUNC DAQmxGetAIBridgeTableElectricalVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgeTableElectricalVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeTableElectricalVals(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_Table_PhysicalVals ***
//     /// int32 __CFUNC DAQmxGetAIBridgeTablePhysicalVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgeTablePhysicalVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeTablePhysicalVals(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_Poly_ForwardCoeff ***
//     /// int32 __CFUNC DAQmxGetAIBridgePolyForwardCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgePolyForwardCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgePolyForwardCoeff(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_Poly_ReverseCoeff ***
//     /// int32 __CFUNC DAQmxGetAIBridgePolyReverseCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgePolyReverseCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgePolyReverseCoeff(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Charge_Units ***
// // Uses value set ChargeUnits
//     /// int32 __CFUNC DAQmxGetAIChargeUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChargeUnits(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChargeUnits(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Is_TEDS ***
//     /// int32 __CFUNC DAQmxGetAIIsTEDS(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_TEDS_Units ***
//     /// int32 __CFUNC DAQmxGetAITEDSUnits(TaskHandle taskHandle, const char channel[], char *data, uint32 bufferSize);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Coupling ***
// // Uses value set Coupling1
//     /// int32 __CFUNC DAQmxGetAICoupling(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAICoupling(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAICoupling(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Impedance ***
// // Uses value set Impedance1
//     /// int32 __CFUNC DAQmxGetAIImpedance(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIImpedance(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIImpedance(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_TermCfg ***
// // Uses value set InputTermCfg
//     /// int32 __CFUNC DAQmxGetAITermCfg(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAITermCfg(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAITermCfg(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_InputSrc ***
//     /// int32 __CFUNC DAQmxGetAIInputSrc(TaskHandle taskHandle, const char channel[], char *data, uint32 bufferSize);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIInputSrc(TaskHandle taskHandle, const char channel[], const char *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIInputSrc(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ResistanceCfg ***
// // Uses value set ResistanceConfiguration
//     /// int32 __CFUNC DAQmxGetAIResistanceCfg(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIResistanceCfg(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIResistanceCfg(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_LeadWireResistance ***
//     /// int32 __CFUNC DAQmxGetAILeadWireResistance(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAILeadWireResistance(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAILeadWireResistance(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_Cfg ***
// // Uses value set BridgeConfiguration1
//     /// int32 __CFUNC DAQmxGetAIBridgeCfg(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgeCfg(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeCfg(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_NomResistance ***
//     /// int32 __CFUNC DAQmxGetAIBridgeNomResistance(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeNomResistance(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeNomResistance(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_InitialVoltage ***
//     /// int32 __CFUNC DAQmxGetAIBridgeInitialVoltage(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgeInitialVoltage(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeInitialVoltage(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_InitialRatio ***
//     /// int32 __CFUNC DAQmxGetAIBridgeInitialRatio(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeInitialRatio(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeInitialRatio(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_ShuntCal_Enable ***
//     /// int32 __CFUNC DAQmxGetAIBridgeShuntCalEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgeShuntCalEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeShuntCalEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_ShuntCal_Select ***
// // Uses value set ShuntCalSelect
//     /// int32 __CFUNC DAQmxGetAIBridgeShuntCalSelect(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgeShuntCalSelect(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeShuntCalSelect(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_ShuntCal_ShuntCalASrc ***
// // Uses value set BridgeShuntCalSource
//     /// int32 __CFUNC DAQmxGetAIBridgeShuntCalShuntCalASrc(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgeShuntCalShuntCalASrc(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeShuntCalShuntCalASrc(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_ShuntCal_GainAdjust ***
//     /// int32 __CFUNC DAQmxGetAIBridgeShuntCalGainAdjust(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeShuntCalGainAdjust(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeShuntCalGainAdjust(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_ShuntCal_ShuntCalAResistance ***
//     /// int32 __CFUNC DAQmxGetAIBridgeShuntCalShuntCalAResistance(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeShuntCalShuntCalAResistance(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeShuntCalShuntCalAResistance(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_ShuntCal_ShuntCalAActualResistance ***
//     /// int32 __CFUNC DAQmxGetAIBridgeShuntCalShuntCalAActualResistance(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeShuntCalShuntCalAActualResistance(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeShuntCalShuntCalAActualResistance(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_ShuntCal_ShuntCalBResistance ***
//     /// int32 __CFUNC DAQmxGetAIBridgeShuntCalShuntCalBResistance(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeShuntCalShuntCalBResistance(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeShuntCalShuntCalBResistance(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_ShuntCal_ShuntCalBActualResistance ***
//     /// int32 __CFUNC DAQmxGetAIBridgeShuntCalShuntCalBActualResistance(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIBridgeShuntCalShuntCalBActualResistance(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeShuntCalShuntCalBActualResistance(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_Balance_CoarsePot ***
//     /// int32 __CFUNC DAQmxGetAIBridgeBalanceCoarsePot(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgeBalanceCoarsePot(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeBalanceCoarsePot(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Bridge_Balance_FinePot ***
//     /// int32 __CFUNC DAQmxGetAIBridgeBalanceFinePot(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIBridgeBalanceFinePot(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIBridgeBalanceFinePot(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_CurrentShunt_Loc ***
// // Uses value set CurrentShuntResistorLocation1
//     /// int32 __CFUNC DAQmxGetAICurrentShuntLoc(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAICurrentShuntLoc(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAICurrentShuntLoc(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_CurrentShunt_Resistance ***
//     /// int32 __CFUNC DAQmxGetAICurrentShuntResistance(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAICurrentShuntResistance(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAICurrentShuntResistance(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Excit_Sense ***
// // Uses value set Sense
//     /// int32 __CFUNC DAQmxGetAIExcitSense(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIExcitSense(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIExcitSense(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Excit_Src ***
// // Uses value set ExcitationSource
//     /// int32 __CFUNC DAQmxGetAIExcitSrc(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIExcitSrc(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIExcitSrc(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Excit_Val ***
//     /// int32 __CFUNC DAQmxGetAIExcitVal(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIExcitVal(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIExcitVal(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Excit_UseForScaling ***
//     /// int32 __CFUNC DAQmxGetAIExcitUseForScaling(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIExcitUseForScaling(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIExcitUseForScaling(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Excit_UseMultiplexed ***
//     /// int32 __CFUNC DAQmxGetAIExcitUseMultiplexed(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIExcitUseMultiplexed(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIExcitUseMultiplexed(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Excit_ActualVal ***
//     /// int32 __CFUNC DAQmxGetAIExcitActualVal(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIExcitActualVal(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIExcitActualVal(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Excit_DCorAC ***
// // Uses value set ExcitationDCorAC
//     /// int32 __CFUNC DAQmxGetAIExcitDCorAC(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIExcitDCorAC(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIExcitDCorAC(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Excit_VoltageOrCurrent ***
// // Uses value set ExcitationVoltageOrCurrent
//     /// int32 __CFUNC DAQmxGetAIExcitVoltageOrCurrent(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIExcitVoltageOrCurrent(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIExcitVoltageOrCurrent(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Excit_IdleOutputBehavior ***
// // Uses value set ExcitationIdleOutputBehavior
//     /// int32 __CFUNC DAQmxGetAIExcitIdleOutputBehavior(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIExcitIdleOutputBehavior(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIExcitIdleOutputBehavior(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ACExcit_Freq ***
//     /// int32 __CFUNC DAQmxGetAIACExcitFreq(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIACExcitFreq(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIACExcitFreq(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ACExcit_SyncEnable ***
//     /// int32 __CFUNC DAQmxGetAIACExcitSyncEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIACExcitSyncEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIACExcitSyncEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ACExcit_WireMode ***
// // Uses value set ACExcitWireMode
//     /// int32 __CFUNC DAQmxGetAIACExcitWireMode(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIACExcitWireMode(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIACExcitWireMode(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_SensorPower_Voltage ***
//     /// int32 __CFUNC DAQmxGetAISensorPowerVoltage(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAISensorPowerVoltage(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAISensorPowerVoltage(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_SensorPower_Cfg ***
// // Uses value set SensorPowerCfg
//     /// int32 __CFUNC DAQmxGetAISensorPowerCfg(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAISensorPowerCfg(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAISensorPowerCfg(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_SensorPower_Type ***
// // Uses value set SensorPowerType
//     /// int32 __CFUNC DAQmxGetAISensorPowerType(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAISensorPowerType(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAISensorPowerType(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_OpenThrmcplDetectEnable ***
//     /// int32 __CFUNC DAQmxGetAIOpenThrmcplDetectEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIOpenThrmcplDetectEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIOpenThrmcplDetectEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Thrmcpl_LeadOffsetVoltage ***
//     /// int32 __CFUNC DAQmxGetAIThrmcplLeadOffsetVoltage(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIThrmcplLeadOffsetVoltage(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIThrmcplLeadOffsetVoltage(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Atten ***
//     /// int32 __CFUNC DAQmxGetAIAtten(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIAtten(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIAtten(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ProbeAtten ***
//     /// int32 __CFUNC DAQmxGetAIProbeAtten(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIProbeAtten(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIProbeAtten(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Lowpass_Enable ***
//     /// int32 __CFUNC DAQmxGetAILowpassEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAILowpassEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAILowpassEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Lowpass_CutoffFreq ***
//     /// int32 __CFUNC DAQmxGetAILowpassCutoffFreq(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAILowpassCutoffFreq(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAILowpassCutoffFreq(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Lowpass_SwitchCap_ClkSrc ***
// // Uses value set SourceSelection
//     /// int32 __CFUNC DAQmxGetAILowpassSwitchCapClkSrc(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAILowpassSwitchCapClkSrc(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAILowpassSwitchCapClkSrc(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Lowpass_SwitchCap_ExtClkFreq ***
//     /// int32 __CFUNC DAQmxGetAILowpassSwitchCapExtClkFreq(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAILowpassSwitchCapExtClkFreq(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAILowpassSwitchCapExtClkFreq(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Lowpass_SwitchCap_ExtClkDiv ***
//     /// int32 __CFUNC DAQmxGetAILowpassSwitchCapExtClkDiv(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAILowpassSwitchCapExtClkDiv(TaskHandle taskHandle, const char channel[], uint32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAILowpassSwitchCapExtClkDiv(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Lowpass_SwitchCap_OutClkDiv ***
//     /// int32 __CFUNC DAQmxGetAILowpassSwitchCapOutClkDiv(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAILowpassSwitchCapOutClkDiv(TaskHandle taskHandle, const char channel[], uint32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAILowpassSwitchCapOutClkDiv(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Enable ***
//     /// int32 __CFUNC DAQmxGetAIDigFltrEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIDigFltrEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Type ***
// // Uses value set FilterType2
//     /// int32 __CFUNC DAQmxGetAIDigFltrType(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIDigFltrType(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrType(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Response ***
// // Uses value set FilterResponse
//     /// int32 __CFUNC DAQmxGetAIDigFltrResponse(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIDigFltrResponse(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrResponse(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Order ***
//     /// int32 __CFUNC DAQmxGetAIDigFltrOrder(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIDigFltrOrder(TaskHandle taskHandle, const char channel[], uint32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrOrder(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Lowpass_CutoffFreq ***
//     /// int32 __CFUNC DAQmxGetAIDigFltrLowpassCutoffFreq(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIDigFltrLowpassCutoffFreq(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrLowpassCutoffFreq(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Highpass_CutoffFreq ***
//     /// int32 __CFUNC DAQmxGetAIDigFltrHighpassCutoffFreq(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIDigFltrHighpassCutoffFreq(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrHighpassCutoffFreq(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Bandpass_CenterFreq ***
//     /// int32 __CFUNC DAQmxGetAIDigFltrBandpassCenterFreq(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIDigFltrBandpassCenterFreq(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrBandpassCenterFreq(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Bandpass_Width ***
//     /// int32 __CFUNC DAQmxGetAIDigFltrBandpassWidth(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIDigFltrBandpassWidth(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrBandpassWidth(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Notch_CenterFreq ***
//     /// int32 __CFUNC DAQmxGetAIDigFltrNotchCenterFreq(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIDigFltrNotchCenterFreq(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrNotchCenterFreq(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Notch_Width ***
//     /// int32 __CFUNC DAQmxGetAIDigFltrNotchWidth(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIDigFltrNotchWidth(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrNotchWidth(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DigFltr_Coeff ***
//     /// int32 __CFUNC DAQmxGetAIDigFltrCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIDigFltrCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDigFltrCoeff(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Filter_Enable ***
//     /// int32 __CFUNC DAQmxGetAIFilterEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIFilterEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIFilterEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Filter_Freq ***
//     /// int32 __CFUNC DAQmxGetAIFilterFreq(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIFilterFreq(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIFilterFreq(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Filter_Response ***
// // Uses value set FilterResponse1
//     /// int32 __CFUNC DAQmxGetAIFilterResponse(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIFilterResponse(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIFilterResponse(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Filter_Order ***
//     /// int32 __CFUNC DAQmxGetAIFilterOrder(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIFilterOrder(TaskHandle taskHandle, const char channel[], uint32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIFilterOrder(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_FilterDelay ***
//     /// int32 __CFUNC DAQmxGetAIFilterDelay(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
// //*** Set/Get functions for DAQmx_AI_FilterDelayUnits ***
// // Uses value set DigitalWidthUnits4
//     /// int32 __CFUNC DAQmxGetAIFilterDelayUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIFilterDelayUnits(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIFilterDelayUnits(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_RemoveFilterDelay ***
//     /// int32 __CFUNC DAQmxGetAIRemoveFilterDelay(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIRemoveFilterDelay(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIRemoveFilterDelay(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_FilterDelayAdjustment ***
//     /// int32 __CFUNC DAQmxGetAIFilterDelayAdjustment(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIFilterDelayAdjustment(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIFilterDelayAdjustment(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_AveragingWinSize ***
//     /// int32 __CFUNC DAQmxGetAIAveragingWinSize(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIAveragingWinSize(TaskHandle taskHandle, const char channel[], uint32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIAveragingWinSize(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ResolutionUnits ***
// // Uses value set ResolutionType1
//     /// int32 __CFUNC DAQmxGetAIResolutionUnits(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Resolution ***
//     /// int32 __CFUNC DAQmxGetAIResolution(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
// //*** Set/Get functions for DAQmx_AI_RawSampSize ***
//     /// int32 __CFUNC DAQmxGetAIRawSampSize(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_RawSampJustification ***
// // Uses value set DataJustification1
//     /// int32 __CFUNC DAQmxGetAIRawSampJustification(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_ADCTimingMode ***
// Uses value set ADCTimingMode
    /// int32 __CFUNC DAQmxGetAIADCTimingMode(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIADCTimingMode(IntPtr taskHandle, string channel, out ADCTimingMode data);
    /// int32 __CFUNC DAQmxSetAIADCTimingMode(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIADCTimingMode(IntPtr taskHandle, string channel, ADCTimingMode data);
    /// int32 __CFUNC DAQmxResetAIADCTimingMode(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIADCTimingMode(IntPtr taskHandle, string channel);
//*** Set/Get functions for DAQmx_AI_ADCCustomTimingMode ***
    /// int32 __CFUNC DAQmxGetAIADCCustomTimingMode(TaskHandle taskHandle, const char channel[], uint32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetAIADCCustomTimingMode(IntPtr taskHandle, string channel, out uint data);
    /// int32 __CFUNC DAQmxSetAIADCCustomTimingMode(TaskHandle taskHandle, const char channel[], uint32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetAIADCCustomTimingMode(IntPtr taskHandle, string channel, uint data);
    /// int32 __CFUNC DAQmxResetAIADCCustomTimingMode(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetAIADCCustomTimingMode(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Dither_Enable ***
//     /// int32 __CFUNC DAQmxGetAIDitherEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIDitherEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDitherEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_HasValidCalInfo ***
//     /// int32 __CFUNC DAQmxGetAIChanCalHasValidCalInfo(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_EnableCal ***
//     /// int32 __CFUNC DAQmxGetAIChanCalEnableCal(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalEnableCal(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalEnableCal(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_ApplyCalIfExp ***
//     /// int32 __CFUNC DAQmxGetAIChanCalApplyCalIfExp(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalApplyCalIfExp(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalApplyCalIfExp(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_ScaleType ***
// // Uses value set ScaleType3
//     /// int32 __CFUNC DAQmxGetAIChanCalScaleType(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalScaleType(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalScaleType(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_Table_PreScaledVals ***
//     /// int32 __CFUNC DAQmxGetAIChanCalTablePreScaledVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalTablePreScaledVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalTablePreScaledVals(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_Table_ScaledVals ***
//     /// int32 __CFUNC DAQmxGetAIChanCalTableScaledVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalTableScaledVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalTableScaledVals(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_Poly_ForwardCoeff ***
//     /// int32 __CFUNC DAQmxGetAIChanCalPolyForwardCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalPolyForwardCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalPolyForwardCoeff(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_Poly_ReverseCoeff ***
//     /// int32 __CFUNC DAQmxGetAIChanCalPolyReverseCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalPolyReverseCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalPolyReverseCoeff(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_OperatorName ***
//     /// int32 __CFUNC DAQmxGetAIChanCalOperatorName(TaskHandle taskHandle, const char channel[], char *data, uint32 bufferSize);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalOperatorName(TaskHandle taskHandle, const char channel[], const char *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalOperatorName(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_Desc ***
//     /// int32 __CFUNC DAQmxGetAIChanCalDesc(TaskHandle taskHandle, const char channel[], char *data, uint32 bufferSize);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalDesc(TaskHandle taskHandle, const char channel[], const char *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalDesc(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_Verif_RefVals ***
//     /// int32 __CFUNC DAQmxGetAIChanCalVerifRefVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalVerifRefVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalVerifRefVals(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChanCal_Verif_AcqVals ***
//     /// int32 __CFUNC DAQmxGetAIChanCalVerifAcqVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChanCalVerifAcqVals(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChanCalVerifAcqVals(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Rng_High ***
//     /// int32 __CFUNC DAQmxGetAIRngHigh(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIRngHigh(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIRngHigh(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Rng_Low ***
//     /// int32 __CFUNC DAQmxGetAIRngLow(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIRngLow(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIRngLow(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DCOffset ***
//     /// int32 __CFUNC DAQmxGetAIDCOffset(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIDCOffset(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDCOffset(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_Gain ***
//     /// int32 __CFUNC DAQmxGetAIGain(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIGain(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIGain(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_SampAndHold_Enable ***
//     /// int32 __CFUNC DAQmxGetAISampAndHoldEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAISampAndHoldEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAISampAndHoldEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_AutoZeroMode ***
// // Uses value set AutoZeroType1
//     /// int32 __CFUNC DAQmxGetAIAutoZeroMode(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIAutoZeroMode(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIAutoZeroMode(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_ChopEnable ***
//     /// int32 __CFUNC DAQmxGetAIChopEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIChopEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIChopEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DataXferMaxRate ***
//     /// int32 __CFUNC DAQmxGetAIDataXferMaxRate(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIDataXferMaxRate(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDataXferMaxRate(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DataXferMech ***
// // Uses value set DataTransferMechanism
//     /// int32 __CFUNC DAQmxGetAIDataXferMech(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIDataXferMech(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDataXferMech(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DataXferReqCond ***
// // Uses value set InputDataTransferCondition
//     /// int32 __CFUNC DAQmxGetAIDataXferReqCond(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIDataXferReqCond(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDataXferReqCond(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DataXferCustomThreshold ***
//     /// int32 __CFUNC DAQmxGetAIDataXferCustomThreshold(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIDataXferCustomThreshold(TaskHandle taskHandle, const char channel[], uint32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIDataXferCustomThreshold(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_UsbXferReqSize ***
//     /// int32 __CFUNC DAQmxGetAIUsbXferReqSize(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIUsbXferReqSize(TaskHandle taskHandle, const char channel[], uint32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIUsbXferReqSize(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_UsbXferReqCount ***
//     /// int32 __CFUNC DAQmxGetAIUsbXferReqCount(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIUsbXferReqCount(TaskHandle taskHandle, const char channel[], uint32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIUsbXferReqCount(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_MemMapEnable ***
//     /// int32 __CFUNC DAQmxGetAIMemMapEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIMemMapEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIMemMapEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_RawDataCompressionType ***
// // Uses value set RawDataCompressionType
//     /// int32 __CFUNC DAQmxGetAIRawDataCompressionType(TaskHandle taskHandle, const char channel[], int32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIRawDataCompressionType(TaskHandle taskHandle, const char channel[], int32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIRawDataCompressionType(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_LossyLSBRemoval_CompressedSampSize ***
//     /// int32 __CFUNC DAQmxGetAILossyLSBRemovalCompressedSampSize(TaskHandle taskHandle, const char channel[], uint32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAILossyLSBRemovalCompressedSampSize(TaskHandle taskHandle, const char channel[], uint32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAILossyLSBRemovalCompressedSampSize(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_DevScalingCoeff ***
//     /// int32 __CFUNC DAQmxGetAIDevScalingCoeff(TaskHandle taskHandle, const char channel[], float64 *data, uint32 arraySizeInElements);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_EnhancedAliasRejectionEnable ***
//     /// int32 __CFUNC DAQmxGetAIEnhancedAliasRejectionEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIEnhancedAliasRejectionEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIEnhancedAliasRejectionEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_OpenChanDetectEnable ***
//     /// int32 __CFUNC DAQmxGetAIOpenChanDetectEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIOpenChanDetectEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIOpenChanDetectEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_InputLimitsFaultDetect_UpperLimit ***
//     /// int32 __CFUNC DAQmxGetAIInputLimitsFaultDetectUpperLimit(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIInputLimitsFaultDetectUpperLimit(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIInputLimitsFaultDetectUpperLimit(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_InputLimitsFaultDetect_LowerLimit ***
//     /// int32 __CFUNC DAQmxGetAIInputLimitsFaultDetectLowerLimit(TaskHandle taskHandle, const char channel[], float64 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel, out double data);
//     /// int32 __CFUNC DAQmxSetAIInputLimitsFaultDetectLowerLimit(TaskHandle taskHandle, const char channel[], float64 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIInputLimitsFaultDetectLowerLimit(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_InputLimitsFaultDetectEnable ***
//     /// int32 __CFUNC DAQmxGetAIInputLimitsFaultDetectEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIInputLimitsFaultDetectEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIInputLimitsFaultDetectEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_PowerSupplyFaultDetectEnable ***
//     /// int32 __CFUNC DAQmxGetAIPowerSupplyFaultDetectEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIPowerSupplyFaultDetectEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIPowerSupplyFaultDetectEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
// //*** Set/Get functions for DAQmx_AI_OvercurrentDetectEnable ***
//     /// int32 __CFUNC DAQmxGetAIOvercurrentDetectEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxSetAIOvercurrentDetectEnable(TaskHandle taskHandle, const char channel[], bool32 data);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
//     /// int32 __CFUNC DAQmxResetAIOvercurrentDetectEnable(TaskHandle taskHandle, const char channel[]);
//     [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
//     internal static extern int Asd(IntPtr taskHandle, string channel);
        //*** Set/Get functions for DAQmx_DI_InvertLines ***
    /// int32 __CFUNC DAQmxGetDIInvertLines(TaskHandle taskHandle, const char channel[], bool32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIInvertLines(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDIInvertLines(TaskHandle taskHandle, const char channel[], bool32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIInvertLines(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDIInvertLines(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIInvertLines(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_NumLines ***
    /// int32 __CFUNC DAQmxGetDINumLines(TaskHandle taskHandle, const char channel[], uInt32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDINumLines(IntPtr taskHandle, string channel, out uint data);
    //*** Set/Get functions for DAQmx_DI_DigFltr_Enable ***
    /// int32 __CFUNC DAQmxGetDIDigFltrEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIDigFltrEnable(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDIDigFltrEnable(TaskHandle taskHandle, const char channel[], bool32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIDigFltrEnable(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDIDigFltrEnable(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIDigFltrEnable(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_DigFltr_MinPulseWidth ***
    /// int32 __CFUNC DAQmxGetDIDigFltrMinPulseWidth(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIDigFltrMinPulseWidth(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetDIDigFltrMinPulseWidth(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIDigFltrMinPulseWidth(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetDIDigFltrMinPulseWidth(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIDigFltrMinPulseWidth(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_DigFltr_EnableBusMode ***
    /// int32 __CFUNC DAQmxGetDIDigFltrEnableBusMode(TaskHandle taskHandle, const char channel[], bool32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIDigFltrEnableBusMode(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDIDigFltrEnableBusMode(TaskHandle taskHandle, const char channel[], bool32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIDigFltrEnableBusMode(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDIDigFltrEnableBusMode(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIDigFltrEnableBusMode(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_DigFltr_TimebaseSrc ***
    /// int32 __CFUNC DAQmxGetDIDigFltrTimebaseSrc(TaskHandle taskHandle, const char channel[], char *data, uInt32 bufferSize);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIDigFltrTimebaseSrc(IntPtr taskHandle, string channel, in byte data, uint bufferSize);
    /// int32 __CFUNC DAQmxSetDIDigFltrTimebaseSrc(TaskHandle taskHandle, const char channel[], const char *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIDigFltrTimebaseSrc(IntPtr taskHandle, string channel, string data);
    /// int32 __CFUNC DAQmxResetDIDigFltrTimebaseSrc(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIDigFltrTimebaseSrc(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_DigFltr_TimebaseRate ***
    /// int32 __CFUNC DAQmxGetDIDigFltrTimebaseRate(TaskHandle taskHandle, const char channel[], float64 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIDigFltrTimebaseRate(IntPtr taskHandle, string channel, out double data);
    /// int32 __CFUNC DAQmxSetDIDigFltrTimebaseRate(TaskHandle taskHandle, const char channel[], float64 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIDigFltrTimebaseRate(IntPtr taskHandle, string channel, double data);
    /// int32 __CFUNC DAQmxResetDIDigFltrTimebaseRate(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIDigFltrTimebaseRate(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_DigSync_Enable ***
    /// int32 __CFUNC DAQmxGetDIDigSyncEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIDigSyncEnable(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDIDigSyncEnable(TaskHandle taskHandle, const char channel[], bool32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIDigSyncEnable(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDIDigSyncEnable(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIDigSyncEnable(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_Tristate ***
    /// int32 __CFUNC DAQmxGetDITristate(TaskHandle taskHandle, const char channel[], bool32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDITristate(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDITristate(TaskHandle taskHandle, const char channel[], bool32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDITristate(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDITristate(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDITristate(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_LogicFamily ***
    // Uses value set LogicFamily
    /// int32 __CFUNC DAQmxGetDILogicFamily(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDILogicFamily(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDILogicFamily(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDILogicFamily(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDILogicFamily(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDILogicFamily(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_DataXferMech ***
    // Uses value set DataTransferMechanism
    /// int32 __CFUNC DAQmxGetDIDataXferMech(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIDataXferMech(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDIDataXferMech(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIDataXferMech(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDIDataXferMech(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIDataXferMech(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_DataXferReqCond ***
    // Uses value set InputDataTransferCondition
    /// int32 __CFUNC DAQmxGetDIDataXferReqCond(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIDataXferReqCond(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDIDataXferReqCond(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIDataXferReqCond(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDIDataXferReqCond(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIDataXferReqCond(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_UsbXferReqSize ***
    /// int32 __CFUNC DAQmxGetDIUsbXferReqSize(TaskHandle taskHandle, const char channel[], uInt32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIUsbXferReqSize(IntPtr taskHandle, string channel, out uint data);
    /// int32 __CFUNC DAQmxSetDIUsbXferReqSize(TaskHandle taskHandle, const char channel[], uInt32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIUsbXferReqSize(IntPtr taskHandle, string channel, uint data);
    /// int32 __CFUNC DAQmxResetDIUsbXferReqSize(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIUsbXferReqSize(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_UsbXferReqCount ***
    /// int32 __CFUNC DAQmxGetDIUsbXferReqCount(TaskHandle taskHandle, const char channel[], uInt32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIUsbXferReqCount(IntPtr taskHandle, string channel, out uint data);
    /// int32 __CFUNC DAQmxSetDIUsbXferReqCount(TaskHandle taskHandle, const char channel[], uInt32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIUsbXferReqCount(IntPtr taskHandle, string channel, uint data);
    /// int32 __CFUNC DAQmxResetDIUsbXferReqCount(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIUsbXferReqCount(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_MemMapEnable ***
    /// int32 __CFUNC DAQmxGetDIMemMapEnable(TaskHandle taskHandle, const char channel[], bool32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIMemMapEnable(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDIMemMapEnable(TaskHandle taskHandle, const char channel[], bool32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIMemMapEnable(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDIMemMapEnable(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIMemMapEnable(IntPtr taskHandle, string channel);
    //*** Set/Get functions for DAQmx_DI_AcquireOn ***
    // Uses value set SampleClockActiveOrInactiveEdgeSelection
    /// int32 __CFUNC DAQmxGetDIAcquireOn(TaskHandle taskHandle, const char channel[], int32 *data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetDIAcquireOn(IntPtr taskHandle, string channel, out int data);
    /// int32 __CFUNC DAQmxSetDIAcquireOn(TaskHandle taskHandle, const char channel[], int32 data);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxSetDIAcquireOn(IntPtr taskHandle, string channel, int data);
    /// int32 __CFUNC DAQmxResetDIAcquireOn(TaskHandle taskHandle, const char channel[]);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxResetDIAcquireOn(IntPtr taskHandle, string channel);


/******************************************************/
/***                    Timing                      ***/
/******************************************************/


// (Analog/Counter Timing)
    /// int32 __CFUNC     DAQmxCfgSampClkTiming          (TaskHandle taskHandle, const char source[], float64 rate, int32 activeEdge, int32 sampleMode, uInt64 sampsPerChan);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxCfgSampClkTiming(IntPtr taskHandle, string source, double rate, SampleClockActiveEdge edge, SampleQuantityMode sampleMode, ulong samsPerChan);
// (Digital Timing)
    /// int32 __CFUNC     DAQmxCfgHandshakingTiming      (TaskHandle taskHandle, int32 sampleMode, uInt64 sampsPerChan);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxCfgHandshakingTiming(IntPtr taskHandle, SampleQuantityMode sampleMode, uint sampsPerChan);
// (Burst Import Clock Timing)
    /// int32 __CFUNC     DAQmxCfgBurstHandshakingTimingImportClock(TaskHandle taskHandle, int32 sampleMode, uInt64 sampsPerChan, float64 sampleClkRate, const char sampleClkSrc[], int32 sampleClkActiveEdge, int32 pauseWhen, int32 readyEventActiveLevel);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxCfgBurstHandshakingTimingImportClock(IntPtr taskHandle, SampleQuantityMode sampleMode, ulong samsPerChan, double sampleClkRate, string sampleClkSrc, SampleClockActiveEdge sampleClkActiveEdge, int readyEventActiveLevel);
// (Burst Export Clock Timing)
    /// int32 __CFUNC     DAQmxCfgBurstHandshakingTimingExportClock(TaskHandle taskHandle, int32 sampleMode, uInt64 sampsPerChan, float64 sampleClkRate, const char sampleClkOutpTerm[], int32 sampleClkPulsePolarity, int32 pauseWhen, int32 readyEventActiveLevel);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxCfgBurstHandshakingTimingExportClock(IntPtr taskHandle, SampleQuantityMode sampleMode, ulong samsPerChan, double sampleClkRate, string sampleClkOutpTerm, int sampleClkPulsePolarity, int pauseWhen, int readyEventActiveLevel);
    /// int32 __CFUNC     DAQmxCfgChangeDetectionTiming  (TaskHandle taskHandle, const char risingEdgeChan[], const char fallingEdgeChan[], int32 sampleMode, uInt64 sampsPerChan);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxCfgChangeDetectionTiming(IntPtr taskHandle, string risingEdgeChan, string fallingEdgeChan, SampleQuantityMode sampleMode, ulong samsPerChan);
// (Counter Timing)
    /// int32 __CFUNC     DAQmxCfgImplicitTiming         (TaskHandle taskHandle, int32 sampleMode, uInt64 sampsPerChan);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxCfgImplicitTiming(IntPtr taskHandle, SampleQuantityMode sampleMode, ulong sampsPerChan);
// (Pipelined Sample Clock Timing)
    /// int32 __CFUNC     DAQmxCfgPipelinedSampClkTiming (TaskHandle taskHandle, const char source[], float64 rate, int32 activeEdge, int32 sampleMode, uInt64 sampsPerChan);
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxCfgPipelinedSampClkTiming(IntPtr taskHandle, string source, double rate, SampleClockActiveEdge activeEdge, SampleQuantityMode sampleMode, ulong samsPerChan);

    // /// int32 __CFUNC_C   DAQmxGetTimingAttribute        (TaskHandle taskHandle, int32 attribute, void *value, ...);
    // [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    // internal static extern int DAQmxGetTimingAttribute(IntPtr taskHandle);
    // /// int32 __CFUNC_C   DAQmxSetTimingAttribute        (TaskHandle taskHandle, int32 attribute, ...);
    // [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    // internal static extern int DAQmxSetTimingAttribute(IntPtr taskHandle);
    // /// int32 __CFUNC     DAQmxResetTimingAttribute      (TaskHandle taskHandle, int32 attribute);
    // [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    // internal static extern int DAQmxResetTimingAttribute(IntPtr taskHandle, int attribute);

    // /// int32 __CFUNC_C   DAQmxGetTimingAttributeEx      (TaskHandle taskHandle, const char deviceNames[], int32 attribute, void *value, ...);
    // [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    // internal static extern int DAQmxGetTimingAttributeEx(IntPtr taskHandle);
    // /// int32 __CFUNC_C   DAQmxSetTimingAttributeEx      (TaskHandle taskHandle, const char deviceNames[], int32 attribute, ...);
    // [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    // internal static extern int DAQmxSetTimingAttributeEx(IntPtr taskHandle);
    // /// int32 __CFUNC     DAQmxResetTimingAttributeEx    (TaskHandle taskHandle, const char deviceNames[], int32 attribute);
    // [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    // internal static extern int DAQmxResetTimingAttributeEx(IntPtr taskHandle, string deviceNames, int attribute);


}