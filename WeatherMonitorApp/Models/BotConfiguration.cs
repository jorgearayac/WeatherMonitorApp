using WeatherMonitorApp.Enums;
using WeatherMonitorApp.Structs;

namespace WeatherMonitorApp.Models;

/// <summary>
/// Configuration settings for a weather monitoring bot.
/// </summary>
public class BotConfiguration
{
    public BotType BotType { get; set; }
    public bool Enabled { get; set; }
    public Threshold? HumidityThreshold { get; set; }
    public Threshold? TemperatureThreshold { get; set; }
    public string? Message { get; set; }

    /// <summary>
    /// Validates the bot configuration.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Message))
        {
            throw new InvalidOperationException("Bot message cannot be null or empty.");
        }
        switch (BotType)
        {
            case BotType.Rain:
                if (!HumidityThreshold.HasValue)
                {
                    throw new InvalidOperationException("RainBot requires a HumidityThreshold.");
                }
                break;
            
            case BotType.Sun:
            // SunBot may not require specific thresholds
            
            case BotType.Snow:
                if (!TemperatureThreshold.HasValue)
                {
                    throw new InvalidOperationException("SnowBot requires a TemperatureThreshold.");
                }
                break;
            
            default:
                throw new InvalidOperationException("Unknown BotType.");
        }
    }
}