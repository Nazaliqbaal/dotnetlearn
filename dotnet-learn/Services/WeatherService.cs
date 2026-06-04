using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using dotnet_learn.Models;
using dotnet_learn.Options;
using Microsoft.Extensions.Options;

namespace dotnet_learn.Services
{
    public class WeatherService(
        HttpClient httpClient,
        IOptions<OpenWeatherOptions> options,
        ILogger<WeatherService> logger) : IWeatherService
    {
        private readonly string _apiKey = options.Value.ApiKey;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // Private DTOs matching the OpenWeather API response shape
        private record OWMain(double Temp, [property: JsonPropertyName("feels_like")] double FeelsLike, int Humidity);
        private record OWSys(string Country);
        private record OWWeatherItem(string Description);
        private record OWWind(double Speed);
        private record OWCurrentResponse(string Name, OWMain Main, OWSys Sys, List<OWWeatherItem> Weather, OWWind Wind);
        private record OWForecastItem(long Dt, OWMain Main, List<OWWeatherItem> Weather);
        private record OWCity(string Name, string Country);
        private record OWForecastResponse(OWCity City, List<OWForecastItem> List);

        public async Task<CurrentWeatherResponse?> GetCurrentWeatherAsync(string city, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"weather?q={city}&appid={_apiKey}&units=metric", ct);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning("OpenWeather returned {StatusCode} for city '{City}'", response.StatusCode, city);
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<OWCurrentResponse>(_jsonOptions, ct);
            if (data is null)
                return null;

            return new CurrentWeatherResponse(
                City: data.Name,
                Country: data.Sys.Country,
                TemperatureC: data.Main.Temp,
                TemperatureF: Math.Round(data.Main.Temp * 9 / 5 + 32, 1),
                FeelsLikeC: data.Main.FeelsLike,
                Humidity: data.Main.Humidity,
                Description: data.Weather[0].Description,
                WindSpeed: data.Wind.Speed
            );
        }

        public async Task<ForecastResponse?> GetForecastAsync(string city, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"forecast?q={city}&appid={_apiKey}&units=metric", ct);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning("OpenWeather returned {StatusCode} for city '{City}'", response.StatusCode, city);
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<OWForecastResponse>(_jsonOptions, ct);
            if (data is null)
                return null;

            var items = data.List.Select(entry => new ForecastItem(
                DateTime: DateTimeOffset.FromUnixTimeSeconds(entry.Dt).UtcDateTime,
                TemperatureC: entry.Main.Temp,
                TemperatureF: Math.Round(entry.Main.Temp * 9 / 5 + 32, 1),
                Description: entry.Weather[0].Description,
                Humidity: entry.Main.Humidity
            )).ToList();

            return new ForecastResponse(data.City.Name, data.City.Country, items);
        }
    }
}
