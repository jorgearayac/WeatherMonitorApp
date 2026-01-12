using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Models;

namespace WeatherMonitorApp.Bots;

public abstract class WeatherBot : IWeatherObserver
{
    protected BotConfiguration Configuration { get; } 
    public abstract string BotName { get; }
    protected WeatherBot(BotConfiguration configuration)
    {
        if (configuration == null)
        {
            Configuration = new BotConfiguration { Enabled = false };
        }
        else
        {
            Configuration = configuration;
        }
    }

    public void Update(WeatherData weatherData)
    {
        if (!Configuration.Enabled)
        return;

        if (ShouldActivate(weatherData))
        {
            Activate();
        }
    }

    protected abstract bool ShouldActivate(WeatherData data);

    protected void Activate()
    {
        Console.WriteLine($"{BotName} Activated: {Configuration.Message}");
    }
}