namespace HavayarQuiz.Domain.Common;

public interface IEntity<TId> where TId : struct, IEquatable<TId>, IComparable<TId>
{
    TId Id { get; set; }
}
