using Application.DTOs;
using Application.Interfaces;
using Application.Requests;

namespace Application.Services;

public class QuestionService(IQuestionRepository questionRepository)
{
	public async Task<int> AddNewQuestion(AddNewQuestionRequest request)
	{
		var questionDto = new AddNewQuestionDto
		{
			QuestionEn = request.QuestionEn,
			QuestionRu = request.QuestionRu,
			QuestionTj = request.QuestionTj,
			OptionsEn = request.OptionsEn,
			OptionsRu = request.OptionsRu,
			OptionsTj = request.OptionsTj,
			AnswerId = request.AnswerId,
		};
		int id = await questionRepository.AddNewQuestion(questionDto);

		return id;
	}
}
