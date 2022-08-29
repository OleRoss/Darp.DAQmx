using System.Runtime.InteropServices;
using System.Text;

namespace Darp.DAQmx.NationalInstruments.Functions;

internal partial class Interop
{
    /// int32 __CFUNC     DAQmxGetErrorString            (int32 errorCode, char errorString[], uInt32 bufferSize);
    /// https://www.ni.com/docs/en-US/bundle/ni-daqmx-c-api-ref/page/daqmxcfunc/daqmxgeterrorstring.html
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetErrorString(int errorCode, StringBuilder errorString, uint bufferSize);

    // int32 __CFUNC     DAQmxGetExtendedErrorInfo      (char errorString[], uInt32 bufferSize);
}