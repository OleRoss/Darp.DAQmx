using ErrorOr;

namespace Darp.NrfBleDriver;

public static class BleErrors
{
    public static Error WriteToPeripheralSendFailed = Error.Failure(
        "Ble.WriteToPeripheralSendFailed",
        "Could not send write request to peripheral");
    public static Error WriteToPeripheralFailed = Error.Failure(
        "Ble.WriteToPeripheralFailed",
        "Write to peripheral is not successful");
}