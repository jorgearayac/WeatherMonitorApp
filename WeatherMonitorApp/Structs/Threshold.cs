namespace WeatherMonitorApp.Structs;

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