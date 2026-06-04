using dotnet_learn.Models;

namespace dotnet_learn.Services
{
    public interface IWeatherService
    {
        Task<CurrentWeatherResponse?> GetCurrentWeatherAsync(string city, CancellationToken ct = default);
        Task<ForecastResponse?> GetForecastAsync(string city, CancellationToken ct = default);
    }
}
