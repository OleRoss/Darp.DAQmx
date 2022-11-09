using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ble.Configuration;
using Ble.Connection;

namespace Ble.Gap;

public class Advertisement<TData> : IAdvertisement
    where TData : struct
{
    private readonly IAdvertisement _advertisement;

    public Advertisement(IAdvertisement advertisement, TData data)
    {
        _advertisement = advertisement;
        ManufacturerData = data;
    }

    public DateTimeOffset Timestamp => _advertisement.Timestamp;
    public AdvertisementType Type => _advertisement.Type;
    public ulong DeviceAddress => _advertisement.DeviceAddress;
    public string DeviceAddressString => _advertisement.DeviceAddressString;
    public bool IsAnonymous => _advertisement.IsAnonymous;
    public bool IsConnectable => _advertisement.IsConnectable;
    public bool IsDirected => _advertisement.IsDirected;
    public bool IsScanResponse => _advertisement.IsScanResponse;
    public bool IsScannable => _advertisement.IsScannable;
    public AddressType AddressType => _advertisement.AddressType;
    public short Rssi => _advertisement.Rssi;
    public short? TransmitPowerLevel => _advertisement.TransmitPowerLevel;
    public string Name => _advertisement.Name;
    public IReadOnlyList<(SectionType, byte[])> DataSections => _advertisement.DataSections;
    IReadOnlyDictionary<CompanyId, byte[]> IAdvertisement.ManufacturerDatas => _advertisement.ManufacturerDatas;
    public TData ManufacturerData { get; }
    public AdvertisementFlags AdvertisementFlags => _advertisement.AdvertisementFlags;
    public Guid[] ServiceUuids => _advertisement.ServiceUuids;

    public Task<IConnection?> ConnectAsync(Peripheral peripheral, CancellationToken cancellationToken) =>
        _advertisement.ConnectAsync(peripheral, cancellationToken);
    public Task<IConnection?> ConnectAsync(CancellationToken cancellationToken = default) =>
        _advertisement.ConnectAsync(cancellationToken);
}