using WeatherMonitorApp.Models;
using WeatherMonitorApp.Enums;

namespace WeatherMonitorApp.Interfaces;

public interface IWeatherObserver
{
    BotType BotType { get; }
    string BotName { get; }
    void Update(WeatherData data);
}