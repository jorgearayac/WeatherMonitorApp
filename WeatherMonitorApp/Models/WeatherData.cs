using WeatherMonitorApp.Structs;

namespace WeatherMonitorApp.Models;

/// <summary>
/// Weather data model containing location, temperature, and humidity information.
/// </summary>
public class WeatherData
{
    public Location Location { get; set; }
    public Temperature Temperature { get; set; }
    public Humidity Humidity { get; set; }
}