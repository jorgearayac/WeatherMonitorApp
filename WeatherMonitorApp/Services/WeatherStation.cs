using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Models;

namespace WeatherMonitorApp.Services;

/// <summary>
/// Represents a weather station that notifies observers of weather data updates.
/// </summary>
public class WeatherStation : IWeatherSubject
{
    private readonly List<IWeatherObserver> _observers = new();

    public void RegisterObserver(IWeatherObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void RemoveObserver(IWeatherObserver observer)
    {
        _observers.Remove(observer);
    }

    public void PublishWeather(WeatherData weatherData)
    {
        NotifyObservers(weatherData);
    }

    public void NotifyObservers(WeatherData data)
    {
        foreach (var observer in _observers)
        {
            observer.Update(data);
        }
    }
}