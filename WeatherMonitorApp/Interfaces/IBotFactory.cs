using WeatherMonitorApp.Models;

namespace WeatherMonitorApp.Interfaces;

/// <summary>
/// Interface for creating weather observer bots based on application configuration.
/// </summary>
public interface IBotFactory
{
    IEnumerable<IWeatherObserver> CreateBots(AppConfiguration appConfiguration);
}