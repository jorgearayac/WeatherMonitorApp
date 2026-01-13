using WeatherMonitorApp.Models;
using WeatherMonitorApp.Bots;
using  WeatherMonitorApp.Interfaces;

namespace WeatherMonitorApp.Services;

/// <summary>
/// Factory class to create weather observer bots based on application configuration.
/// </summary>
public class BotFactory : IBotFactory
{
    /// <summary>
    /// Creates a list of weather observer bots based on the provided application configuration.
    /// </summary>
    /// <typeparam name="IWeatherObserver"></typeparam>
    /// <param name="appConfiguration">The application configuration containing bot settings.</param>
    /// <returns>A list of IWeatherObserver instances.</returns>
    public IEnumerable<IWeatherObserver> CreateBots(AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        var bots = new List<IWeatherObserver>();
        
        AddBots(appConfiguration, bots);

        return bots;
    }

    /// <summary>
    /// Helper method to add bots based on configuration.
    /// </summary>
    /// <param name="appConfiguration"></param>
    /// <param name="bots"></param>
    private static void AddBots(AppConfiguration appConfiguration, List<IWeatherObserver> bots)
    {
        if (appConfiguration.RainBot != null)
        {
            bots.Add(new RainBot(appConfiguration.RainBot));
        }

        if (appConfiguration.SunBot != null)
        {
            bots.Add(new SunBot(appConfiguration.SunBot));
        }

        if (appConfiguration.SnowBot != null)
        {
            bots.Add(new SnowBot(appConfiguration.SnowBot));
        }
    }
}