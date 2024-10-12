using Application.DTOs;

namespace Application.Interfaces;

public interface IQuestionRepository
{
	Task<int> AddNewQuestion(AddNewQuestionDto question);
}
