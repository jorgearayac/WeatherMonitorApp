# Weather Monitoring and Reporting Service
C# application that simulates a real-time weather monitoring and reporting service. 

## Supported Input Formats
JSON Format:
```.json

{
  "Location": "City Name",
  "Temperature": 23.0,
  "Humidity": 85.0
}
```
XML Format:
```.xml
<WeatherData>
  <Location>City Name</Location>
  <Temperature>23.0</Temperature>
  <Humidity>85.0</Humidity>
</WeatherData>
```

## Types of Weather Bots
- RainBot
- SunBot
- SnowBot

## How to Interact with the Application
User starts the application, the system prompts: `Enter weather data:`.

User enters data in JSON format: \
`{"Location": "City Name", "Temperature": 32, "Humidity": 40}` \
or XML format: \
`<WeatherData><Location>City Name</Location><Temperature>32</Temperature><Humidity>40</Humidity></WeatherData>` \

## Example of Application Flow
User input: \
`{"Location": "City Name", "Temperature": 23.0, "Humidity": 85.0}` \
\
Output: 
```
Processed Weather Data:
Location: City Name
Temperature: 23°C
Humidity: 85%
RainBot Activated: It looks like it's about to pour down!
```