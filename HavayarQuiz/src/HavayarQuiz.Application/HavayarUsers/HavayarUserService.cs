using HavayarQuiz.Application.HavayarUsers.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace HavayarQuiz.Application.HavayarUsers;

[Authorize(Roles = Domain.Consts.Roles.Admin)]
public class HavayarUserService : IHavayarUserService
{
    private readonly IHavayarUserRepository _havayarUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HavayarUserService(IHavayarUserRepository havayarUserRepository, IUnitOfWork unitOfWork)
    {
        _havayarUserRepository = havayarUserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateHavayarUserAsync(HavayarUserCreateDto model, CancellationToken cancellation)
    {

        var user = new HavayarUser
        {
            Email = model.Email,
            UserName = model.Username,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            ProfilePicture = model.ProfilePicture
        };

        return Guid.Parse(await _havayarUserRepository.AddAsync(user, model.Roles, model.Password, cancellation));
    }

    //[AllowAnonymous]
    public async Task<HavayarUserReturnDto> GetHavayarUserAsync(Guid Id, CancellationToken cancellation)
    {
        var user = await _havayarUserRepository.GetAsync(Id, cancellation);
        return new HavayarUserReturnDto(user.Id, user.Email, user.UserName, user.FirstName, user.LastName, user.BirthDate, user.ProfilePicture);

    }

    public async Task<IEnumerable<HavayarUserReturnDto>> GetHavayarUserAsync(CancellationToken cancellation)
    {
        var users = await _havayarUserRepository.GetAllAsync(cancellation);
        return users.Select(model => new HavayarUserReturnDto(model.Id, model.Email, model.UserName, model.FirstName, model.LastName, model.BirthDate, model.ProfilePicture)).ToList();

    }

    public Task UpdateWeatherForecastAsync(HavayarUserUpdateDto model, CancellationToken cancellation) => throw new NotImplementedException();

    public Task RemoveHavayarUserAsync(Guid userId, HavayarUserReturnDto model, CancellationToken cancellation) => throw new NotImplementedException();
}
