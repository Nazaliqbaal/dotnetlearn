using dotnet_learn.Options;
using dotnet_learn.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOptions<OpenWeatherOptions>()
    .BindConfiguration(OpenWeatherOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHttpClient<IWeatherService, WeatherService>(client =>
{
    var baseUrl = builder.Configuration["OpenWeather:BaseUrl"]!.TrimEnd('/') + "/";
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
