using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Darp.NrfBleDriver.Nrf;

public struct SerialPortDescription
{
    private const int MaxLength = 512;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxLength)]
    public string Port;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxLength)]
    public string Manufacturer;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxLength)]
    public string SerialNumber;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxLength)]
    public string PnpId;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxLength)]
    public string LocationId;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxLength)]
    public string VendorId;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxLength)]
    public string ProductId;
}

internal class SdRpc
{
    static SdRpc()
    {
        NativeLibrary.SetDllImportResolver(typeof(SdRpc).Assembly, InteropHelper.ImportResolver);
    }

    /// <summary>
    /// Enumerate available serial ports.
    /// </summary>
    /// <param name="serialPortDescriptions">The array of serial port descriptors to be filled in.</param>
    /// <param name="size">The size of the array. The number of ports found is stored here.</param>
    /// <returns>
    /// NRF_SUCCESS         The serial ports were enumerated successfully.
    /// NRF_ERROR_NULL      size was a null pointer.
    /// NRF_ERROR_DATA_SIZE The size of the array was not large enough to keep all descriptors found.
    ///                     No descriptors where copied. Call again with an larger array.
    /// NRF_ERROR           There was an error enumerating the serial ports.
    /// </returns>
    /// 
    [DllImport(InteropHelper.Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern NrfError sd_rpc_serial_port_enum([MaybeNull]out SerialPortDescription[] serialPortDescriptions, ref uint size);

    /**@brief Create a new serial physical layer.
 *
 * @param[in]  port_name  The serial port name.
 * @param[in]  baud_rate  The serial port speed.
 * @param[in]  flow_control  The flow control scheme to use.
 * @param[in]  parity  The parity scheme to use.
 *
 * @retval The physical layer or NULL.
 */
    [DllImport(InteropHelper.Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr sd_rpc_physical_layer_create_uart(string portName, uint baudRate, SdRpcFlowControlT flowControl, SdRpcParityT parity);

    /**@brief Create a new data link layer.
 *
 * @param[in]  physical_layer  The physical layer to use with this data link layer.
 * @param[in]  response_timeout  Response timeout of the data link layer.
 *
 * @retval The data link layer or NULL.
 */
    [DllImport(InteropHelper.Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr sd_rpc_data_link_layer_create_bt_three_wire(IntPtr physicalLayer, uint retransmissionInterval);

    /**@brief Create a new transport layer.
 *
 * @param[in]  data_link_layer  The data linkk layer to use with this transport.
 * @param[in]  response_timeout  Response timeout.
 *
 * @retval The transport layer or NULL.
 */
    [DllImport(InteropHelper.Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr sd_rpc_transport_layer_create(IntPtr dataLinkLayer, uint responseTimeout);

    /**@brief Create a new transport adapter.
 *
 * @param[in]  transport_layer  The transport layer to use with this adapter.
 *
 * @retval The adapter or NULL.
 */
    [DllImport(InteropHelper.Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr sd_rpc_adapter_create(IntPtr transportLayer);

    public delegate void sd_rpc_status_handler_t(IntPtr adapter, SdRpcAppStatusT code, string message);
    public delegate void sd_rpc_evt_handler_t(IntPtr adapter, SdRpcAppStatusT code, string message);

    /**@brief Initialize the SoftDevice RPC module.
 *
 * @note This function must be called prior to the sd_ble_* API commands.
 *       The serial port will be attempted opened with the configured serial port settings.
 *
 * @param[in]  adapter  The transport adapter.
 * @param[in]  status_handler  The status handler callback.
 * @param[in]  evt_handler  The event handler callback.
 * @param[in]  log_handler  The log handler callback.
 *
 * @retval NRF_SUCCESS  The module was opened successfully.
 * @retval NRF_ERROR    There was an error opening the module.
 */
    //[DllImport(InteropHelper.Lib, CallingConvention = CallingConvention.StdCall)]
    //public static extern NrfError sd_rpc_open(IntPtr adapter, sd_rpc_status_handler_t status_handler, sd_rpc_evt_handler_t event_handler, sd_rpc_log_handler_t log_handler);

    /**@brief Close the SoftDevice RPC module.
 *
 * @note This function will close the serial port and release allocated resources.
 *
 * @param[in]  adapter  The transport adapter.
 *
 * @retval NRF_SUCCESS  The module was closed successfully.
 * @retval NRF_ERROR    There was an error closing the module.
 */
    [DllImport(InteropHelper.Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern NrfError sd_rpc_close(IntPtr adapter);

    /**@brief Set the lowest log level for messages to be logged to handler.
 *        Default log handler severity filter is LOG_INFO.
 *
 * @param[in]  adapter  The transport adapter.
 * @param[in]  severity_filter  The lowest severity level messages should be logged.
 *
 * @retval NRF_SUCCESS              severity_filter is valid.
 * @retval NRF_ERROR_INVALID_PARAM  severity_filter is not one of the valid enum values
 *                                  in app_log_severity_t
 */
    [DllImport(InteropHelper.Lib, CallingConvention = CallingConvention.StdCall)]
    public static extern NrfError sd_rpc_log_handler_severity_filter_set(IntPtr adapter, SdRpcLogSeverityT severityFilter);
}