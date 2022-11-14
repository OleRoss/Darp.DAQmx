using System;

namespace Ble.Gatt;

[Flags]
public enum Property
{
    /// <summary>The characteristic doesn’t have any properties that apply.</summary>
    None = 0,
    /// <summary> The characteristic supports broadcasting </summary>
    Broadcast = 1,
    /// <summary> The characteristic is readable </summary>
    Read = 2,
    /// <summary> The characteristic supports Write Without Response </summary>
    WriteWithoutResponse = 4,
    /// <summary>The characteristic is writable</summary>
    Write = 8,
    /// <summary>The characteristic is notifiable</summary>
    Notify = 16, // 0x00000010
    /// <summary> The characteristic is indicatable </summary>
    Indicate = 32,
    /// <summary> The characteristic supports signed writes </summary>
    AuthenticatedSignedWrites = 64,
}