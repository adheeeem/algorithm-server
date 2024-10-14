using Application.DTOs;

namespace Application.Interfaces;

public interface IQuestionRepository
{
	Task<int> AddNewQuestion(CreateQuestionDto question);
	Task<List<QuestionFullDto>> GetAllQuestions(int limit, int page, int weekNumber, int unitNumber);
	Task<int> GetQuestionCount();
}
