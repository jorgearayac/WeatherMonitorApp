using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Models;

namespace WeatherMonitorApp.Services;

/// <summary>
/// Represents a weather station that notifies observers of weather data updates.
/// </summary>
public class WeatherStation : IWeatherSubject
{
    private readonly List<IWeatherObserver> _observers = new();

    /// <summary>
    /// Register a weather observer to receive updates.
    /// </summary>
    /// <param name="observer"></param>
    public void RegisterObserver(IWeatherObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    /// <summary>
    /// Remove a weather observer from receiving updates.
    /// </summary>
    /// <param name="observer"></param>
    public void RemoveObserver(IWeatherObserver observer)
    {
        _observers.Remove(observer);
    }

    /// <summary>
    /// Publishes new weather data to all registered observers.
    /// </summary>
    /// <param name="weatherData"></param>
    public void PublishWeather(WeatherData weatherData)
    {
        NotifyObservers(weatherData);
    }

    /// <summary>
    /// Notifies all registered observers with the provided weather data.
    /// </summary>
    /// <param name="data"></param>
    public void NotifyObservers(WeatherData data)
    {
        foreach (var observer in _observers)
        {
            observer.Update(data);
        }
    }
}