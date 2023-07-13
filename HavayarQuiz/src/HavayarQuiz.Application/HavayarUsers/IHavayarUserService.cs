using HavayarQuiz.Application.HavayarUsers.Dtos;

namespace HavayarQuiz.Application.HavayarUsers;
public interface IHavayarUserService
{
    Task<string> CreateHavayarUserAsync(HavayarUserCreateDto model, CancellationToken cancellation);
    Task<HavayarUserReturnDto> GetHavayarUserAsync(Guid Id, CancellationToken cancellation);
    Task<IEnumerable<HavayarUserReturnDto>> GetHavayarUserAsync(CancellationToken cancellation);
    Task UpdateHavayarUserAsync(HavayarUserUpdateDto model, CancellationToken cancellation);
    Task RemoveHavayarUserAsync(Guid? userId, CancellationToken cancellation);
}
