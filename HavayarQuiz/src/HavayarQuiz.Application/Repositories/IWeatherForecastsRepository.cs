using HavayarQuiz.Domain.Entities;

namespace HavayarQuiz.Application.Repositories;

public interface IWeatherForecastsRepository : IRepository<WeatherForecast, Guid>
{

}
