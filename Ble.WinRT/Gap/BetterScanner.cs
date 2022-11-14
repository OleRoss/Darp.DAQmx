using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

// ReSharper disable InconsistentNaming

namespace Bluetooth.WinRT.Advertisement
{

    internal class BetterScanner
    {
        private readonly ILogger? _logger;
        private IntPtr? _handle;
        private IntPtr? _findHandle;
        private readonly CancellationTokenSource _source;

        public BetterScanner(ILogger? logger)
        {
            _logger = logger;
            _source = new CancellationTokenSource();
        }

        /// <summary>
        /// Starts scanning for LE devices.
        /// Example: BetterScanner.StartScanner(0, 29, 29)
        /// </summary>
        /// <param name="scanType">0 = Passive, 1 = Active</param>
        /// <param name="scanInterval">Interval in 0.625 ms units</param>
        /// <param name="scanWindow">Window in 0.625 ms units</param>
        public async Task<bool> StartScanner(int scanType, ushort scanInterval, ushort scanWindow)
        {
            if (_source.IsCancellationRequested)
            {
                _logger?.LogWarning("Cannot start better scanner! This instance was already started");
                return false;
            }
            return await Task.Run(() =>
            {
                var param = new BLUETOOTH_FIND_RADIO_PARAM();
                param.Initialize();
                _findHandle = BluetoothFindFirstRadio(ref param, out var radioHandle);
                _handle = radioHandle;
                var req = new LE_SCAN_REQUEST
                {
                    unknown1 = 0,
                    scanType = scanType,
                    unknown2 = 0,
                    scanInterval = scanInterval,
                    scanWindow = scanWindow,
                    unknown3_0 = 0,
                    unknown3_1 = 0
                };
                try
                {
                    var success = DeviceIoControl(radioHandle,
                        0x41118c,
                        ref req,
                        (uint)Marshal.SizeOf<LE_SCAN_REQUEST>(),
                        IntPtr.Zero,
                        0,
                        out _,
                        IntPtr.Zero);

                    if (success)
                    {
                        _logger?.LogTrace("Successfully returned from scanner");
                        return true;
                    }

                    var errorCode = Marshal.GetLastWin32Error();
                    if (errorCode == 87)
                        _logger?.LogError(
                            "Failed because of error code {ErrorCode}: A provided parameter is invalid. " +
                            "Check for example the provided times (got {ScanType}, {ScanInterval}, {ScanWindow})",
                            errorCode, scanType, scanInterval, scanWindow);
                    else if (errorCode == 1784)
                        _logger?.LogError(
                            "Failed because of error code {ErrorCode}: An invalid buffer has been defined " +
                            "(see https://stackoverflow.com/questions/62767336/ble-scan-interval-windows-10-deviceiocontrol-return-false-with-last-error-178)",
                            errorCode);
                    else
                        _logger?.LogError("Failed because of error code {ErrorCode}", errorCode);
                }
                catch (Exception e)
                {
                    _logger?.LogCritical(e, "Got an exception!");
                }
                return false;
            }, _source.Token);
        }

        public void Close()
        {
            if (!_handle.HasValue || !_findHandle.HasValue) return;
            _logger?.LogTrace("Closing scanner");
            _source.Cancel();
            // TODO Handle closing events - see overlapped io
            // https://stackoverflow.com/questions/37307301/ble-scan-interval-windows-10/37328965
            //Console.WriteLine("Attempting to close scanner");

            _findHandle = null;

            _handle = null;
        }
        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="handle">[In] A valid handle to an open object.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("Kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// Finds the first bluetooth radio present in device manager
        /// </summary>
        /// <param name="pbtfrp">Pointer to a BLUETOOTH_FIND_RADIO_PARAMS structure</param>
        /// <param name="phRadio">Pointer to where the first enumerated radio handle will be returned. When no longer needed, this handle must be closed via CloseHandle.</param>
        /// <returns>In addition to the handle indicated by phRadio, calling this function will also create a HBLUETOOTH_RADIO_FIND handle for use with the BluetoothFindNextRadio function.
        /// When this handle is no longer needed, it must be closed via the BluetoothFindRadioClose.
        /// Returns NULL upon failure. Call the GetLastError function for more information on the error. The following table describe common errors:</returns>
        [DllImport("irprops.cpl", SetLastError = true)]
        static extern IntPtr BluetoothFindFirstRadio(ref BLUETOOTH_FIND_RADIO_PARAM pbtfrp, out IntPtr phRadio);

        /// <summary>
        /// The BluetoothFindRadioClose function closes the enumeration handle associated with finding Bluetooth radios.
        /// </summary>
        /// <param name="handle">Handle for the query to be closed. Obtained in a previous call to the <see cref="BluetoothFindFirstRadio"/> function.</param>
        /// <returns>
        /// Returns TRUE when the handle is successfully closed.
        /// Returns FALSE if the attempt fails to close the enumeration handle. For additional information on possible errors associated with closing the handle, call the GetLastError function.


        /// </returns>
        [DllImport("irprops.cpl", SetLastError = true)]
        static extern bool BluetoothFindRadioClose(IntPtr handle);
        /// <summary>
        /// The BLUETOOTH_FIND_RADIO_PARAMS structure facilitates enumerating installed Bluetooth radios.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct BLUETOOTH_FIND_RADIO_PARAM
        {
            internal UInt32 dwSize;
            internal void Initialize()
            {
                dwSize = (UInt32)Marshal.SizeOf(typeof(BLUETOOTH_FIND_RADIO_PARAM));
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LE_SCAN_REQUEST
        {
            internal uint unknown1;
            internal int scanType;
            internal uint unknown2;
            internal ushort scanInterval;
            internal ushort scanWindow;
            internal uint unknown3_0;
            internal uint unknown3_1;
        }

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode,
            ref LE_SCAN_REQUEST lpInBuffer, uint nInBufferSize,
            IntPtr lpOutBuffer, uint nOutBufferSize,
            out uint lpBytesReturned, IntPtr lpOverlapped);
    }
}