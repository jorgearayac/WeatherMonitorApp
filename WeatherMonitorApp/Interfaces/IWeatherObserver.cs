using WeatherMonitorApp.Models;
using WeatherMonitorApp.Enums;

namespace WeatherMonitorApp.Interfaces;

/// <summary>
/// Interface for weather observer bots.
/// </summary>
public interface IWeatherObserver
{
    BotType BotType { get; }
    string BotName { get; }
    void Update(WeatherData data);
}