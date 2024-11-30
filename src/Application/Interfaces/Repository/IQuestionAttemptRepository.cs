using Application.DTOs;

namespace Application.Interfaces.Repository;

public interface IQuestionAttemptRepository
{
    Task SubmitQuestionAttempt(List<QuestionAttemptDto> questionAttempts);
}