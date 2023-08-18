namespace HavayarQuiz.Persistence.MsSql.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly HavayarQuizContext _dbContext;

    public UnitOfWork(HavayarQuizContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CompleteAsync(CancellationToken cancellation) => await _dbContext.SaveChangesAsync(cancellation);
}
