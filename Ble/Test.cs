using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Ble.Configuration;
using Ble.Connection;
using Ble.Gap;

namespace Ble;

public static class Test
{
    public static async Task ScanAsync()
    {
        IBleAdapter adapter = null!;
        await foreach (IAdvertisement a in adapter.Observe(ScanningMode.None, 1000, 1000).ToAsyncEnumerable())
        {
            
        }

        IAdvertisement adv = await adapter.Observe(ScanningMode.None, 1000, 1000)
            .FirstAsync()
            .ToTask();

        IConnection? connection = await adv.ConnectAsync()!;
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

        var notifyCharacteristic = new GattCharacteristic<NotifyProperty>("1600");
        var writeCharacteristic = new GattCharacteristic<WriteProperty>("1601");
        GattService service = new GattService("fd4c")
            .Characteristics.Add(notifyCharacteristic)
            .Characteristics.Add(writeCharacteristic);
        Peripheral peripheral = new Peripheral()
            .Services.Add(service);

        IConnection? connection = await adapter.ConnectAsync(1, peripheral);
        IConnectedGattService connectedService = connection!.Select(service);
        IObservable<byte[]> notifyObservable = await connectedService.Select(notifyCharacteristic)
            .EnableNotificationsAsync();
        notifyObservable.Subscribe(bytes => Console.WriteLine(string.Join(',', bytes)));

        bool success = await connectedService.Select(writeCharacteristic)
            .WriteAsync(new byte[] {1,2,3});
    }
}