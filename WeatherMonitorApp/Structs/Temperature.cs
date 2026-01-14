using WeatherMonitorApp.Enums;

namespace WeatherMonitorApp.Structs;

/// <summary>
/// Temperature measurement in Celsius.
/// </summary>
public struct Temperature
{
    public double Celsius { get; set; }

    public Temperature(double celsius)
    {
        if (celsius < -273.15)
        {
            throw new ArgumentOutOfRangeException(nameof(celsius), "Temperature cannot be below absolute zero (-273.15°C).");
        }
        Celsius = celsius;
    }

    public override string ToString() => $"{Celsius}°C";
}