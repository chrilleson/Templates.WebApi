using ApplicationName.Domain.WeatherForecast;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationName.Application.Weather.Queries;

public record GetWeatherForecastRequest() : IRequest<IEnumerable<WeatherForecast>>;

public class GetWeatherForecast : IRequestHandler<GetWeatherForecastRequest, IEnumerable<WeatherForecast>>
{
    private readonly ILogger<GetWeatherForecast> _logger;
    private static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    public GetWeatherForecast(ILogger<GetWeatherForecast> logger)
    {
        _logger = logger;
    }

    public Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        Summaries[Random.Shared.Next(Summaries.Length)]
                    ))
                .ToList();
            return Task.FromResult<IEnumerable<WeatherForecast>>(forecast);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong while fetching weather forecast.");
            throw;
        }
    }
}