using ApplicationName.Application.Weather.Queries;
using ApplicationName.Domain.WeatherForecast;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationName.Api.Controllers;

[ApiController]
[Route("/weatherforecast")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get() => await _mediator.Send(new GetWeatherForecastRequest());
}