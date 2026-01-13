using WeatherMonitorApp.Models;
using WeatherMonitorApp.Enums;

namespace WeatherMonitorApp.Interfaces;

/// <summary>
/// Interface for parsing weather data from different formats.
/// </summary>
public interface IWeatherDataParser
{
    DataFormat Format { get; }
    WeatherData Parse(string rawData);
    bool CanParse(string rawData);
}