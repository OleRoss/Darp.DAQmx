using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ble.Config;
using Ble.Gatt;
using Ble.Uuid;

namespace Ble.Gap;

public interface IGapAdvertisement
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
    IReadOnlyDictionary<CompanyUuid, byte[]> ManufacturerDatas { get; }
    AdvertisementFlags AdvertisementFlags { get; }
    Guid[] Services { get; }

    Task<IConnectedPeripheral?> ConnectAsync(Configuration configuration, CancellationToken cancellationToken = default);
    Task<IConnectedPeripheral?> ConnectAsync(CancellationToken cancellationToken = default);
}