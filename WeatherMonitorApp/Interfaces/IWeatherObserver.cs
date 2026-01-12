using WeatherMonitorApp.Models;

namespace WeatherMonitorApp.Interfaces;

public interface IWeatherObserver
{
    string BotName { get; }
    void Update(WeatherData data);
}