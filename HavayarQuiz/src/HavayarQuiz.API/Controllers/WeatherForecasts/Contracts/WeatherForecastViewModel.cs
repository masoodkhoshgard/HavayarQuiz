using System.ComponentModel.DataAnnotations;

namespace HavayarQuiz.API.Controllers.WeatherForecasts.Contracts;

public record WeatherForecastViewModel(
    Guid Id,
    DateTime Date,
    int TemperatureC,
    int TemperatureF,
    string? Summary);
