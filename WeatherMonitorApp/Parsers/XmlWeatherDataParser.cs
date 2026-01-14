using System.Xml.Linq;
using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Models;
using WeatherMonitorApp.Enums;
using WeatherMonitorApp.Structs;

namespace WeatherMonitorApp.Parsers;

public class XmlWeatherDataParser : IWeatherDataParser
{
    public DataFormat Format => DataFormat.XML;
    
    /// <summary>
    /// Determines if the raw data can be parsed as XML.
    /// </summary>
    /// <param name="rawData"></param>
    /// <returns></returns>
    public bool CanParse(string rawData)
    {
        if (string.IsNullOrWhiteSpace(rawData))
        {
            return false;
        }
        
        rawData = rawData.Trim();
        return rawData.StartsWith("<") && rawData.EndsWith(">");
    }

    /// <summary>
    /// Parses the raw XML weather data into a WeatherData object.
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
            throw new FormatException("Provided data is not in valid XML format.");
        }

        // Parse the XML document
        XDocument xmlDoc = XDocument.Parse(rawData);
        XElement xmlRoot = xmlDoc.Element("WeatherData") ?? throw new FormatException("XML does not contain WeatherData root element.");

        // Extract fields from XML
        XElement locationElement = xmlRoot.Element("Location") ?? throw new FormatException("Missing 'Location' element in XML data.");
        string locationValue = LocationExtractor(locationElement);

        XElement temperatureElement = xmlRoot.Element("Temperature") ?? throw new FormatException("Missing 'Temperature' element in XML data.");
        double temperatureValue = TemperatureExtractor(temperatureElement);

        XElement humidityElement = xmlRoot.Element("Humidity") ?? throw new FormatException("Missing 'Humidity' element in XML data.");
        int humidityValue = HumidityExtractor(humidityElement);

        // Create WeatherData object with structs
        Location location = new Location(locationValue);
        Temperature temperature = new Temperature(temperatureValue);
        Humidity humidity = new Humidity(humidityValue);

        return new WeatherData(location, temperature, humidity);
    }

    /// <summary>
    /// Extracts the location value from the XML location element.
    /// </summary>
    /// <param name="locationElement"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    private static string LocationExtractor(XElement locationElement)
    {
        string locationValue = locationElement.Value;
        
        if (string.IsNullOrWhiteSpace(locationValue))
        {
            throw new FormatException("'Location' value cannot be empty.");
        }

        return locationValue;
    }

    /// <summary>
    /// Extracts the temperature value from the XML temperature element.
    /// </summary>
    /// <param name="temperatureElement"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    private static double TemperatureExtractor(XElement temperatureElement)
    {
        if (!double.TryParse(temperatureElement.Value, out double temperatureValue))
        {
            throw new FormatException($"'Temperature' value {temperatureElement.Value} is not a valid number.");
        }

        return temperatureValue;
    }

    /// <summary>
    /// Extracts the humidity value from the XML humidity element.
    /// </summary>
    /// <param name="humidityElement"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    private static int HumidityExtractor(XElement humidityElement)
    {
        if (!double.TryParse(humidityElement.Value, out double humidityDouble))
        {
            throw new FormatException($"'Humidity' value {humidityElement.Value} is not a valid number.");
        }
        int humidityValue = (int)humidityDouble;

        return humidityValue;
    }
}