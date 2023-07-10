namespace HavayarQuiz.Persistence.MsSql.Repositories;

internal class WeatherForecastsRepository : Repository<WeatherForecast, Guid>, IWeatherForecastsRepository
{
    public WeatherForecastsRepository(HavayarQuizContext dbContext) : base(dbContext)
    {
    }
}
