using Application.DTOs;

namespace Application.Interfaces;

public interface IQuestionRepository
{
	Task<int> AddNewQuestion(CreateQuestionDto question);
	Task<List<QuestionFullDto>> GetAllQuestions(int limit, int page, int weekNumber, int unitNumber, int grade);
	Task<int> GetQuestionCount();
	Task DeleteQuestion(int id);
	Task SetImageId(int questionId, Guid imageId);
	Task<bool> CheckIfQuestionExists(int questionId);
}
