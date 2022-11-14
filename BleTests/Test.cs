using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Ble;
using Ble.Config;
using Ble.Gap;
using Ble.Gatt;

namespace BleTests;

public static class Test
{
    public static async Task ScanAsync()
    {
        IBleAdapter adapter = null!;
        await foreach (IGapAdvertisement a in adapter
                           .Scan(ScanningMode.None, 1000, 1000)
                           .ToAsyncEnumerable())
        {
            
        }

        IGapAdvertisement adv = await adapter.Scan(ScanningMode.None, 1000, 1000)
            .FirstAsync()
            .ToTask();

        IConnectedPeripheral? connection = await adv.ConnectAsync()!;
        foreach (IConnectedGattService connectedGattService in connection!.Services)
        {
            Console.WriteLine(connectedGattService);
            foreach (IConnectedGattCharacteristic gattCharacteristic in connectedGattService.Characteristics)
            {
                Console.WriteLine(gattCharacteristic);
                foreach (IConnectedGattDescriptor gattCharacteristicDescriptor in gattCharacteristic.Descriptors)
                {
                    Console.WriteLine(gattCharacteristicDescriptor);
                }
            }
        }
    }
    
    public static async Task ConnectAsync()
    {
        IBleAdapter adapter = null!;

        var notifyCharacteristic = new Characteristic<NotifyProperty>("1600");
        var writeCharacteristic = new Characteristic<WriteProperty>("1601");
        Service service = new Service("fd4c")
            .Characteristics.Add(notifyCharacteristic)
            .Characteristics.Add(writeCharacteristic);
        Configuration configuration = new Configuration()
            .Services.Add(service);

        IConnectedPeripheral? connection = await adapter.ConnectAsync(1, configuration);
        IConnectedGattService connectedService = connection!.Select(service);
        IObservable<byte[]> notifyObservable = await connectedService.Select(notifyCharacteristic)
            .EnableNotificationsAsync();
        notifyObservable.Subscribe(bytes => Console.WriteLine(string.Join(',', bytes)));

        bool success = await connectedService.Select(writeCharacteristic)
            .WriteAsync(new byte[] {1,2,3});
    }
}