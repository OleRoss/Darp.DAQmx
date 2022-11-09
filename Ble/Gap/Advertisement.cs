using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ble.Configuration;
using Ble.Connection;

namespace Ble.Gap;

public record Advertisement(IBleAdapter Adapter,
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
    IReadOnlyDictionary<CompanyId, byte[]> ManufacturerDatas,
    AdvertisementFlags AdvertisementFlags,
    Guid[] ServiceUuids) : IAdvertisement
{
    internal IBleAdapter Adapter { get; init; } = Adapter;
    public string DeviceAddressString => $"{DeviceAddress:X12}";

    public async Task<IConnection?> ConnectAsync(Peripheral peripheral, CancellationToken cancellationToken = default)
    {
        return await Adapter.ConnectAsync(DeviceAddress, peripheral, cancellationToken);
    }

    public async Task<IConnection?> ConnectAsync(CancellationToken cancellationToken = default)
    {
        return await Adapter.ConnectAsync(DeviceAddress, cancellationToken);
    }
}