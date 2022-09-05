using System;
using System.Collections.Generic;
using Darp.DAQmx.Channel.AnalogOutput;
using Darp.DAQmx.Channel.CounterOutput;
using static Darp.DAQmx.NationalInstruments.Functions.Interop;
using static Darp.DAQmx.DaqMxException;

namespace Darp.DAQmx;

public class Device
{
    private readonly Lazy<bool> _lazyIsSimulated;
    private readonly Lazy<ProductCategory> _lazyProductCategory;
    private readonly Lazy<string> _lazyProductType;
    private readonly Lazy<uint> _lazyProductNumber;
    private readonly Lazy<uint> _lazySerialNumber;

    public Device(string deviceName)
    {
        DeviceName = deviceName;
        _lazyIsSimulated = LazyThrowIfFailedOrReturn((out int data) =>
            DAQmxGetDevIsSimulated(DeviceName, out data), i => i == 1);
        _lazyProductCategory = LazyThrowIfFailedOrReturn((out ProductCategory category) =>
            DAQmxGetDevProductCategory(DeviceName, out category));
        _lazyProductType = LazyThrowIfFailedOrReturnString((in byte pointer, uint length) =>
            DAQmxGetDevProductType(DeviceName, pointer, length));
        _lazyProductNumber = LazyThrowIfFailedOrReturn((out uint data) => DAQmxGetDevProductNum(DeviceName, out data));
        _lazySerialNumber = LazyThrowIfFailedOrReturn((out uint data) => DAQmxGetDevSerialNum(DeviceName, out data));

        AnalogInputs = new DeviceAnalogInputs(DeviceName);
        AnalogOutputs = new DeviceAnalogOutputs(DeviceName);
        DigitalInputs = new DeviceDigitalInputs(DeviceName);
        DigitalOutputs = new DeviceDigitalOutputs(DeviceName);
        CounterInputs = new DeviceCounterInputs(DeviceName);
        CounterOutputs = new DeviceCounterOutputs(DeviceName);
    }

    public string DeviceName { get; }

    public bool IsSimulated => _lazyIsSimulated.Value;
    public ProductCategory ProductCategory => _lazyProductCategory.Value;
    public string ProductType => _lazyProductType.Value;
    public uint ProductNumber => _lazyProductNumber.Value;
    public uint SerialNumber => _lazySerialNumber.Value;

    public DeviceAnalogInputs AnalogInputs { get; }
    public DeviceAnalogOutputs AnalogOutputs { get; }
    public DeviceDigitalInputs DigitalInputs { get; }
    public DeviceDigitalOutputs DigitalOutputs { get; }
    public DeviceCounterInputs CounterInputs { get; }
    public DeviceCounterOutputs CounterOutputs { get; }

    public static implicit operator string(Device device) => device.DeviceName;
}

public class DeviceAnalogInputs
{
    private readonly string _deviceName;
    private readonly Lazy<IReadOnlyList<string>> _lazyPhysicalChannels;

    public DeviceAnalogInputs(string deviceName)
    {
        _deviceName = deviceName;
        _lazyPhysicalChannels = LazyThrowIfFailedOrReturnString((in byte pointer, uint length) =>
            DAQmxGetDevAIPhysicalChans(_deviceName, pointer, length), x => x.SplitNi());
    }

    public IReadOnlyList<string> PhysicalChannels => _lazyPhysicalChannels.Value;

    public override string ToString() => $"{GetType().Name} {{ NumChannels = {PhysicalChannels.Count} }}";
}

public class DeviceAnalogOutputs
{
    private readonly string _deviceName;
    private readonly Lazy<IReadOnlyList<string>> _lazyPhysicalChannels;
    private readonly Lazy<IReadOnlyList<AOOutputType>> _lazyOutputTypes;

    public DeviceAnalogOutputs(string deviceName)
    {
        _deviceName = deviceName;
        _lazyPhysicalChannels = LazyThrowIfFailedOrReturnString((in byte pointer, uint length) =>
            DAQmxGetDevAOPhysicalChans(_deviceName, pointer, length), x => x.SplitNi());
        _lazyOutputTypes = LazyThrowIfFailedOrReturnListOf((in AOOutputType pointer, uint length) =>
            DAQmxGetDevAOSupportedOutputTypes(_deviceName, pointer, length));
    }

