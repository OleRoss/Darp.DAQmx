using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Ble;
using Ble.Config;
using Ble.Gap;
using Ble.Gatt;
using Ble.Nrf;
using Ble.Reactive;
using Ble.Utils;
using Ble.Uuid;
using Ble.WinRT;
using Microsoft.Extensions.Logging;
using Serilog;

Console.WriteLine("Hello, World!");
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .CreateLogger();
ILogger<Program> logger = LoggerFactory
    .Create(builder => builder.AddSerilog(Log.Logger))
    .CreateLogger<Program>();


var notifyCharacteristic = new Characteristic<NotifyProperty>("ff62");
var writeCharacteristic = new Characteristic<WriteProperty>("ff61");
Service service = new Service("fd4c")
    .Characteristics.Add(notifyCharacteristic)
    .Characteristics.Add(writeCharacteristic);
Configuration configuration = new Configuration()
    .Services.Add(service)
    .Advertisement.Services.Add(service);

// using IBleAdapter adapter = new NrfAdapter("COM5", logger:logger);
using IBleAdapter adapter = new WinBleAdapter(logger:logger);

IConnectedPeripheral connectedPeripheral2 = await adapter
    .ScanAndConnectFirstAsync(configuration);
adapter.Scan(ScanningMode.Passive, 1000, 1000)
    .Where(adv => adv.Services.Length > 0)
    .Where(adv => adv.Services.Contains(0xfd4c))
    .Subscribe(x => logger.LogInformation("{Adv}", x));
await Task.Delay(50000);
return;
IAdvertisementScanner advObserver = adapter.Scanner();
advObserver.Start(ScanningMode.Passive, 1000, 1000);
advObserver.Subscribe(x => logger.LogDebug("Found"));
advObserver.Stop();

IObservable<IGapAdvertisement> advObs = adapter
    .Scan(configuration);
IObservable<IConnectedPeripheral> peripheralObs = adapter
    .ScanAndConnect(configuration);
IConnectedPeripheral connectedPeripheral1 = await adapter
    .ScanAndConnectFirstAsync(configuration);



/*
var advertiser = adapter.Advertise(configuration);
advertiser.OnNext(configuration);
IConnectedCentral remoteCentral = adapter.AdvertiseAndAccept(configuration);
var observableValue = new Subject<byte>();
observableValue.Subscribe(service[writeCharacteristic]);
centralObs.Subscribe((central, service) =>
{
    service[writeCharacteristic].UpdateValue();
});

adapter.AsCentral().Connect(configuration);
var aObs = adapter.AsObserver();
aObs.Start()
    .ForEachAsync(x => logger.LogDebug("{A}", x));

IObservable<IGapAdvertisement> scan = adapter.Observer()
    .Start();
scan.ForEachAsync

*/


return;
await foreach (IConnectedPeripheral x in adapter
                   .Scan(ScanningMode.Passive, 1000, 1000)
                   .Connect(configuration)
                   .Take(3)
                   .ToAsyncEnumerable())
{
    logger.LogInformation("Connection: {@Connection}", x);
    x.Dispose();
}
return;
IConnectedPeripheral connectedPeripheral = await adapter
    .Scan(ScanningMode.Passive, 1000, 1000)
    .Connect(configuration)
    .FirstAsync()
    .ToTask();
logger.LogInformation("Connection: {@Connection}", connectedPeripheral);

IObservable<byte[]> notifyObservable =
    (await connectedPeripheral[service].Select(notifyCharacteristic).EnableNotificationsAsync()).FirstAsync();

await connectedPeripheral[service].Select(writeCharacteristic).WriteAsync("5B000110005C005D".ToByteArray());
byte[] bytes = await notifyObservable.ToTask();
logger.LogInformation("Received bytes {@Bytes}", bytes);


/*
IConnection connection2 = await adapter
    .Observe(ScanningMode.Passive, 1000, 1000)
    .Where(adv => adv.ServiceUuids.Contains("fd4c".ToBleGuid()))
    .Connect()
    .FirstAsync()
    .ToTask();
IConnectedGattService fd4CService = connection2["fd4c".ToBleGuid()];
notifyObservable = (await fd4CService["ff62".ToBleGuid()].EnableNotificationsAsync(default)).FirstAsync();
await fd4CService["ff61".ToBleGuid()].WriteAsync("5B000110005C005D".ToByteArray(), default);
byte[] notifiedBytes = await notifyObservable.ToTask();
logger.LogInformation();*/