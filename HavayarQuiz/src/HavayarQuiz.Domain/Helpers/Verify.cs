using System.Runtime.CompilerServices;

namespace HavayarQuiz.Domain.Helpers;

internal static class Verify
{
    public static void Argument(
        bool condition,
        string message,
        [CallerArgumentExpression("condition")] string? conditionExpression = null)
    {
        if (!condition)
            throw new DomainException(message, conditionExpression);
    }
}
