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
		if (!((request.Grade > 0 && request.Grade < 12) && (request.WeekNumber > 0 && request.WeekNumber < 5) && (request.UnitNumber > 0 && request.UnitNumber < 9)))
			throw new BadRequestException("Invalid request, make sure everything in proper range.");
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

	public async Task<ListedResponse<QuestionFullDto>> GetAllQuestions(int limit, int page, int weekNumber, int unitNumber, int grade)
	{
		var questions = await questionRepository.GetAllQuestions(limit, page, weekNumber, unitNumber, grade);
		var questionsCount = await questionRepository.GetQuestionCount();
		return new ListedResponse<QuestionFullDto> { Items = questions, Total = questionsCount };
	}

	public async Task DeleteQuestion(int questionId)
	{
		await questionRepository.DeleteQuestion(questionId);
	}
}
