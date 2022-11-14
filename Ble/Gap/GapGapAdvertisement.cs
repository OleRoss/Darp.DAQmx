using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ble.Config;
using Ble.Gatt;
using Ble.Uuid;

namespace Ble.Gap;

public record GapGapAdvertisement(IBleAdapter Adapter,
    DateTimeOffset Timestamp,
    AdvertisementType Type,
    ulong DeviceAddress,
    bool IsAnonymous,
    bool IsConnectable,
    bool IsDirected,
    bool IsScanResponse,
    bool IsScannable,
    AddressType AddressType,
    short Rssi,
    short? TransmitPowerLevel,
    string Name,
    IReadOnlyList<(SectionType, byte[])> DataSections,
    IReadOnlyDictionary<CompanyUuid, byte[]> ManufacturerDatas,
    AdvertisementFlags AdvertisementFlags,
    Guid[] Services) : IGapAdvertisement
{
    internal IBleAdapter Adapter { get; init; } = Adapter;
    public string DeviceAddressString => $"{DeviceAddress:X12}";

    public async Task<IConnectedPeripheral?> ConnectAsync(Configuration configuration, CancellationToken cancellationToken = default)
    {
        return await Adapter.ConnectAsync(DeviceAddress, configuration, cancellationToken);
    }

    public async Task<IConnectedPeripheral?> ConnectAsync(CancellationToken cancellationToken = default)
    {
        return await Adapter.ConnectAsync(DeviceAddress, cancellationToken);
    }
}