using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Requests;
using Application.Responses;

namespace Application.Services;

public class QuestionService(IQuestionRepository questionRepository, IWeekRepository weekRepository)
{
	public async Task<int> AddNewQuestion(CreateQuestionRequest request)
	{
		int weekId = await weekRepository.GetWeekId(request.UnitNumber, request.WeekNumber, request.Grade);
		if (weekId == 0)
			throw new RecordNotFoundException("Week with this unit number and grade does not exist");
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

	public async Task<ListedResponse<QuestionFullDto>> GetAllQuestions(int limit, int page, int weekNumber, int unitNumber)
	{
		var questions = await questionRepository.GetAllQuestions(limit, page, weekNumber, unitNumber);
		var questionsCount = await questionRepository.GetQuestionCount();
		return new ListedResponse<QuestionFullDto> { Items = questions, Total = questionsCount };
	}
}
