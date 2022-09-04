using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Darp.Smc;

public static class Smc
{
    private static byte[] Construct(byte length, MessageCommand command, Action<BinaryWriter>? action = null)
    {
        var stream = new MemoryStream();
        var writer = new BinaryWriter(stream, Encoding.BigEndianUnicode);
        writer.Write(length);
        writer.Write((byte)command);
        action?.Invoke(writer);
        return stream.ToArray();
    }

    public static byte[] ConstructSetSamplingPeriod(short samplingPeriod) => Construct(4, MessageCommand.SetSamplingPeriod,
        writer =>
    {
        writer.Write(samplingPeriod);
    });
    public static byte[] ConstructStartSampling() => Construct(2, MessageCommand.StartSampling);
    public static byte[] ConstructStopSampling() => Construct(2, MessageCommand.StopSampling);
    public static byte[] ConstructGetStateAndData() => Construct(2, MessageCommand.GetStateAndData);
    public static byte[] ConstructGetState() => Construct(2, MessageCommand.GetState);
    public static byte[] ConstructGetBme280Data() => Construct(2, MessageCommand.GetBme280Data);
    public static byte[] ConstructGetStateAndTemp() => Construct(2, MessageCommand.GetStateAndTemp);

    private static Span<T> Reversed<T>(this Span<T> span)
    {
        span.Reverse();
        return span;
    }
    public static IResponse Deconstruct(Span<byte> bytes)
    {
        byte length = bytes[0];
        var command = (MessageCommand)bytes[1];
        switch (command)
        {
            case MessageCommand.GetBme280Data:
                if (length != 14)
                    throw new ArgumentOutOfRangeException(nameof(length), "Invalid length");
                var temperature = BitConverter.ToInt32(bytes[2..6].Reversed());
                var humidity = BitConverter.ToInt32(bytes[6..10].Reversed());
                var pressure = BitConverter.ToInt32(bytes[10..14].Reversed());
                return new Bme280Data(length, command,
                    temperature / 100.0f, humidity / 100.0f, pressure / 100.0f);
            case MessageCommand.GetState:
                if (length != 4)
                    throw new ArgumentOutOfRangeException(nameof(length), "Invalid length");
                var state1 = (ControllinoState) bytes[2];
                var errorCode1 = (ErrorCode) bytes[3];
                return new State(length, command, state1, errorCode1);
            case MessageCommand.GetStateAndData:
                if (length != 12)
                    throw new ArgumentOutOfRangeException(nameof(length), "Invalid length");
                var state2 = (ControllinoState) bytes[2];
                var errorCode2 = (ErrorCode) bytes[3];
                var channelOne = BitConverter.ToInt16(bytes[4..6].Reversed());
                var channelTwo = BitConverter.ToInt16(bytes[6..8].Reversed());
                var channelThree = BitConverter.ToInt16(bytes[8..10].Reversed());
                var channelFour = BitConverter.ToInt16(bytes[10..12].Reversed());
                return new StateAndData(length, command, state2, errorCode2,
                    channelOne, channelTwo, channelThree, channelFour);
            case MessageCommand.GetStateAndTemp:
                if (length != 36)
                    throw new ArgumentOutOfRangeException(nameof(length), "Invalid length");
                var state3 = (ControllinoState) bytes[2];
                var errorCode3 = (ErrorCode) bytes[3];
                var temperatureOne = BitConverter.ToInt32(bytes[4..8].Reversed());
                var temperatureTwo = BitConverter.ToInt32(bytes[8..12].Reversed());
                var temperatureThree = BitConverter.ToInt32(bytes[12..16].Reversed());
                var temperatureFour = BitConverter.ToInt32(bytes[16..20].Reversed());
                var temperatureFive = BitConverter.ToInt32(bytes[20..24].Reversed());
                var temperatureSix = BitConverter.ToInt32(bytes[24..28].Reversed());
                var temperatureSeven = BitConverter.ToInt32(bytes[28..32].Reversed());
                var temperatureEight = BitConverter.ToInt32(bytes[32..36].Reversed());
                return new StateAndTemp(length, command, state3, errorCode3,
                    temperatureOne / 100.0f, temperatureTwo / 100.0f,
                    temperatureThree / 100.0f, temperatureFour / 100.0f,
                    temperatureFive / 100.0f, temperatureSix / 100.0f,
                    temperatureSeven / 100.0f, temperatureEight / 100.0f);
            case MessageCommand.SetSamplingPeriod:
            case MessageCommand.StartSampling:
            case MessageCommand.StopSampling:
            default:
                throw new ArgumentOutOfRangeException(nameof(command),$"{command} is unknown");
        }
    }
}

public interface IResponse
{
    byte Length { get; init; }
    MessageCommand MessageCommand { get; init; }
}

public record State(byte Length, MessageCommand MessageCommand,
    ControllinoState ControllinoState, ErrorCode ErrorCode) : IResponse;

public record StateAndData(byte Length, MessageCommand MessageCommand,
    ControllinoState ControllinoState,
    ErrorCode ErrorCode,
    short ChannelOne,
    short ChannelTwo,
    short ChannelThree,
    short ChannelFour) : State(Length, MessageCommand, ControllinoState, ErrorCode);

public record Bme280Data(byte Length, MessageCommand MessageCommand,
    float Temperature,
    float Humidity,
    float Pressure) : IResponse
{
    /// <summary>
    /// Temperature in °C
    /// </summary>
    public float Temperature { get; init; } = Temperature;
    /// <summary>
    /// Humidity in ???
    /// </summary>
    public float Humidity { get; init; } = Humidity;
    /// <summary>
    /// Pressure in hPa
    /// </summary>
    public float Pressure { get; init; } = Pressure;
}

public record StateAndTemp(byte Length, MessageCommand MessageCommand,
    ControllinoState ControllinoState,
    ErrorCode ErrorCode,
    float TemperatureOne,
    float TemperatureTwo,
    float TemperatureThree,
    float TemperatureFour,
    float TemperatureFive,
    float TemperatureSix,
    float TemperatureSeven,
    float TemperatureEight) : State(Length, MessageCommand, ControllinoState, ErrorCode);