using Application.DTOs;
using Application.Interfaces;
using Application.Requests;

namespace Application.Services;

public class QuestionService(IQuestionRepository questionRepository, IWeekRepository weekRepository)
{
	public async Task<int> AddNewQuestion(CreateQuestionRequest request)
	{
		int weekId = await weekRepository.GetWeekId(request.UnitNumber, request.WeekNumber);
		var questionDto = new CreateQuestionDto
		{
			QuestionEn = request.QuestionEn,
			QuestionRu = request.QuestionRu,
			QuestionTj = request.QuestionTj,
			OptionsEn = request.OptionsEn,
			OptionsRu = request.OptionsRu,
			OptionsTj = request.OptionsTj,
			AnswerId = request.AnswerId,
			WeekId = weekId,
		};
		int id = await questionRepository.AddNewQuestion(questionDto);

		return id;
	}
}
