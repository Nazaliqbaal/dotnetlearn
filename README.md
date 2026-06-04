# dotnet-learn

An ASP.NET Core Web API that fetches real-time weather data from the [OpenWeather API](https://openweathermap.org/api).

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A free OpenWeather API key — sign up at [openweathermap.org](https://openweathermap.org/appid)

## Setup

**1. Clone the repository**

```bash
git clone https://github.com/Nazaliqbaal/dotnetlearn.git
cd dotnetlearn
```

**2. Add your OpenWeather API key via User Secrets**

```bash
dotnet user-secrets set "OpenWeather:ApiKey" "your-api-key-here" --project dotnet-learn/dotnet-learn.csproj
```

**3. Run the app**

```bash
dotnet run --project dotnet-learn/dotnet-learn.csproj
```

## Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/weather/{city}` | Current weather for a city |
| GET | `/weather/{city}/forecast` | 5-day forecast for a city |

### Examples

```
GET http://localhost:5132/weather/London
GET http://localhost:5132/weather/London/forecast
```

## Project Structure

```
dotnet-learn/
├── Controllers/
│   └── WeatherController.cs      # Route handlers
├── Models/
│   └── WeatherModels.cs          # Response DTOs
├── Options/
│   └── OpenWeatherOptions.cs     # Strongly-typed config
└── Services/
    ├── IWeatherService.cs        # Service interface
    └── WeatherService.cs         # OpenWeather API integration
```
