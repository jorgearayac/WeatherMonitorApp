using WeatherMonitorApp.Services;
using WeatherMonitorApp.Parsers;
using WeatherMonitorApp.Interfaces;
using WeatherMonitorApp.Models;
using System.Diagnostics;

namespace WeatherMonitorApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine("Weather Monitor Application Started.");
        Console.WriteLine("-------------------------------------");

        WeatherStation weatherStation = InitializeSystem();

        Console.WriteLine("System ready. Enter weather data below.");
        Console.WriteLine("Supported formats:");
        Console.WriteLine("JSON:\n {\n\"Location\": \"City Name\",\n\"Temperature\": 23.0,\n\"Humidity\": 85.0\n}");
        Console.WriteLine();
        Console.WriteLine("XML:\n <WeatherData>\n   <Location>City</Location>\n   <Temperature>23.0</Temperature>\n   <Humidity>85.0</Humidity>\n</WeatherData>");
        Console.WriteLine();

        // Main loop
        RunMainLoop(weatherStation);

        Console.WriteLine();
        Console.WriteLine("System shutting down. Goodbye!");
    }

    /// <summary>
    /// Initializes the weather monitoring system.
    /// </summary>
    /// <returns></returns>
    private static WeatherStation InitializeSystem()
    {
        // Initialize configuration reader
        Console.WriteLine("Loading configuration...");
        IConfigurationReader configurationReader = new JsonConfigurationReader();

        // Read configuration
        AppConfiguration appConfiguration = configurationReader.ReadConfiguration("appSettings.json");
        Console.WriteLine("Configuration loaded successfully.");
        Console.WriteLine();

        // Create bots
        Console.WriteLine("Creating bots...");
        IBotFactory botFactory = new BotFactory();
        IEnumerable<IWeatherObserver> bots = botFactory.CreateBots(appConfiguration);
        Console.WriteLine($"Created {bots.Count()} bot(s).");

        // Create parsers
        IWeatherDataParser[] parsers = new IWeatherDataParser[]
        {
            new JsonWeatherDataParser(),
            new XmlWeatherDataParser()
        };

        // Create data processor
        WeatherDataProcessor dataProcessor = new WeatherDataProcessor(parsers.ToList());

        // Create weather station
        WeatherStation weatherStation = new WeatherStation();
        foreach (var bot in bots)
        {
            weatherStation.RegisterObserver(bot);
            Console.WriteLine($"Registered bot: {bot.BotName}");
        }

        Console.WriteLine();
        return weatherStation;
    }

    /// <summary>
    /// Runs the main input loop for processing weather data.
    /// </summary>
    /// <param name="weatherStation"></param>
    private static void RunMainLoop(WeatherStation weatherStation)
    {
        IWeatherDataParser[] parsers = new IWeatherDataParser[]
        {
            new JsonWeatherDataParser(),
            new XmlWeatherDataParser()
        };

        WeatherDataProcessor dataProcessor = new WeatherDataProcessor(parsers.ToList());

        while (true)
        {
            Console.WriteLine("Enter weather data (or 'exit' to quit):");
            string? rawData = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(rawData))
            {
                Console.WriteLine("No data entered. Please try again.");
                continue;
            }

            if (rawData.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting application.");
                break;
            }

            ProcesssInput(weatherStation, dataProcessor, rawData);
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Processes the input raw data and notifies observers.
    /// </summary>
    /// <param name="weatherStation"></param>
    /// <param name="dataProcessor"></param>
    /// <param name="rawData"></param>
    private static void ProcesssInput(WeatherStation weatherStation, WeatherDataProcessor dataProcessor, string rawData)
    {
        WeatherData weatherData = dataProcessor.ProcessWeatherData(rawData);

        Console.WriteLine("Processed Weather Data:");
        Console.WriteLine($"Location: {weatherData.Location}");
        Console.WriteLine($"Temperature: {weatherData.Temperature}");
        Console.WriteLine($"Humidity: {weatherData.Humidity}");

        weatherStation.NotifyObservers(weatherData);

    }
}