using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Ble.Config;
using Ble.Gap;
using Ble.Gatt;
using Ble.Nrf.Gap;
using Ble.Nrf.Gatt;
using Ble.Nrf.Nrf;
using Microsoft.Extensions.Logging;
using NrfBleDriver;

namespace Ble.Nrf;

public sealed class NrfAdapter : NrfAdapterBase, IBleAdapter
{
    private readonly NrfAdvertisementScanner _scanner;
    private bool _connectionInProgress;
    public NrfAdapter(string serialPort, uint baudRate = 1000000, ILogger? logger = null)
        : base(serialPort, baudRate, 150, logger)
    {
        _scanner = new NrfAdvertisementScanner(this, logger);
    }

    public async Task<IConnectedPeripheral?> ConnectAsync(ulong bluetoothId, Configuration? configuration, CancellationToken cancellationToken = default)
    {
        // Wait until running connections are finished
        if (_connectionInProgress)
            _logger?.LogWarning("Other connection still in progress ... waiting");
        while (_connectionInProgress)
        {
            bool cancelled = await Task.Delay(10, cancellationToken).WithoutThrowing();
            if (cancelled) return null;
        }
        _logger?.LogInformation("Attempting connection with {BluetoothId:X}", bluetoothId);
        _scanner.Stop();
        _connectionInProgress = true;
        try
        {
            // Determines minimum connection interval in milliseconds
            const ushort minConnectionInterval = 7500 / 1250;
            // Determines maximum connection interval in milliseconds
            const ushort maxConnectionInterval = 7500 / 1250;
            // Slave Latency in number of connection events
            const ushort slaveLatency = 0;
            const ushort timeoutMs = 4000;
            // Determines supervision time-out in units of 10 milliseconds
            const ushort connectionSupervisionTimeout = timeoutMs / 10;
            const ushort scanInterval = 0x00A0;
            const ushort scanWindow = 0x0050;

            var addr = new BleGapAddrT
            {
                Addr = BitConverter.GetBytes(bluetoothId)[..6],
                AddrType = BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_PUBLIC,
                AddrIdPeer = 0
            };
            var mConnectionParam = new BleGapConnParamsT
            {
                MinConnInterval = minConnectionInterval,
                MaxConnInterval = maxConnectionInterval,
                SlaveLatency = slaveLatency,
                ConnSupTimeout = connectionSupervisionTimeout
            };
            var scanParam = new BleGapScanParamsT
            {
                Active = 0,
                Interval = scanInterval,
                Window = scanWindow,
                Timeout = 0,
                AdvDirReport = 0,
                UseWhitelist = 0
            };
            if (ble_gap.SdBleGapConnect(AdapterHandle, addr, scanParam, mConnectionParam, ConfigId)
                .IsFailed(_logger, "Ble gap connection failed"))
            {
                return null;
            }

            _logger?.LogTrace("Connecting ...");
            BleGapEvtT? evt = await GapConnectResponses.FirstOrDefaultAsync().ToTask(cancellationToken);
            if (evt == null)
            {
                _logger?.LogWarning("Connection timed out");
                return null;
            }

            var connection = new NrfConnectedPeripheral(this, evt.ConnHandle, _logger);
            Connections.Add(connection);
            
            bool success = await connection.DiscoverServicesAsync(configuration, cancellationToken);
            if (success)
            {
                _logger?.LogDebug("Successfully connected");
                return connection;
            }

            _logger?.LogWarning("Service discovery with failed. Disconnecting from peripheral...");
            connection.Dispose();
            return null;
        }
        finally
        {
            _connectionInProgress = false;
        }
    }

    public bool Disconnect(IConnectedPeripheral connectedPeripheral)
    {
        if (connectedPeripheral is not NrfConnectedPeripheral nrfPeripheral)
            throw new ArgumentException("Cannot disconnect peripheral, which is not of type nrf peripheral");
        _logger?.LogTrace("Disconnecting connection 0x{Connection:X}", nrfPeripheral.ConnectionHandle);
        Connections.Remove(nrfPeripheral);
        return ble_gap.SdBleGapDisconnect(AdapterHandle, nrfPeripheral.ConnectionHandle,
                (byte)BLE_HCI_STATUS_CODES.BLE_HCI_REMOTE_USER_TERMINATED_CONNECTION)
            .IsFailed(_logger, "Disconnection failed");
    }

    public void DisconnectAll()
    {
        _logger?.LogTrace("Clearing {ConnectionCount} connected devices", Connections.Count);
        for (int index = Connections.Count - 1; index >= 0; index--)
        {
            Disconnect(Connections[index]);
        }
        Connections.Clear();
    }

    public IAdvertisementScanner Scanner() => _scanner;

    public void Dispose()
    {
        _logger?.LogDebug("Disposing of nrf bluetooth adapter");
        DisconnectAll();
        sd_rpc.SdRpcClose(AdapterHandle)
            .IsFailed(_logger, "Failed to close nRF BLE Driver");
        AdapterHandle.Dispose();
    }
}



