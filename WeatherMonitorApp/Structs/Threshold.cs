namespace WeatherMonitorApp.Structs;

/// <summary>
/// Threshold structure representing a value and its unit.
/// </summary>
public struct Threshold
{
    public double Value { get; }
    public string Unit { get; }

    public Threshold(double value, string unit)
    {
        Value = value;
        Unit = unit ?? "";
    }

    public override string ToString() => $"{Value} {Unit}";
}