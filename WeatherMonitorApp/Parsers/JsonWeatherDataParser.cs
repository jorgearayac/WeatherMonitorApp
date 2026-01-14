using System.Text.Json;
using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Enums;
using WeatherMonitorApp.Models;
using WeatherMonitorApp.Structs;

namespace WeatherMonitorApp.Parsers;

/// <summary>
/// Parser for JSON formatted weather data.
/// </summary>
public class JsonWeatherDataParser : IWeatherDataParser
{
    public DataFormat Format => DataFormat.JSON;

    /// <summary>
    /// Determines if the raw data can be parsed as JSON.
    /// </summary>
    /// <param name="rawData"></param>
    /// <returns></returns>
    public bool CanParse(string rawData)
    {
        rawData = rawData.Trim();
        return rawData.StartsWith("{") && rawData.EndsWith("}");
    }

    /// <summary>
    /// Parses the raw JSON weather data into a WeatherData object.
    /// </summary>
    /// <param name="rawData"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="FormatException"></exception>
    public WeatherData Parse(string rawData)
    {
        if (string.IsNullOrWhiteSpace(rawData))
        {
            throw new ArgumentException("Raw data cannot be null or empty.", nameof(rawData));
        }

        if (!CanParse(rawData))
        {
            throw new FormatException("Provided data is not in valid JSON format.");
        }

        // Parse the JSON data with case-insensitive property names
        // NOT USED YET
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        JsonDocument jsonDoc = JsonDocument.Parse(rawData);
        JsonElement jsonRoot = jsonDoc.RootElement;

        // Extract fields from JSON
        string locationValue;
        double temperatureValue;
        int humidityValue;
        FieldExtractor(jsonRoot, out locationValue, out temperatureValue, out humidityValue);

        // Create WeatherData object with structs
        Location location = new Location(locationValue);
        Temperature temperature = new Temperature(temperatureValue);
        Humidity humidity = new Humidity(humidityValue);

        return new WeatherData(location, temperature, humidity);
    }

    /// <summary>
    /// Extracts required fields from the JSON jsonRoot element.
    /// </summary>
    /// <param name="jsonRoot"></param>
    /// <param name="locationValue"></param>
    /// <param name="temperatureValue"></param>
    /// <param name="humidityValue"></param>
    /// <exception cref="FormatException"></exception>
    private static void FieldExtractor(JsonElement jsonRoot, out string locationValue, out double temperatureValue, out int humidityValue)
    {
        if (!jsonRoot.TryGetProperty("Location", out JsonElement locationElement))
        {
            throw new FormatException("Missing 'Location' field in the JSON data.");
        }
        locationValue = locationElement.GetString() ?? throw new FormatException("Location value is null.");
        
        if (!jsonRoot.TryGetProperty("Temperature", out JsonElement temperatureElement))
        {
            throw new FormatException("Missing 'Temperature' field in the JSON data.");
        }
        temperatureValue = temperatureElement.GetDouble();
        
        if (!jsonRoot.TryGetProperty("Humidity", out JsonElement humidityElement))
        {
            throw new FormatException("Missing 'Humidity' field in the JSON data.");
        }
        humidityValue = (int)humidityElement.GetDouble();
    }
}