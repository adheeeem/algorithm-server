using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Application.Storage;

namespace Application.Services;

public class QuestionService(IUnitOfWork unitOfWork, IBlobService blobService)
{
    public async Task<int> AddNewQuestion(CreateQuestionRequest request)
    {
        if (!((request.Grade > 0 && request.Grade < 12) && (request.WeekNumber > 0 && request.WeekNumber < 5) &&
              (request.UnitNumber > 0 && request.UnitNumber < 9)))
            throw new BadRequestException("Invalid request, make sure everything in proper range.");
        var weekId = await unitOfWork.WeekRepository.GetWeekId(request.UnitNumber, request.WeekNumber, request.Grade);
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
        var id = await unitOfWork.QuestionRepository.AddNewQuestion(questionDto);

        return id;
    }

    public async Task<ListedResponse<QuestionFullDto>> GetAllQuestions(int limit, int page, int weekNumber,
        int unitNumber, int userId)
    {
        var paid = await unitOfWork.UserEnrollmentRepository.CheckIfUserPaidForUnit(userId, unitNumber);
        if (!paid)
            throw new BadRequestException("User with this unit number and grade does not exist.");
        var enrolled = await unitOfWork.UserEnrollmentRepository.GetUserEnrollment(userId, unitNumber);
        if (!enrolled.Enrolled)
            throw new BadRequestException("You are not enrolled yet.");
        var startedDate =
            await unitOfWork.UserWeeklyActivityRepository.GetUserWeeklyActivityStartedDateByUnitNumber(userId,
                unitNumber);
        var weekAccess = ApplicationUtils.CalculateWeeksAccess(startedDate.Date);
        if ((weekNumber == 1 && !weekAccess.Week1) || (weekNumber == 2 && !weekAccess.Week2) ||
            (weekNumber == 3 && !weekAccess.Week3) || (weekNumber == 4 && !weekAccess.Week4))
            throw new BadRequestException("You do not have an access to this week.");
        var user = await unitOfWork.UserRepository.GetUserById(userId);
        var questions =
            await unitOfWork.QuestionRepository.GetAllQuestions(limit, page, weekNumber, unitNumber, user.Grade);
        var questionsCount = await unitOfWork.QuestionRepository.GetQuestionCount();
        return new ListedResponse<QuestionFullDto> { Items = questions, Total = questionsCount };
    }

    public async Task DeleteQuestion(int questionId)
    {
        await unitOfWork.QuestionRepository.DeleteQuestion(questionId);
    }

    public async Task UploadQuestionImage(Stream imageStream, string contentType, int questionId)
    {
        if (!(await unitOfWork.QuestionRepository.CheckIfQuestionExists(questionId)))
            throw new RecordNotFoundException("Question with this id does not exist.");
        var imageGuid = await blobService.UploadAsync(imageStream, contentType);
        await unitOfWork.QuestionRepository.SetImageId(questionId, imageGuid);
    }
}