using System.ComponentModel.DataAnnotations;

namespace dotnet_learn.Options
{
    public class OpenWeatherOptions
    {
        public const string SectionName = "OpenWeather";

        [Required]
        public string ApiKey { get; init; } = string.Empty;

        [Required]
        public string BaseUrl { get; init; } = string.Empty;
    }
}
