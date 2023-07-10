namespace HavayarQuiz.Application.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellation);
}
