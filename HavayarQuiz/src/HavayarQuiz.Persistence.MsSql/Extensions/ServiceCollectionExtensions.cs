using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HavayarQuiz.Persistence.MsSql.Extensions;

public static class ServiceCollectionExtensions
{
    public const string DbConnectionStringName = "HavayarQuizDb";

    public static void RegisterMsSqlPersistenceServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.Secure = CookieSecurePolicy.Always;
        });

        var dbConnectionString = configuration.GetConnectionString(DbConnectionStringName);

        if (dbConnectionString is null || dbConnectionString.Length <= 0)
            throw new ArgumentNullException(nameof(dbConnectionString), "DB connection string cannot be null.");

        services.AddDbContext<HavayarQuizContext>(builder =>
            builder.UseSqlServer(
                dbConnectionString,
                options => options.EnableRetryOnFailure()));

        #region [ Identity ]

        #region snippet_lock
        services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        });
        #endregion

        #region snippet_pw
        services.Configure<IdentityOptions>(options =>
        {
            // Default Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 3;
        });
        #endregion

        #region snippet_si
        services.Configure<IdentityOptions>(options =>
        {
            // Default SignIn settings.
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        });
        #endregion

        #region snippet_user
        services.Configure<IdentityOptions>(options =>
        {
            // Default User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });
        #endregion

        #region snippet_cookie

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/AccessDenied";
            options.Cookie.Name = "HavayarQuiz";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(5);
            options.LoginPath = "/Login";
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration = true;
            options.LogoutPath = "/Logout";
        });

        //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //       .AddCookie(options =>
        //       {
        //           options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        //           options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
        //           options.Cookie.HttpOnly = true;
        //           options.ExpireTimeSpan = TimeSpan.FromSeconds(30);//.FromHours(5);
        //           options.LoginPath = "/Identity/Account/Login";
        //           // ReturnUrlParameter requires 
        //           //using Microsoft.AspNetCore.Authentication.Cookies;
        //           options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        //           options.SlidingExpiration = true;
        //           options.LogoutPath = "/Identity/Account/Logout";
        //       }
        //   ).AddCookie(IdentityConstants.TwoFactorUserIdScheme, options =>
        //   {
        //       options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        //       options.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
        //       options.Cookie.HttpOnly = true;
        //       options.ExpireTimeSpan = TimeSpan.FromSeconds(30);//.FromHours(5);
        //       options.LoginPath = "/Identity/Account/Login";
        //       // ReturnUrlParameter requires 
        //       //using Microsoft.AspNetCore.Authentication.Cookies;
        //       options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        //       options.SlidingExpiration = true;
        //       options.LogoutPath = "/Identity/Account/Logout";
        //   })
        //   .AddCookie(IdentityConstants.ExternalScheme, options =>
        //   {
        //       options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        //       options.Cookie.Name = IdentityConstants.ExternalScheme;
        //       options.Cookie.HttpOnly = true;
        //       options.ExpireTimeSpan = TimeSpan.FromSeconds(30);//.FromHours(5);
        //       options.LoginPath = "/Identity/Account/Login";
        //       // ReturnUrlParameter requires 
        //       //using Microsoft.AspNetCore.Authentication.Cookies;
        //       options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        //       options.SlidingExpiration = true;
        //       options.LogoutPath = "/Identity/Account/Logout";
        //   })
        //   .AddCookie(IdentityConstants.ApplicationScheme, options =>
        //   {
        //       options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        //       options.Cookie.Name = IdentityConstants.ApplicationScheme;
        //       options.Cookie.HttpOnly = true;
        //       options.ExpireTimeSpan = TimeSpan.FromSeconds(30);//.FromHours(5);
        //       options.LoginPath = "/Identity/Account/Login";
        //       // ReturnUrlParameter requires 
        //       //using Microsoft.AspNetCore.Authentication.Cookies;
        //       options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        //       options.SlidingExpiration = true;
        //       options.LogoutPath = "/Identity/Account/Logout";
        //   });

        //services.ConfigureApplicationCookie(options =>
        //{
        //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        //    options.Cookie.Name = "ServerDocumentation";
        //    options.Cookie.HttpOnly = true;
        //    options.ExpireTimeSpan = TimeSpan.FromHours(5);
        //    options.LoginPath = "/Identity/Account/Login";
        //    // ReturnUrlParameter requires 
        //    //using Microsoft.AspNetCore.Authentication.Cookies;
        //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        //    options.SlidingExpiration = true;
        //    options.LogoutPath = "/Identity/Account/Logout";

        //});
        #endregion

        #region snippet_route

        services.AddRazorPages(options => options.Conventions.Add(new HavayarPageRouteModelConvention()));

        #endregion

        #endregion Identity

        services.AddIdentity<HavayarUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<HavayarQuizContext>()
                .AddUserManager<UserManager<HavayarUser>>()
                .AddUserStore<UserStore<HavayarUser, IdentityRole<Guid>, HavayarQuizContext, Guid>>()
                .AddSignInManager<SignInManager<HavayarUser>>()
                .AddDefaultTokenProviders();
        //;

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IHavayarUserRepository, HavayarUserRepository>();
    }

    public static async void SeedDataAsync(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        var context = serviceProvider.GetRequiredService<HavayarQuizContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<HavayarUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        await ContextSeed.SeedRolesAsync(roleManager);
        await ContextSeed.SeedSuperAdminAsync(userManager);
    }
}

public class HavayarPageRouteModelConvention : IPageRouteModelConvention
{
    public void Apply(PageRouteModel model)
    {
        // Specify your custom logic here to modify the route for Identity pages
        foreach (var selector in model.Selectors)
        {
            if (selector.AttributeRouteModel.Template.StartsWith("Identity/Account/"))
            {
                // Add route values as needed
                selector.AttributeRouteModel.Template = selector.AttributeRouteModel.Template.Replace("Identity/Account", "");
                selector.AttributeRouteModel.Name = "HavayarRoute";
            }

            if (selector.AttributeRouteModel.Template.StartsWith("HavayarUsers"))
            {
                // Add route values as needed
                selector.AttributeRouteModel.Template = selector.AttributeRouteModel.Template.Replace("HavayarUsers", "Users");
                selector.AttributeRouteModel.Name = "HavayarUserRoute";
            }
        }
    }
}
