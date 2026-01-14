namespace WeatherMonitorApp.Structs;

/// <summary>
/// Location represented by city name.
/// </summary>
public struct Location
{
    public string CityName { get; }

    public Location(string cityName)
    {
        if (string.IsNullOrWhiteSpace(cityName))
        {
            throw new ArgumentException("City name cannot be null or empty.", nameof(cityName));
        }
        CityName = cityName;
    }

    public override string ToString() => CityName;
}