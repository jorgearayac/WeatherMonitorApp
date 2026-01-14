using WeatherMonitorApp.Models;
using WeatherMonitorApp.Interfaces;

namespace WeatherMonitorApp.Services;

/// <summary>
/// Processes raw weather data by finding the correct parser and parsing the data.
/// </summary>
public class WeatherDataProcessor
{
    private readonly List<IWeatherDataParser> _parsers;

    public WeatherDataProcessor(List<IWeatherDataParser> parsers)
    {
        _parsers = parsers.ToList();

        if (_parsers == null || _parsers.Count == 0)
        {
            throw new InvalidOperationException("At least one Weather Data Parser must be provided.");
        }
    }

    /// <summary>
    /// Processes the weather data and returns a WeatherData object.
    /// </summary>
    /// <param name="rawData"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public WeatherData ProcessWeatherData(string rawData)
    {
        if (string.IsNullOrWhiteSpace(rawData))
        {
            throw new ArgumentException("Raw data cannot be null or empty.", nameof(rawData));
        }

        IWeatherDataParser? parser = FindSuitableParser(rawData);

        if (parser == null)
        {
            throw new InvalidOperationException("No suitable parser found for the provided raw data. Supported formats: JSON, XML");
        }

        return parser.Parse(rawData);
    }

    /// <summary>
    /// Finds a suitable parser for the given data.
    /// </summary>
    /// <param name="rawData"></param>
    /// <returns></returns>
    private IWeatherDataParser? FindSuitableParser(string rawData)
    {
        foreach (var parser in _parsers)
        {
            if (parser.CanParse(rawData))
            {
                return parser;
            }
        }

        return null;
    }
}