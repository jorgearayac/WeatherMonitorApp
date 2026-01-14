using WeatherMonitorApp.Enums;
using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Models;
using WeatherMonitorApp.Structs;

namespace WeatherMonitorApp.Bots;

/// <summary>
/// Sun Bot that activates on high temperature conditions.
/// </summary>
public class SunBot : WeatherBot
{
    public override BotType BotType => BotType.Sun;
    public override string BotName => "SunBot";

    public SunBot(BotConfiguration settings) : base(settings)
    {
        if (settings.BotType != BotType.Sun)
        {
            throw new ArgumentException($"Configuration is for {settings.BotType}, expected {BotType.Sun}");
        }
    }

    protected override bool ShouldActivate(WeatherData weatherData)
    {
        return weatherData.Temperature.Celsius > BotSettings.TemperatureThreshold?.Value;
    }
}