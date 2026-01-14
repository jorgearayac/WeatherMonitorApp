using WeatherMonitorApp.Enums;
using WeatherMonitorApp.Models;
using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Structs;

namespace WeatherMonitorApp.Bots;

/// <summary>
/// Rain Bot that activates on high humidity conditions.
/// </summary>
public class RainBot : WeatherBot
{
    public override BotType BotType => BotType.Rain;
    public override string BotName => "RainBot";

    public RainBot(BotConfiguration settings) : base(settings)
    {
        if (settings.BotType != BotType.Rain)
        {
            throw new ArgumentException($"Configuration is for {settings.BotType}, expected {BotType.Rain}");
        }
    }

    protected override bool ShouldActivate(WeatherData weatherData)
    {
       return weatherData.Humidity.Percentage > BotSettings.HumidityThreshold?.Value;
    }
}