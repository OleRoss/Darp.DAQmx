using System;
using System.Threading;
using System.Threading.Tasks;
using Ble.Config;
using Ble.Gap;
using Ble.Gatt;

namespace Ble;

public interface IBleAdapter : IDisposable
{
    Task<IConnectedPeripheral?> ConnectAsync(ulong bluetoothId, Configuration? peripheral, CancellationToken cancellationToken = default);

    Task<IConnectedPeripheral?> ConnectAsync(ulong bluetoothId, CancellationToken cancellationToken) =>
        ConnectAsync(bluetoothId, null, cancellationToken);
    void Clear();
    IAdvertisementScanner Scanner();
}