namespace WeatherMonitorApp.Models;

/// <summary>
/// Application configuration containing type of bot and their settings.
/// </summary>
public class AppConfiguration
{
    public BotConfiguration? RainBot { get; set; }
    public BotConfiguration? SunBot { get; set; }
    public BotConfiguration? SnowBot { get; set; }

    /// <summary>
    /// Validates the application configuration.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public void Validate()
    {
        if ((RainBot == null) && (SunBot == null) && (SnowBot == null))
        {
            throw new InvalidOperationException("At least one bot configuration must be provided.");
        }
        RainBot?.Validate();
        SunBot?.Validate();
        SnowBot?.Validate();
    }
}