    public IReadOnlyList<string> PhysicalChannels => _lazyPhysicalChannels.Value;
    public IReadOnlyList<AOOutputType> OutputTypes => _lazyOutputTypes.Value;

    public override string ToString() => $"{GetType().Name} {{ NumChannels = {PhysicalChannels.Count} }}";
}

public class DeviceDigitalInputs
{
    private readonly string _deviceName;
    private readonly Lazy<IReadOnlyList<string>> _lazyPorts;
    private readonly Lazy<IReadOnlyList<string>> _lazyLines;

    public DeviceDigitalInputs(string deviceName)
    {
        _deviceName = deviceName;
        _lazyPorts = LazyThrowIfFailedOrReturnString((in byte pointer, uint length) =>
                DAQmxGetDevDIPorts(_deviceName, pointer, length), x => x.SplitNi());
        _lazyLines = LazyThrowIfFailedOrReturnString((in byte pointer, uint length) =>
                DAQmxGetDevDILines(_deviceName, pointer, length), x => x.SplitNi());
    }

    public IReadOnlyList<string> Ports => _lazyPorts.Value;
    public IReadOnlyList<string> Lines => _lazyLines.Value;

    public override string ToString() => $"{GetType().Name} {{ NumLines = {Lines.Count} }}";
}

public class DeviceDigitalOutputs
{
    private readonly string _deviceName;
    private readonly Lazy<IReadOnlyList<string>> _lazyPorts;
    private readonly Lazy<IReadOnlyList<string>> _lazyLines;

    public DeviceDigitalOutputs(string deviceName)
    {
        _deviceName = deviceName;
        _lazyPorts = LazyThrowIfFailedOrReturnString((in byte pointer, uint length) =>
                DAQmxGetDevDOPorts(_deviceName, pointer, length), x => x.SplitNi());
        _lazyLines = LazyThrowIfFailedOrReturnString((in byte pointer, uint length) =>
                DAQmxGetDevDOLines(_deviceName, pointer, length), x => x.SplitNi());
    }

    public IReadOnlyList<string> Ports => _lazyPorts.Value;
    public IReadOnlyList<string> Lines => _lazyLines.Value;

    public override string ToString() => $"{GetType().Name} {{ NumLines = {Lines.Count} }}";
}

public class DeviceCounterInputs
{
    private readonly string _deviceName;
    private readonly Lazy<IReadOnlyList<string>> _lazyPhysicalChannels;

    public DeviceCounterInputs(string deviceName)
    {
        _deviceName = deviceName;
        _lazyPhysicalChannels = LazyThrowIfFailedOrReturnString((in byte pointer, uint length) =>
            DAQmxGetDevCIPhysicalChans(_deviceName, pointer, length), x => x.SplitNi());
    }

    public IReadOnlyList<string> PhysicalChannels => _lazyPhysicalChannels.Value;

    public override string ToString() => $"{GetType().Name} {{ NumChannels = {PhysicalChannels.Count} }}";
}

public class DeviceCounterOutputs
{
    private readonly string _deviceName;
    private readonly Lazy<IReadOnlyList<string>> _lazyPhysicalChannels;
    private readonly Lazy<IReadOnlyList<COOutputType>> _lazyOutputTypes;

    public DeviceCounterOutputs(string deviceName)
    {
        _deviceName = deviceName;
        _lazyPhysicalChannels = LazyThrowIfFailedOrReturnString((in byte pointer, uint length) =>
            DAQmxGetDevCOPhysicalChans(_deviceName, pointer, length), x => x.SplitNi());
        _lazyOutputTypes = LazyThrowIfFailedOrReturnListOf((in COOutputType pointer, uint length) =>
            DAQmxGetDevCOSupportedOutputTypes(_deviceName, pointer, length));
    }

    public IReadOnlyList<string> PhysicalChannels => _lazyPhysicalChannels.Value;
    public IReadOnlyList<COOutputType> OutputTypes => _lazyOutputTypes.Value;

    public override string ToString() => $"{GetType().Name} {{ NumChannels = {PhysicalChannels.Count} }}";
}