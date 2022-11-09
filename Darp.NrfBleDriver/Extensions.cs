﻿namespace Darp.NrfBleDriver;

public static class Extensions
{
    public static byte[] ToByteArray(this string hex)
    {
        if (hex.Length % 2 == 1)
            throw new Exception("The binary key cannot have an odd number of digits");
        var arr = new byte[hex.Length >> 1];
        for (var i = 0; i < hex.Length >> 1; ++i)
            arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + GetHexVal(hex[(i << 1) + 1]));
        return arr;
    }

    private static int GetHexVal(char hex)
    {
        int val = hex;
        //For uppercase A-F letters:
        //return val - (val < 58 ? 48 : 55);
        //For lowercase a-f letters:
        //return val - (val < 58 ? 48 : 87);
        //Or the two combined, but a bit slower:
        return val - (val < 58 ? 48 : val < 97 ? 55 : 87);
    }
}