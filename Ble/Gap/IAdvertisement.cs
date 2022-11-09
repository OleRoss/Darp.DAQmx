using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ble.Configuration;
using Ble.Connection;

namespace Ble.Gap;

public interface IAdvertisement
{
    DateTimeOffset Timestamp { get; }
    AdvertisementType Type { get; }
    ulong DeviceAddress { get; }
    string DeviceAddressString { get; }
    bool IsAnonymous { get; }
    bool IsConnectable { get; }
    bool IsDirected { get; }
    bool IsScanResponse { get; }
    bool IsScannable { get; }
    AddressType AddressType { get; }
    short Rssi { get; }
    short? TransmitPowerLevel { get; }

    string Name { get; }
    IReadOnlyList<(SectionType, byte[])> DataSections { get; }
    IReadOnlyDictionary<CompanyId, byte[]> ManufacturerDatas { get; }
    AdvertisementFlags AdvertisementFlags { get; }
    Guid[] ServiceUuids { get; }

    Task<IConnection?> ConnectAsync(Peripheral peripheral, CancellationToken cancellationToken = default);
    Task<IConnection?> ConnectAsync(CancellationToken cancellationToken = default);
}