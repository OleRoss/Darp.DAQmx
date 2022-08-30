namespace Darp.DAQmx.Task.Common;

public readonly struct DaqMxUnit
{
    private const int DefaultValue = 10348;
    private const string DefaultCustomScaleName = "";
    // public static readonly DaqMxUnit FromCustomScale = new(nameof(FromCustomScale), 10065); // 0x00002751
    public static readonly DaqMxUnit Volts = new(10348, string.Empty); // 0x0000286C

    public int Value { get; } = DefaultValue;
    public string CustomScaleName { get; } = DefaultCustomScaleName;
    private DaqMxUnit(int value, string customScaleName)
    {
        CustomScaleName = customScaleName;
        Value = value;
    }

    public override string ToString() => $"{CustomScaleName} ({Value})";
    public static implicit operator int(DaqMxUnit unit) => unit.Value;
}