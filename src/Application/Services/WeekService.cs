using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Requests;

namespace Application.Services;

public class WeekService(IUnitOfWork unitOfWork)
{
	public async Task<int> CreateWeek(CreateWeekRequest request)
	{
		if (!((request.Grade > 0 && request.Grade < 12) && (request.Number > 0 && request.Number < 5) && (request.UnitNumber > 0 && request.UnitNumber < 9)))
			throw new BadRequestException("Invalid request, make sure everything in proper range.");
		bool check = await unitOfWork.WeekRepository.CheckIfWeekExists(request.UnitNumber, request.Number, request.Grade);
		if (check)
			throw new RecordAlreadyExistsException("Week with this number, grade and unit number already exists.");
		var week = new CreateWeekDto
		{
			Number = request.Number,
			UnitNumber = request.UnitNumber,
			QuestionsDownloadUrl = request.QuestionsDownloadUrl,
			Grade = request.Grade
		};
		return await unitOfWork.WeekRepository.CreateWeek(week);
	}
}
