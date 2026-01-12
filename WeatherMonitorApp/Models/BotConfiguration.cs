namespace WeatherMonitorApp.Models;

public class BotConfiguration
{
    public bool Enabled { get; set; }
    public double? HumidityThreshold { get; set; }
    public double? TemeperatureThreshold { get; set; }
    public string Message { get; set; }
}