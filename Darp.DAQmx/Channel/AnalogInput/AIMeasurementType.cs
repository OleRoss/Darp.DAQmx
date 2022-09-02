namespace Darp.DAQmx.Channel.AnalogInput;

/// <summary>Indicates the measurement to take with the analog input channel and in some cases, such as for temperature measurements, the sensor to use.</summary>
/// <remarks>Indicates the measurement to take with the analog input channel and in some cases, such as for temperature measurements, the sensor to use.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.AIChannel.MeasurementType" />.</remarks>
public enum AIMeasurementType
{
    /// <summary>Current measurement.</summary>
    Current = 10134, // 0x00002796
    /// <summary>Frequency measurement using a frequency to voltage converter.</summary>
    Frequency = 10181, // 0x000027C5
    /// <summary>Resistance measurement.</summary>
    Resistance = 10278, // 0x00002826
    /// <summary>Strain measurement.</summary>
    StrainGage = 10300, // 0x0000283C
    /// <summary>Temperature measurement using an RTD.</summary>
    Rtd = 10301, // 0x0000283D
    /// <summary>Temperature measurement using a thermistor.</summary>
    Thermistor = 10302, // 0x0000283E
    /// <summary>Temperature measurement using a thermocouple.</summary>
    Thermocouple = 10303, // 0x0000283F
    /// <summary>Temperature measurement using a built-in sensor on a terminal block or device. On SCXI modules, for example, this could be the CJC sensor.</summary>
    BuiltInTemperatureSensor = 10311, // 0x00002847
    /// <summary>Voltage measurement.</summary>
    Voltage = 10322, // 0x00002852
    /// <summary>Voltage measurement with an excitation source. You can use this measurement type for custom sensors that require excitation, but you must use a custom scale to scale the measured voltage.</summary>
    VoltageCustomWithExcitation = 10323, // 0x00002853
    /// <summary>Voltage RMS measurement.</summary>
    VoltageRms = 10350, // 0x0000286E
    /// <summary>Current RMS measurement.</summary>
    CurrentRms = 10351, // 0x0000286F
    /// <summary>Position measurement using an LVDT.</summary>
    Lvdt = 10352, // 0x00002870
    /// <summary>Position measurement using an RVDT.</summary>
    Rvdt = 10353, // 0x00002871
    /// <summary>Sound pressure measurement using a microphone.</summary>
    Microphone = 10354, // 0x00002872
    /// <summary>Acceleration measurement using an accelerometer.</summary>
    Accelerometer = 10356, // 0x00002874
    /// <summary>Measurement type defined by TEDS.</summary>
    TedsSensor = 12531, // 0x000030F3
    /// <summary>Position measurement using an eddy current proximity probe.</summary>
    EddyCurrentProximityProbe = 14835, // 0x000039F3
    /// <summary>Force measurement using an IEPE Sensor.</summary>
    ForceIepeSensor = 15895, // 0x00003E17
    /// <summary>Force measurement using a bridge-based sensor.</summary>
    ForceBridge = 15899, // 0x00003E1B
    /// <summary>Pressure measurement using a bridge-based sensor.</summary>
    PressureBridge = 15902, // 0x00003E1E
    /// <summary>Torque measurement using a bridge-based sensor.</summary>
    TorqueBridge = 15905, // 0x00003E21
    /// <summary>Measure <see href="javascript:launchSharedHelp('measfunds.chm::/bridgeScaling.html');">voltage ratios</see> from a Wheatstone bridge.</summary>
    Bridge = 15908, // 0x00003E24
    /// <summary>Velocity measurement using an IEPE Sensor.</summary>
    VelocityIepeSensor = 15966, // 0x00003E5E
    /// <summary>Strain measurement using a rosette strain gage.</summary>
    RosetteStrainGage = 15980, // 0x00003E6C
    /// <summary>Acceleration measurement using a charge-based sensor.</summary>
    AccelerationCharge = 16104, // 0x00003EE8
    /// <summary>Charge measurement.</summary>
    Charge = 16105, // 0x00003EE9
    /// <summary>Acceleration measurement using a 4 wire DC voltage based sensor.</summary>
    AccelerationFourWireDCVoltage = 16106, // 0x00003EEA
}