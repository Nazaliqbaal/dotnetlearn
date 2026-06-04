namespace dotnet_learn.Models
{
    public record CurrentWeatherResponse(
        string City,
        string Country,
        double TemperatureC,
        double TemperatureF,
        double FeelsLikeC,
        int Humidity,
        string Description,
        double WindSpeed
    );

    public record ForecastItem(
        DateTime DateTime,
        double TemperatureC,
        double TemperatureF,
        string Description,
        int Humidity
    );

    public record ForecastResponse(
        string City,
        string Country,
        List<ForecastItem> Forecast
    );
}
