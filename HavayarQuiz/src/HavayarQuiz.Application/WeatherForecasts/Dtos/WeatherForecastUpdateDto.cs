namespace HavayarQuiz.Application.WeatherForecasts.Dtos;

public record WeatherForecastUpdateDto(
    Guid Id,
    DateTime Date,
    int TemperatureC,
    string? Summary);
