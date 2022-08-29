using System;

namespace Darp.DAQmx;

public class DaqMxException : Exception
{
    public DaqMxException(string message)
        : base(message)
    {
    }

    public DaqMxException(int errorCode, string message)
        : base($"{errorCode} - {message} ({DaqMx.GetErrorString(errorCode)})")
    {
    }
}