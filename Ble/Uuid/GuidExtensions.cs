﻿using System;
using System.Collections.Generic;
using Ble.Utils;

namespace Ble.Uuid;

public static class GuidExtensions
{
    public static Guid ToBleGuid(this ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length == 16)
            return new Guid(bytes);
        Span<byte> baseUuidBytes = stackalloc byte[] { 0,0,0,0,0,0,0,16,128,0,0,128,95,155,52,251 };
        switch (bytes.Length)
        {
            case 2:
                baseUuidBytes[0] = bytes[0];
                baseUuidBytes[1] = bytes[1];
                break;
            case 4:
                baseUuidBytes[0] = bytes[0];
                baseUuidBytes[1] = bytes[1];
                baseUuidBytes[2] = bytes[2];
                baseUuidBytes[3] = bytes[3];
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(bytes), $"Provided invalid number of bytes for guid: {bytes.Length}");
        }
        return new Guid(baseUuidBytes);
    }

    public static Guid ToBleGuid(this byte[] bytes) => new ReadOnlySpan<byte>(bytes).ToBleGuid();

    public static Guid ToBleGuid(this string bluetoothUuid)
    {
        Span<byte> bytes = stackalloc byte[bluetoothUuid.Length >> 1];
        bluetoothUuid.ToByteArray(bytes);
        if (bytes.Length % 2 != 0)
            throw new ArgumentOutOfRangeException(nameof(bluetoothUuid), $"even number of bytes necessary, got {bytes.Length}");
        for (var i = 0; i < bytes.Length / 2; i++)
            (bytes[^(i + 1)], bytes[i]) = (bytes[i], bytes[^(i + 1)]);
        return ToBleGuid(bytes);
    }

    public static Guid ToBleGuid(this GattUuid guid)
    {
        ReadOnlySpan<byte> bytes = stackalloc byte[] { (byte)guid, (byte)((ushort)guid >> 8)};
        return bytes.ToBleGuid();
    }
    public static bool Contains(this IEnumerable<Guid> guids, ushort uuid)
    {
        foreach (Guid guid in guids)
        {
            unsafe
            {
                var pGuid = (ushort*)&guid;
                if (*pGuid == uuid)
                    return true;
            }
        }
        return false;
    }

    public static GattUuid ToDefaultUuid(this Guid guid)
    {
        unsafe
        {
            ushort value = *(ushort*)&guid;
            return (GattUuid)value;
        }
    }

    public static bool ContainsKey<T>(this IDictionary<Guid, T> dict, GattUuid uuid)
    {
        foreach ((Guid key, _) in dict)
        {
            if (key.ToDefaultUuid() == uuid)
                return true;
        }
        return false;
    }

    public static T Get<T>(this IDictionary<Guid, T> dict, GattUuid uuid)
    {
        foreach ((Guid key, T? value) in dict)
        {
            if (key.ToDefaultUuid() == uuid)
                return value;
        }
        throw new KeyNotFoundException($"Default guid {uuid} is not contained in characteristic");
    }
    public static bool Contains(this IEnumerable<Guid> guids, GattUuid uuid) => guids.Contains((ushort)uuid);

}