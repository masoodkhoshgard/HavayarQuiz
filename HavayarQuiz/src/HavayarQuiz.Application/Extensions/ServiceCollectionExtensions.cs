using Microsoft.Extensions.DependencyInjection;

namespace HavayarQuiz.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IHavayarUserService, HavayarUserService>();
    }
}
