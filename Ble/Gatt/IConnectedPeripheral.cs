using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ble.Config;
using Ble.Uuid;

namespace Ble.Gatt;

public interface IConnectedPeripheral : IDisposable
{
    Task<bool> DiscoverServicesAsync(Guid? serviceUuid, CancellationToken cancellationToken = default);

    ICollection<IConnectedGattService> Services { get; }
    IConnectedGattService this[Service service] { get; }
    IConnectedGattService this[Guid guid] { get; }
    IConnectedGattService this[GattUuid guid] { get; }
    bool ContainsService(Guid guid);
    bool ContainsService(GattUuid guid);
    IConnectedGattService Select(Service service);
}

public static class Ex
{
    public static async Task<bool> DiscoverServicesAsync(this IConnectedPeripheral peripheral,
        Configuration? configuration,
        CancellationToken cancellationToken)
    {
        if (configuration is null || !configuration.Services.Any())
        {
            bool success = await peripheral.DiscoverServicesAsync(serviceUuid: null, cancellationToken);
            if (!success)
            {
                // TODO _logger?.LogWarning("Service discovery with errors. Contents might be incomplete");
            }
        }
        else
        {
            foreach (Service peripheralService in configuration.Services)
            {
                bool success = await peripheral.DiscoverServicesAsync(peripheralService.Uuid, cancellationToken);
                if (!success)
                    return false;
            }
        }
        foreach (IConnectedGattService gattService in peripheral.Services)
        {
            if (!await gattService.DiscoverCharacteristics(cancellationToken))
            {
                // TODO _logger?.LogWarning("Characteristic discovery with error - might be incomplete");
            }
        }
        if (configuration is null) return true;
        foreach (Service gattService in configuration.Services)
        {
            if (!peripheral.ContainsService(gattService.Uuid))
            {
                {
                    // TODO _logger?.LogWarning("Missing service with uuid {Uuid}, available are {Available}",
                    // gattService.Uuid,
                    // peripheral.Services.Select(x => x.Uuid).ToArray());
                }
                return false;
            }
            IConnectedGattService connectedService = peripheral[gattService.Uuid];
            bool allPresent = gattService.Characteristics.All(gattChar =>
            {
                if (!connectedService.ContainsCharacteristic(gattChar.Uuid))
                {
                    {
                        // TODO _logger?.LogWarning("Missing characteristic with uuid {Uuid}, available are {Available}",
                        // gattChar.Uuid,
                        // connectedService.Characteristics.Select(connChar => connChar.Uuid).ToArray());
                        
                    }
                    return false;
                }
                IConnectedGattCharacteristic connChar = connectedService[gattChar.Uuid];
                if ((gattChar.Property & connChar.Property) != gattChar.Property)
                {
                    {
                        // TODO _logger?.LogWarning("Invalid properties for characteristic with {Uuid}: {ExpectedProp} available are {AvailableProp}",
                        // gattChar.Uuid,
                        // gattChar.Property,
                        // connChar.Property);
                    }
                    return false;
                }
                return true;
            });
            if (!allPresent)
                return false;
        }
        return true;
    }
}