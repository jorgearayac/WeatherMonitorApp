namespace WeatherMonitorApp.Models;

/// <summary>
/// Configuration details file containing multiple bot configurations.
/// </summary>
public class ConfigurationDetailsFile
{
    public Dictionary<string, BotConfiguration> Bots { get; set; } = new();
}