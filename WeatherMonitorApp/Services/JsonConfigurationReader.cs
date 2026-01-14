using System.Text.Json;
using WeatherMonitorApp.Models;
using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Structs;
using WeatherMonitorApp.Enums;

namespace WeatherMonitorApp.Services;

/// <summary>
/// Reads application configuration from a JSON file.
/// </summary>
public class JsonConfigurationReader : IConfigurationReader
{
    /// <summary>
    /// Reads the configuration from the specified JSON file.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public AppConfiguration ReadConfiguration(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Configuration file not found.", filePath);
        }

        var json = File.ReadAllText(filePath);

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        RawConfigurationFile? rawConfig = JsonSerializer.Deserialize<RawConfigurationFile>(json, options);

        if (rawConfig == null)
        {
            throw new InvalidOperationException("Failed to deserialize configuration file.");
        }

        AppConfiguration appConfiguration = new()
        {
            RainBot = ConvertToBotConfiguration(rawConfig.RainBot, BotType.Rain),
            SunBot = ConvertToBotConfiguration(rawConfig.SunBot, BotType.Sun),
            SnowBot = ConvertToBotConfiguration(rawConfig.SnowBot, BotType.Snow)
        };

        appConfiguration.Validate();

        return appConfiguration;
    }

    /// <summary>
    /// Converts a raw Bot Configuration to BotConfiguration type.
    /// </summary>
    /// <param name="rawBot"></param>
    /// <param name="botType"></param>
    /// <returns></returns>
    private static BotConfiguration? ConvertToBotConfiguration(RawBotConfiguration? rawBot, BotType botType)
    {
        if (rawBot == null)
        {
            return null;
        }

        return new BotConfiguration
        {
            BotType = botType,
            Enabled = rawBot.Enabled,
            HumidityThreshold = rawBot.HumidityThreshold.HasValue 
                ? new Threshold(rawBot.HumidityThreshold.Value, "%") 
                : null,
            TemperatureThreshold = rawBot.TemperatureThreshold.HasValue 
                ? new Threshold(rawBot.TemperatureThreshold.Value, "°C") 
                : null,
            Message = rawBot.Message ?? string.Empty
        };
    }

    // Helper classes for deserialization
    
    /// <summary>
    /// Represents the raw structure of the configuration file.
    /// </summary>
    private class RawConfigurationFile
    {
        public RawBotConfiguration? RainBot { get; set; }
        public RawBotConfiguration? SunBot { get; set; }
        public RawBotConfiguration? SnowBot { get; set; }
    }

    /// <summary>
    /// Represents the raw structure of a bot configuration.
    /// </summary>
    private class RawBotConfiguration
    {
        public bool Enabled { get; set; }
        public double? HumidityThreshold { get; set; }
        public double? TemperatureThreshold { get; set; }
        public string? Message { get; set; }
    }
}