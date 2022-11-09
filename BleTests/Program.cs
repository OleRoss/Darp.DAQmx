using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Ble;
using Ble.Configuration;
using Ble.Connection;
using Ble.Gap;
using Ble.Nrf;
using Ble.Utils;
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


var notifyCharacteristic = new GattCharacteristic<NotifyProperty>("ff62");
var writeCharacteristic = new GattCharacteristic<WriteProperty>("ff61");
GattService service = new GattService("fd4c")
    .Characteristics.Add(notifyCharacteristic)
    .Characteristics.Add(writeCharacteristic);
Peripheral peripheral = new Peripheral()
    .Services.Add(service)
    .Advertisement.Services.Add(service);

using IBleAdapter adapter = new NrfAdapter("COM5", logger:logger);

IConnection connection = await adapter
    .Observe(ScanningMode.Passive, 1000, 1000)
    .Connect(peripheral)
    .FirstAsync()
    .ToTask();

logger.LogInformation("Connection: {@Connection}", connection);

IObservable<byte[]> notifyObservable = await connection[service].Select(notifyCharacteristic).EnableNotificationsAsync();

await connection[service].Select(writeCharacteristic).WriteAsync("5B000110005C005D".ToByteArray());
byte[] bytes = await notifyObservable.ToTask();
logger.LogInformation("Received bytes {@Bytes}", bytes);



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
