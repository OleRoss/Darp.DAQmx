using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace Darp.Smc;

public static class SerialPortExtensions
{
    private static async Task<byte> ReadByteAsync(this SerialPort serialPort,
        TimeSpan timeout,
        CancellationToken token = default)
    {
        DateTime timeoutTime = DateTime.UtcNow + timeout;
        while (!token.IsCancellationRequested)
        {
            if (serialPort.BytesToRead > 0)
                return (byte)serialPort.ReadByte();
            if (timeout.Ticks > 0 && DateTime.UtcNow > timeoutTime)
                throw new TimeoutException($"Reading bytes timed out after {timeout}");
            await Task.Delay(10, token);
        }
        throw new OperationCanceledException("Could not complete reading bytes");
    }
    public static async Task SetSamplingPeriodAsync(this SerialPort serialPort, short samplingPeriod) => await Task.Run(() =>
    {
        byte[] data = Smc.ConstructSetSamplingPeriod(samplingPeriod);
        serialPort.Write(data, 0, data.Length);
    });

    private static async Task SendSimple(this SerialPort serialPort, Func<byte[]> func) => await Task.Run(() =>
    {
        byte[] data = func();
        serialPort.Write(data, 0, data.Length);
    });
    public static Task<State> StartSamplingAsync(this SerialPort serialPort) => serialPort
        .GetResponse<State>(Smc.ConstructStartSampling);

    public static void OnSampleReceived(this SerialPort serialPort,
        Action<StateAndTemp> callback,
        CancellationToken token = default)
    {
        Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var state = await serialPort.ReadResponse<State>(token);
                    if (state.ControllinoState is not ControllinoState.Running)
                        return;
                    if (state is StateAndTemp stateAndTemp)
                        callback(stateAndTemp);
                }
                catch (OperationCanceledException) { }
            }
        }, token);
    }

    public static async Task StopSamplingAsync(this SerialPort serialPort)
    {
        await serialPort.SendSimple(Smc.ConstructStopSampling);
        // Wait a bit for the arduino to send the last stuff and discard everything up to now to get a fresh start
        await Task.Delay(100);
        serialPort.DiscardInBuffer();
    }

    private static async Task<T> GetResponse<T>(this SerialPort serialPort,
        Func<byte[]> func,
        CancellationToken token = default)
        where T : IResponse
    {
        await serialPort.SendSimple(func);
        return await serialPort.ReadResponse<T>(token: token);
    }
    private static async Task<T> ReadResponse<T>(this SerialPort serialPort, CancellationToken token = default)
        where T : IResponse
    {
        TimeSpan timeout = TimeSpan.FromSeconds(5);
        byte length = await serialPort.ReadByteAsync(timeout, token: token);
        if (length <= 1)
            throw new ArgumentOutOfRangeException(nameof(length), $"Length must be greater than 1, but is {length}");
        var byteBuffer = new byte[length];
        byteBuffer[0] = length;
        for (var i = 0; i < length - 1; i++)
            byteBuffer[i + 1] = await serialPort.ReadByteAsync(timeout, token: token);
        IResponse res = Smc.Deconstruct(byteBuffer);
        if (res is T response)
            return response;
        throw new Exception($"Invalid response! Expected {typeof(T).Name} but got {res.GetType().Name}");
    }
    public static Task<State> GetStateAsync(this SerialPort serialPort) => serialPort
        .GetResponse<State>(Smc.ConstructGetState);
    [Obsolete("I dont think this function works. In the arduino nothing gets implemented here", error:true)]
    public static Task<StateAndData> GetStateAndDataAsync(this SerialPort serialPort) => serialPort
        .GetResponse<StateAndData>(Smc.ConstructGetStateAndData);
    public static Task<StateAndTemp> GetStateAndTempAsync(this SerialPort serialPort) => serialPort
        .GetResponse<StateAndTemp>(Smc.ConstructGetStateAndTemp);
    public static Task<Bme280Data> GetBme280DataAsync(this SerialPort serialPort) => serialPort
        .GetResponse<Bme280Data>(Smc.ConstructGetBme280Data);

    public static Task<State> WaitUntilInitializedAsync(this SerialPort serialPort) => serialPort.ReadResponse<State>();
}