using System.ComponentModel.DataAnnotations;

namespace HavayarQuiz.API.Controllers.WeatherForecasts.Contracts;

public record WeatherForecastUpdateModel(
    [Display(Name ="Id")]
    Guid Id,
    DateTime Date,
    int TemperatureC,
    string? Summary);
