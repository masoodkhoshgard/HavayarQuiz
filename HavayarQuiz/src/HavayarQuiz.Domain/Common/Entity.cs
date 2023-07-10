namespace HavayarQuiz.Domain.Common;

public class Entity<TId> : IEntity<TId>
    where TId : struct, IEquatable<TId>, IComparable<TId>
{
    public virtual TId Id { get; set; }
}
