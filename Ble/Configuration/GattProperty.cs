using Ble.Connection;
using Ble.Utils;

namespace Ble.Configuration;

public interface IGattProperty
{
    Property Property { get; }
    bool AreDescriptorsValid(IConnectedGattCharacteristic connChar);
}

public sealed class WriteProperty : IGattProperty
{
    public Property Property => Property.Write;

    public bool AreDescriptorsValid(IConnectedGattCharacteristic connChar)
    {
        return connChar.ContainsDescriptor(DefaultUuid.Characteristic);
    }
}

public sealed class NotifyProperty : IGattProperty
{
    public Property Property => Property.Notify;
    public bool AreDescriptorsValid(IConnectedGattCharacteristic connChar)
    {
        return connChar.ContainsDescriptor(DefaultUuid.ClientCharacteristicConfiguration);
    }
}