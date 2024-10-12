using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Requests;

namespace Application.Services;

public class WeekService(IWeekRepository weekRepository)
{
	public async Task<int> CreateWeek(CreateWeekRequest request)
	{
		bool check = await weekRepository.CheckIfWeekExists(request.UnitNumber, request.Number);
		if (check)
			throw new RecordAlreadyExistsException("Week with this number and unit number already exists.");
		var week = new CreateWeekDto
		{
			Number = request.Number,
			UnitNumber = request.UnitNumber,
			QuestionsDownloadUrl = request.QuestionsDownloadUrl,
			Grade = request.Grade
		};
		return await weekRepository.CreateWeek(week);
	}
}
