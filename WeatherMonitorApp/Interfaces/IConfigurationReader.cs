using WeatherMonitorApp.Models;

namespace WeatherMonitorApp.Interfaces;

/// <summary>
/// Interface for reading application configuration.
/// </summary>
public interface IConfigurationReader
{
    AppConfiguration ReadConfiguration(string filePath);
}