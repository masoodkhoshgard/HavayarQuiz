using HavayarQuiz.Application.HavayarUsers.Dtos;
using HavayarQuiz.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HavayarQuiz.Application.HavayarUsers;

[Authorize(Roles = Domain.Consts.Roles.Admin)]
public class HavayarUserService : IHavayarUserService
{
    private readonly IHavayarUserRepository _havayarUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly UserManager<HavayarUser> _userManager;
    private readonly IUserStore<HavayarUser> _userStore;
    private readonly IUserEmailStore<HavayarUser> _emailStore;

    public HavayarUserService(IHavayarUserRepository havayarUserRepository,
                              IUnitOfWork unitOfWork,
                              UserManager<HavayarUser> userManager,
                              IUserStore<HavayarUser> userStore)
    {
        _havayarUserRepository = havayarUserRepository;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
    }

    public async Task<Guid> CreateHavayarUserAsync(HavayarUserCreateDto model, CancellationToken cancellation)
    {
        try
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
            //var masood = await _userManager.FindByNameAsync("masoodkhoshgard");
            //masood.PhoneNumber = "09125658957";
            //masood = await _userManager.FindByNameAsync("masoodkhoshgard");
            //masood.PhoneNumber = "09125658957";
            //masood = await _userManager.FindByNameAsync("masoodkhoshgard");
            //masood.PhoneNumber = "09125658957";
            //var res = await _userStore.UpdateAsync(masood, cancellation);
            await _userStore.SetUserNameAsync(user, model.Username, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                foreach (var role in model.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role.ToString());
                }

                return Guid.Parse(await _userManager.GetUserIdAsync(user));
            }
            else
            {
                throw new DomainException(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private IUserEmailStore<HavayarUser> GetEmailStore()
    {
        return !_userManager.SupportsUserEmail
            ? throw new NotSupportedException("The default UI requires a user store with email support.")
            : (IUserEmailStore<HavayarUser>)_userStore;
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
