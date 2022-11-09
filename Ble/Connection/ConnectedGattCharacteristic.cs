using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Ble.Configuration;
using Ble.Utils;

namespace Ble.Connection;

public interface IConnectedGattCharacteristic
{
    Guid Uuid { get; }
    ICollection<IConnectedGattDescriptor> Descriptors { get; }
    IConnectedGattDescriptor this[Guid guid] { get; }
    IConnectedGattDescriptor this[DefaultUuid guid] { get; }
    bool ContainsDescriptor(Guid guid);
    bool ContainsDescriptor(DefaultUuid guid);
    Property Property { get; }
    public Task<bool> WriteAsync(byte[] bytes, CancellationToken cancellationToken);
    Task<IObservable<byte[]>> EnableNotificationsAsync(CancellationToken cancellationToken);
}

public interface IConnectedGattDescriptor
{
    Guid Uuid { get; }
    Task<bool> WriteAsync(byte[] bytes, CancellationToken token);
    Task<byte[]> ReadAsync(CancellationToken token);
}

public sealed class ConnectedGattCharacteristic<TCharacteristic> where TCharacteristic : GattCharacteristic
{
    public ConnectedGattCharacteristic(TCharacteristic gattChar,
        IConnectedGattCharacteristic connChar)
    {
        GattCharacteristic = gattChar;
        Characteristic = connChar;
    }

    internal TCharacteristic GattCharacteristic { get; }
    internal IConnectedGattCharacteristic Characteristic { get; }
}

public static class Extensions
{
    public static async Task<IObservable<byte[]>> EnableNotificationsAsync<TCharacteristic>(
        this ConnectedGattCharacteristic<TCharacteristic> connChar,
        CancellationToken cancellationToken = default)
        where TCharacteristic : GattCharacteristic, IGattCharacteristic<NotifyProperty>
    {
        return await connChar.Characteristic.EnableNotificationsAsync(cancellationToken);
    }

    public static async Task<bool> WriteAsync<TCharacteristic>(
        this ConnectedGattCharacteristic<TCharacteristic> connChar,
        byte[] bytes,
        CancellationToken cancellationToken = default)
        where TCharacteristic : GattCharacteristic, IGattCharacteristic<WriteProperty>
    {
        return await connChar.Characteristic.WriteAsync(bytes, cancellationToken);
    }
}