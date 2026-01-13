using WeatherMonitorApp.Models;
using WeatherMonitorApp.Enums;

namespace WeatherMonitorApp.Interfaces;

/// <summary>
/// Interface for weather data subject.
/// </summary>
public interface IWeatherSubject
{
    void RegisterObserver(IWeatherObserver observer);
    void RemoveObserver(IWeatherObserver observer);
    void NotifyObservers(WeatherData data);
}