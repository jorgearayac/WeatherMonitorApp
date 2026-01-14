using WeatherMonitorApp.Enums;
using WeatherMonitorApp.Models;
using WeatherMonitorApp.Interfaces;

namespace WeatherMonitorApp.Bots;

/// <summary>
/// Snow that activates on low temperature conditions.
/// </summary>
public class SnowBot : WeatherBot
{
    public override BotType BotType => BotType.Snow;
    public override string BotName => "SnowBot";

    public SnowBot(BotConfiguration settings) : base(settings)
    {
        if (settings.BotType != BotType.Snow)
        {
            throw new ArgumentException($"Configuration is for {settings.BotType}, expected {BotType.Snow}");
        }
    }

    protected override bool ShouldActivate(WeatherData weatherData)
    {
        return weatherData.Temperature.Celsius < BotSettings.TemperatureThreshold?.Value;
    }
}