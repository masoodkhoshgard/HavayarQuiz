using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace HavayarQuiz.Persistence.MsSql.Context;

public class HavayarQuizContext : IdentityDbContext<HavayarUser, IdentityRole<Guid>, Guid>
{
    public const string DEFAULT_SCHEMA = "dbo";

    public HavayarQuizContext(DbContextOptions<HavayarQuizContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);

        modelBuilder.Entity<IdentityRole<Guid>>(entity => entity.ToTable(name: "Role"));
        modelBuilder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("UserRoles"));

        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("UserClaims"));

        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("UserLogins"));

        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RoleClaims"));

        modelBuilder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UserTokens"));

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
