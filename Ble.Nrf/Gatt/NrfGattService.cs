using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Ble.Configuration;
using Ble.Connection;
using Ble.Nrf.Nrf;
using Ble.Utils;
using Microsoft.Extensions.Logging;
using NrfBleDriver;

namespace Ble.Nrf.Gatt;

public class NrfGattService : IConnectedGattService
{
    private readonly NrfAdapter _adapter;
    private readonly NrfConnection _connection;
    private readonly BleGattcServiceT _gattcService;
    private readonly ILogger? _logger;

    private readonly IDictionary<Guid, IConnectedGattCharacteristic>
        _characteristicDictionary = new Dictionary<Guid, IConnectedGattCharacteristic>();

    public NrfGattService(NrfAdapter adapter, NrfConnection connection, BleGattcServiceT gattcService, ILogger? logger)
    {
        _adapter = adapter;
        _connection = connection;
        _gattcService = gattcService;
        _logger = logger;
        Uuid = BitConverter.GetBytes(gattcService.Uuid.Uuid).ToBleGuid();
    }

    public Guid Uuid { get; }
    public ICollection<IConnectedGattCharacteristic> Characteristics => _characteristicDictionary.Values;

    public IConnectedGattCharacteristic this[GattCharacteristic characteristic] => _characteristicDictionary[characteristic.Uuid];

    public IConnectedGattCharacteristic this[Guid characteristicGuid] => _characteristicDictionary[characteristicGuid];

    public bool ContainsCharacteristic(Guid guid) => _characteristicDictionary.ContainsKey(guid);

    public bool ContainsCharacteristic(DefaultUuid guid) => _characteristicDictionary.Keys
        .Any(x => x.ToDefaultUuid() == guid);

    public ConnectedGattCharacteristic<TCharacteristic> Select<TCharacteristic>(TCharacteristic characteristic)
        where TCharacteristic : GattCharacteristic
    {
        IConnectedGattCharacteristic connChar = this[characteristic.Uuid];
        if (!characteristic.IsValid(connChar))
            throw new ArgumentException($"Characteristic {characteristic} is not valid for service", nameof(characteristic));
        return new ConnectedGattCharacteristic<TCharacteristic>(characteristic, connChar);
    }

    public override string ToString() => $"{nameof(NrfGattService)} (Uuid={_gattcService.Uuid.Uuid:X}, NumChars={Characteristics.Count}, StartHandle={_gattcService.HandleRange.StartHandle}, EndHandle={_gattcService.HandleRange.EndHandle})";

    public async Task<bool> DiscoverCharacteristics(CancellationToken cancellationToken)
    {
        _logger?.LogDebug("Discovering characteristics ...");
        var charHandleRange = new BleGattcHandleRangeT
        {
            StartHandle = _gattcService.HandleRange.StartHandle,
            EndHandle = _gattcService.HandleRange.EndHandle
        };
        var characteristics = new List<NrfGattCharacteristic>();
        while (true)
        {
            if (charHandleRange.StartHandle == charHandleRange.EndHandle)
                break;
            if (ble_gattc.SdBleGattcCharacteristicsDiscover(_adapter.AdapterHandle, _connection.ConnectionHandle, charHandleRange)
                .IsFailed(_logger, $"Characteristic discovery failed from {charHandleRange.StartHandle} to {charHandleRange.EndHandle}"))
            {
                return false;
            }
            BleGattcEvtT? evt = await _adapter.CharacteristicDiscoveryResponses.FirstOrDefaultAsync().ToTask(cancellationToken);
            if (evt is null)
            {
                _logger?.LogWarning("Characteristic discovery was cancelled");
                return false;
            }
            //if (((uint)evt.ErrorHandle).IsFailed(_logger, "Characteristic discovery failed"))
            //    return false;
            var status = (BLE_GATT_STATUS_CODES)evt.GattStatus;
            if (status is BLE_GATT_STATUS_CODES.BLE_GATT_STATUS_ATTERR_ATTRIBUTE_NOT_FOUND)
                break;
            if (status is not BLE_GATT_STATUS_CODES.BLE_GATT_STATUS_SUCCESS)
            {
                _logger?.LogWarning("Unexpected gatt status {GattStatus}", status);
                return false;
            }
            BleGattcEvtCharDiscRspT? response = evt.@params.CharDiscRsp;
            if (response.Chars.Length == 0)
                break;
            foreach (BleGattcCharT gattcCharacteristic in response.Chars)
            {
                var gattCharacteristic = new NrfGattCharacteristic(_adapter,
                    this,
                    gattcCharacteristic,
                    gattcCharacteristic.HandleDecl,
                    charHandleRange.EndHandle,
                    _logger);
                if (characteristics.Count > 0)
                    characteristics[^1].EndHandle = (ushort)(gattcCharacteristic.HandleDecl - 1);
                characteristics.Add(gattCharacteristic);
            }
            charHandleRange.StartHandle = (ushort)(response.Chars[^1].HandleDecl + 1);
        }
        foreach (NrfGattCharacteristic characteristic in characteristics)
        {
            var descHandleRange = new BleGattcHandleRangeT
            {
                StartHandle = characteristic.StartHandle,
                EndHandle = characteristic.EndHandle
            };
            while (true)
            {
                if (ble_gattc.SdBleGattcDescriptorsDiscover(_adapter.AdapterHandle, _connection.ConnectionHandle, descHandleRange)
                    .IsFailed(_logger, $"Descriptor discovery failed from {descHandleRange.StartHandle} to {descHandleRange.EndHandle}"))
                {
                    return false;
                }
                BleGattcEvtT? evt = await _adapter.DescriptorDiscoveryResponses.FirstOrDefaultAsync().ToTask(cancellationToken);
                if (evt is null)
                {
                    _logger?.LogWarning("Descriptor discovery was cancelled");
                    return false;
                }
                //if (((uint)evt.ErrorHandle).IsFailed(_logger, "Descriptor discovery failed"))
                //    return false;
                var status = (BLE_GATT_STATUS_CODES)evt.GattStatus;
                if (status is BLE_GATT_STATUS_CODES.BLE_GATT_STATUS_ATTERR_ATTRIBUTE_NOT_FOUND)
                    break;
                if (status is not BLE_GATT_STATUS_CODES.BLE_GATT_STATUS_SUCCESS)
                {
                    _logger?.LogWarning("Unexpected gatt status {GattStatus} during descriptor discovery", status);
                    return false;
                }
                BleGattcEvtDescDiscRspT? response = evt.@params.DescDiscRsp;
                if (response.Descs.Length == 0)
                    break;
                foreach (BleGattcDescT gattcDesc in response.Descs)
                {
                    characteristic.AddDescriptor(new NrfGattDescriptor(_adapter, _connection, gattcDesc, _logger));
                }
                if (response.Descs[^1].Handle == characteristic.EndHandle)
                    break;
                descHandleRange.StartHandle = (ushort)(response.Descs[^1].Handle + 1);
            }
        }
        foreach (NrfGattCharacteristic nrfGattCharacteristic in characteristics)
            _characteristicDictionary[nrfGattCharacteristic.Uuid] = nrfGattCharacteristic;
        return true;
    }
}