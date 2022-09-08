namespace Darp.DAQmx.Channel.AnalogInput;

public enum ADCTimingMode
{
    Automatic = 16097, // Automatic
    HighResolution = 10195, // High Resolution
    HighSpeed = 14712, // High Speed
    Best50HzRejection = 14713, // Best 50 Hz Rejection
    Best60HzRejection = 14714, // Best 60 Hz Rejection
    Custom = 10137 // Custom
}