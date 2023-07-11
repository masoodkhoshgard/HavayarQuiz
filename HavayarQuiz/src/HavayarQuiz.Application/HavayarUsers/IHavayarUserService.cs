using HavayarQuiz.Application.HavayarUsers.Dtos;

namespace HavayarQuiz.Application.HavayarUsers;
public interface IHavayarUserService
{
    Task<string> CreateHavayarUserAsync(HavayarUserCreateDto model, CancellationToken cancellation);
    Task<HavayarUserReturnDto> GetHavayarUserAsync(Guid Id, CancellationToken cancellation);
    Task<IEnumerable<HavayarUserReturnDto>> GetHavayarUserAsync(CancellationToken cancellation);
    Task UpdateWeatherForecastAsync(HavayarUserUpdateDto model, CancellationToken cancellation);
    Task RemoveHavayarUserAsync(Guid userId, HavayarUserReturnDto model, CancellationToken cancellation);
}
