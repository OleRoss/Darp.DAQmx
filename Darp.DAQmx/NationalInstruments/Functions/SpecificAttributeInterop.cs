using System;
using System.Runtime.InteropServices;

namespace Darp.DAQmx.NationalInstruments.Functions;

internal partial class Interop
{
    [DllImport(Lib, CallingConvention = CallingConvention.StdCall)]
    internal static extern int DAQmxGetSampClkRate(IntPtr taskHandle, ref double data);
}