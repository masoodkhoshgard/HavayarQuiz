namespace HavayarQuiz.Persistence.MsSql.Repositories;
internal class HavayarUserRepository : Repository<HavayarUser, Guid>, IHavayarUserRepository
{
    public HavayarUserRepository(HavayarQuizContext dbContext) : base(dbContext)
    {
    }
}
