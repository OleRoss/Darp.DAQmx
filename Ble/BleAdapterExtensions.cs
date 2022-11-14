﻿using System.Threading;
using Ble.Gap;

namespace Ble;

public static class BleAdapterExtensions
{
    public static IAdvertisementScanner Scan(this IBleAdapter adapter,
        ScanningMode mode,
        float scanIntervalMs,
        float scanWindowMs,
        CancellationToken cancellationToken = default)
    {
        return adapter.Scanner()
            .SetScanMode(mode)
            .SetScanParameters(scanIntervalMs, scanWindowMs)
            .Start(cancellationToken);
    }
}