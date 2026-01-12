using WeatherMonitorApp.Models;

namespace WeatherMonitorApp.Interfaces;

public interface IBotCreator
{
    IEnumerable<IWeatherObserver> CreateBot(AppConfig config);
}