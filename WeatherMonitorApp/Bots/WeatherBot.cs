using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Models;
using WeatherMonitorApp.Enums;

namespace WeatherMonitorApp.Bots;

/// <summary>
/// Abstract base class for weather bots.
/// </summary>
public abstract class WeatherBot : IWeatherObserver
{
    protected BotConfiguration BotSettings { get; } 
    public abstract BotType BotType { get; }
    public abstract string BotName { get; }

    protected WeatherBot(BotConfiguration botSettings)
    {
        if (botSettings == null)
        {
            throw new ArgumentNullException(nameof(botSettings), "Bot configuration settings cannot be null.");
        }
        else
        {
            BotSettings = botSettings;
        }
        BotSettings.Validate();
    }

    /// <summary>
    /// Method to update the bot with new weather data.
    /// </summary>
    /// <param name="weatherData"></param>
    public void Update(WeatherData weatherData)
    {
        if (!BotSettings.Enabled)
            return;

        if (ShouldActivate(weatherData))
        {
            Activate();
        }
    }

    /// <summary>
    /// Method to determine if the bot should activate based on weather data.
    /// </summary>
    /// <param name="weatherData"></param>
    /// <returns></returns>
    protected abstract bool ShouldActivate(WeatherData weatherData);

    /// <summary>
    /// Method to activate the bot and display its message..
    /// </summary>
    protected void Activate()
    {
        Console.WriteLine($"{BotName} Activated: {BotSettings.Message}");
    }
}