using Ble.Gatt;
using Ble.Uuid;

namespace Ble.Config;

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
        return connChar.ContainsDescriptor(connChar.Uuid);
    }
}

public sealed class NotifyProperty : IGattProperty
{
    public Property Property => Property.Notify;
    public bool AreDescriptorsValid(IConnectedGattCharacteristic connChar)
    {
        return connChar.ContainsDescriptor(GattUuid.ClientCharacteristicConfiguration);
    }
}