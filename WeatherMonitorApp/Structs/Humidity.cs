namespace WeatherMonitorApp.Structs;

/// <summary>
/// Humidity measurement in percentage. Must be between 0 and 100.
/// </summary>
public struct Humidity
{
    public int Percentage { get; set; }

    public Humidity(int percentage)
    {
        if (percentage < 0 || percentage > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(percentage), "Humidity percentage must be between 0 and 100.");
        }
        Percentage = percentage;
    }

    public override string ToString() => $"{Percentage}%";
}