using HavayarQuiz.Domain.Enums;
using HavayarQuiz.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HavayarQuiz.Persistence.MsSql.Repositories;
internal class HavayarUserRepository : Repository<HavayarUser, Guid>, IHavayarUserRepository
{
    private readonly SignInManager<HavayarUser> _signInManager;
    private readonly UserManager<HavayarUser> _userManager;
    private readonly IUserStore<HavayarUser> _userStore;
    private readonly IUserEmailStore<HavayarUser> _emailStore;
    public HavayarUserRepository(HavayarQuizContext dbContext, UserManager<HavayarUser> userManager,
        IUserStore<HavayarUser> userStore,
        SignInManager<HavayarUser> signInManager) : base(dbContext)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _signInManager = signInManager;
    }
    public override Task AddAsync(HavayarUser entity, CancellationToken cancellation = default) => throw new NotImplementedException();
    public async Task<string> AddAsync(HavayarUser entity, List<Roles> roles, string password, CancellationToken cancellation = default)
    {
        var user = CreateUser();

        await _userStore.SetUserNameAsync(user, entity.UserName, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, entity.Email, CancellationToken.None);

        foreach (var role in roles)
        {
            await _userManager.AddToRoleAsync(user, role.ToString());
        }

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {

            return await _userManager.GetUserIdAsync(user);
        }
        else
        {
            throw new DomainException(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
        }
    }

    private HavayarUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<HavayarUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(HavayarUser)}'. " +
                $"Ensure that '{nameof(HavayarUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    private IUserEmailStore<HavayarUser> GetEmailStore()
    {
        return !_userManager.SupportsUserEmail
            ? throw new NotSupportedException("The default UI requires a user store with email support.")
            : (IUserEmailStore<HavayarUser>)_userStore;
    }
}
