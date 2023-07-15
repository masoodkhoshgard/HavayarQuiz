using HavayarQuiz.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HavayarQuiz.Application.HavayarUsers;

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

    public async Task<string> CreateHavayarUserAsync(HavayarUserCreateDto model, CancellationToken cancellation)
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
        await _userStore.SetUserNameAsync(user, model.Username, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            foreach (var role in model.Roles)
            {
                await _userManager.AddToRoleAsync(user, role.ToString());
            }

            await _unitOfWork.CompleteAsync(cancellation);
            //return Task.CompletedTask;
            return await _userManager.GetUserIdAsync(user);
        }
        else
        {
            throw new DomainException(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
        }
    }

    private IUserEmailStore<HavayarUser> GetEmailStore()
    {
        return !_userManager.SupportsUserEmail
            ? throw new NotSupportedException("The default UI requires a user store with email support.")
            : (IUserEmailStore<HavayarUser>)_userStore;
    }

    public async Task<HavayarUserReturnDto> GetHavayarUserAsync(Guid Id, CancellationToken cancellation)
    {
        var user = await _havayarUserRepository.GetAsync(Id, cancellation);
        var roles = await _userManager.GetRolesAsync(user);
        return new HavayarUserReturnDto(user.Id, user.Email, user.UserName, user.FirstName, user.LastName, user.BirthDate, user.ProfilePicture, roles);

    }

    public async Task<IEnumerable<HavayarUserReturnDto>> GetHavayarUserAsync(CancellationToken cancellation)
    {
        var users = await _havayarUserRepository.GetAllAsync(cancellation);
        var UserReturnDto = users.Select(async model => new HavayarUserReturnDto(model.Id, model.Email, model.UserName, model.FirstName, model.LastName, model.BirthDate, model.ProfilePicture, await _userManager.GetRolesAsync(model))).Select(c => c.Result);
        return UserReturnDto;

    }

    public async Task UpdateHavayarUserAsync(HavayarUserUpdateDto model, CancellationToken cancellation)
    {
        var user = await _userManager.FindByIdAsync(model.Id.ToString()) ?? throw new DomainException("User not found");
        user.Email = model.Email;
        user.UserName = model.Username;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.BirthDate = model.BirthDate;
        user.ProfilePicture = model.ProfilePicture ?? user.ProfilePicture;
        await _userStore.SetUserNameAsync(user, model.Username, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                throw new DomainException(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
            }

            result = await _userManager.AddToRolesAsync(user, model.Roles.Select(x => x.ToString()));
            if (!result.Succeeded)
            {
                throw new DomainException(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
            }

            await _unitOfWork.CompleteAsync(cancellation);
        }
        else
        {
            throw new DomainException(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
        }
    }

    public async Task RemoveHavayarUserAsync(Guid? userId, CancellationToken cancellation)
    {
        var user = await _userManager.FindByIdAsync(userId?.ToString()) ?? throw new DomainException("User not found");

        if (await _userManager.IsInRoleAsync(user, Domain.Consts.Roles.Admin))
            throw new DomainException($"{Domain.Consts.Roles.Admin} user can't delete");

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            throw new DomainException(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
        }
    }
}
