﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Ble.Config;
using Ble.Gatt;
using Ble.Nrf.Nrf;
using Ble.Uuid;
using Microsoft.Extensions.Logging;
using NrfBleDriver;

namespace Ble.Nrf.Gatt;

public sealed class NrfConnectedPeripheral : IConnectedPeripheral
{
    private readonly NrfAdapter _adapter;
    private readonly ILogger? _logger;
    private readonly IDictionary<Guid, IConnectedGattService> _serviceDictionary = new Dictionary<Guid, IConnectedGattService>();
    public NrfConnectedPeripheral(NrfAdapter adapter, ushort connectionHandle, ILogger? logger)
    {
        _adapter = adapter;
        _logger = logger;
        ConnectionHandle = connectionHandle;
    }

    public async Task<bool> DiscoverServicesAsync(Guid? serviceUuid, CancellationToken cancellationToken)
    {
        _logger?.LogDebug("Discovering primary services ...");
        ushort startHandle = 0x01;
        BleUuidT? uuid = serviceUuid?.ToBleUuid();
        while (true)
        {
            if (ble_gattc.SdBleGattcPrimaryServicesDiscover(_adapter.AdapterHandle, ConnectionHandle, startHandle, uuid)
                .IsFailed(_logger, $"Primary Service Discovery failed from {startHandle}"))
            {
                return false;
            }
            BleGattcEvtT? evt = await _adapter.PrimaryServiceDiscoveryResponses.FirstOrDefaultAsync().ToTask(cancellationToken);
            if (evt is null)
            {
                _logger?.LogWarning("Primary service was cancelled");
                return false;
            }

            if (((uint)evt.ErrorHandle).IsFailed(_logger, "Primary service discovery failed"))
                return false;
            BleGattcEvtPrimSrvcDiscRspT? response = evt.@params.PrimSrvcDiscRsp;
            if (response.Services.Length == 0)
                break;
            foreach (BleGattcServiceT bleGattcServiceT in response.Services)
            {
                var gattService = new NrfGattService(_adapter, this, bleGattcServiceT, _logger);
                _serviceDictionary[gattService.Uuid] = gattService;
            }
            ushort endHandle = response.Services[^1].HandleRange.EndHandle;
            if (endHandle == 0xFFFF)
                break;
            startHandle = (ushort)(response.Services[^1].HandleRange.EndHandle + 1);
        }
        return true;
    }

    public ushort ConnectionHandle { get; }
    public ICollection<IConnectedGattService> Services => _serviceDictionary.Values;
    public IConnectedGattService this[Service service] => _serviceDictionary[service.Uuid];
    public IConnectedGattService this[Guid guid] => _serviceDictionary[guid];
    public IConnectedGattService this[GattUuid guid] => _serviceDictionary[guid.ToBleGuid()];
    public bool ContainsService(Guid guid) => _serviceDictionary.ContainsKey(guid);
    public bool ContainsService(GattUuid guid) => _serviceDictionary.ContainsKey(guid);

    public IConnectedGattService Select(Service service) => _serviceDictionary[service.Uuid];

    public void Dispose()
    {
        _adapter.Disconnect(this);
    }
}