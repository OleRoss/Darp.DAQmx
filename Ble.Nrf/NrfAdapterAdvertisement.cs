using System;
using System.Collections.Generic;
using System.Text;
using Ble.Gap;
using Ble.Nrf.Nrf;
using Ble.Uuid;
using NrfBleDriver;

namespace Ble.Nrf;


public static class Extensions
{
    public static AdvertisementType GetAddressType(this byte type)
    {
        /*
        switch (type.Directed)
        {
            case > 0 when type.Connectable > 0:
                return AdvertisementType.ConnectableDirected;
            case 0 when type.Connectable > 0:
                return AdvertisementType.ConnectableUndirected;
            case 0 when type.Scannable > 0:
                return AdvertisementType.ScannableUndirected;
            case 0 when type.Connectable == 0:
                return AdvertisementType.NonConnectableUndirected;
        }
        if (type.ScanResponse > 0)
            return AdvertisementType.ScanResponse;
        if (type.ExtendedPdu > 0)
            return AdvertisementType.Extended;*/
        return 0;
    }

    public static ulong ToAddress(this BleGapAddrT address) => address.Addr.ToAddress();

    public static string ToAddressString(this byte[] addressBytes)
    {
        var builder = new StringBuilder();
        for (int i = addressBytes.Length - 1; i >= 0; --i) builder.Append($"{addressBytes[i]:X2}");
        return builder.ToString();
    }

    public static ulong ToAddress(this byte[] addressBytes) => Convert.ToUInt64(addressBytes.ToAddressString(), 16);

    public static BleUuidT ToBleUuid(this Guid guid)
    {
        byte[] x = guid.ToByteArray();
        var value = BitConverter.ToUInt16(x.AsSpan()[..2]);
        return new BleUuidT
        {
            Type = (byte)BLE_UUID_TYPES.BLE_UUID_TYPE_BLE,
            Uuid = value
        };
    }

    public static GattUuid ToDefaultUuid(this Guid guid)
    {
        unsafe
        {
            ushort value = *(ushort*)&guid;
            return (GattUuid)value;
        }
    }

    public static AddressType ToAddressType(this BleGapAddrT address)
    {
        return address.AddrType switch
        {
            BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_PUBLIC => AddressType.Public,
            BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_RANDOM_STATIC
                or BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_RANDOM_PRIVATE_RESOLVABLE
                or BLE_GAP_ADDR_TYPES.BLE_GAP_ADDR_TYPE_RANDOM_PRIVATE_NON_RESOLVABLE => AddressType.Random,
            _ => AddressType.Unspecified
        };
    }

    public static unsafe IReadOnlyList<(SectionType, byte[])> ParseAdvertisementReports(this BleGapEvtAdvReportT advData)
    {
        var list = new List<(SectionType, byte[])>();
        byte  index = 0;
        byte[] pData = advData.Data;
        
        while (index < advData.Data.Length)
        {
            byte fieldLength = pData[index];
            if (fieldLength == 0)
                break;
            var fieldType   = (SectionType)pData[index + 1];

            var bytes = new byte[fieldLength - 1];
            for (var i = 0; i < fieldLength - 1; i++) bytes[i] = pData[index + 2 + i];

            list.Add((fieldType, bytes));
            index += (byte)(fieldLength + 1);
        }
        return list;
    }

    public static AdvertisementFlags GetFlags(this IEnumerable<(SectionType, byte[])> dataSections)
    {
        foreach ((SectionType sectionType, byte[]? bytes) in dataSections)
        {
            if (sectionType != SectionType.Flags)
                continue;
            return (AdvertisementFlags) bytes[0];
        }
        return AdvertisementFlags.None;
    }

    public static string GetName(this IEnumerable<(SectionType, byte[])> dataSections)
    {
        foreach ((SectionType sectionType, byte[]? bytes) in dataSections)
        {
            if (sectionType == SectionType.CompleteLocalName)
                return Encoding.ASCII.GetString(bytes);
            if (sectionType == SectionType.ShortenedLocalName)
                return Encoding.ASCII.GetString(bytes);
        }
        return string.Empty;
    }

    public static IReadOnlyDictionary<CompanyUuid, byte[]> GetManufactureSpecifics(
        this IEnumerable<(SectionType, byte[])> dataSections)
    {
        var manufactures = new Dictionary<CompanyUuid, byte[]>();
        foreach ((SectionType sectionType, byte[]? bytes) in dataSections)
        {
            if (sectionType != SectionType.ManufacturerSpecificData)
                continue;
            manufactures[(CompanyUuid)BitConverter.ToUInt16(bytes.AsSpan()[..2])] = bytes[2..];
        }
        return manufactures;
    }

    public static Guid[] GetServiceGuids(this IEnumerable<(SectionType, byte[])> dataSections)
    {
        var serviceGuids = new List<Guid>();
        foreach ((SectionType sectionType, byte[]? bytes) in dataSections)
        {
            if (sectionType is SectionType.CompleteService16BitUuids
                or SectionType.CompleteService32BitUuids
                or SectionType.CompleteService128BitUuids
                or SectionType.IncompleteService16BitUuids
                or SectionType.IncompleteService32BitUuids
                or SectionType.IncompleteService128BitUuids)
            {
                serviceGuids.Add(bytes.ToBleGuid());
            }
        }
        return serviceGuids.ToArray();
    }
}