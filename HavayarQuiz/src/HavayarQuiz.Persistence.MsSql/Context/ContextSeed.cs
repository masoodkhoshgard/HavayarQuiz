using Microsoft.AspNetCore.Identity;
namespace HavayarQuiz.Persistence.MsSql.Context;

public static class ContextSeed
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole<Guid>(Domain.Consts.Roles.Admin));
        await roleManager.CreateAsync(new IdentityRole<Guid>(Domain.Consts.Roles.BasicUser));
    }
    public static async Task SeedSuperAdminAsync(UserManager<HavayarUser> userManager)
    {
        //Seed Default User
        var defaultUser = new HavayarUser
        {
            UserName = "masoodkhoshgard",
            Email = "masoodkhoshgard@gmail.com",
            FirstName = "Masood",
            LastName = "Khoshgard",
            EmailConfirmed = true,
            BirthDate = new DateTime(1986, 04, 28),
            PhoneNumberConfirmed = true,
            ProfilePicture = new byte[] { }
        };

        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.SetUserNameAsync(defaultUser, defaultUser.UserName);
                await userManager.SetEmailAsync(defaultUser, defaultUser.Email);
                await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                await userManager.AddToRoleAsync(defaultUser, Domain.Consts.Roles.Admin);
                await userManager.AddToRoleAsync(defaultUser, Domain.Consts.Roles.BasicUser);
            }
        }
    }
}
