namespace HavayarQuiz.Persistence.MsSql.Context.EntitiesConfigurations;
internal class HavayarUserEntityConfiguration : IEntityTypeConfiguration<HavayarUser>
{
    public void Configure(EntityTypeBuilder<HavayarUser> builder)
    {
        builder.ToTable(name: "User");
    }
}
