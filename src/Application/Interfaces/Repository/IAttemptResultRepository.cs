namespace Application.Interfaces.Repository;

public interface IAttemptResultRepository
{
    Task AddAttemptResult(Guid attemptGroupId, int correctAnswers);
}