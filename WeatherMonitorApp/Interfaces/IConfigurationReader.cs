using WeatherMonitorApp.Models;

namespace WeatherMonitorApp.Interfaces;

public interface IConfigurationReader
{
    AppConfiguration ReadConfiguration(string filePath);
}