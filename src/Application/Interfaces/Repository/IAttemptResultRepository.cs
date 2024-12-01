using Application.DTOs;

namespace Application.Interfaces.Repository;

public interface IAttemptResultRepository
{
    Task AddAttemptResult(Guid attemptGroupId, int correctAnswers);
    Task<List<MinimalQuestionAttemptResultByWeekAndUnitNumberDto>> GetMinimalQuestionAttemptResultsByWeekAndUnitNumber(int weekNumber, int unitNumber, int userId);
}