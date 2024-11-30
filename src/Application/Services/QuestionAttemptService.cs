using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Requests;

namespace Application.Services;

public class QuestionAttemptService(IUnitOfWork unitOfWork)
{
    public async Task SubmitQuestionAttempt(List<QuestionAttemptRequest> questionAttempts, int userId)
    {
        if (questionAttempts?.Count == 0 || questionAttempts == null)
            return;
        var unitAndWeekNumber =
            await unitOfWork.QuestionRepository.GetUnitAndWeekNumberByQuestionId(questionAttempts[0].QuestionId);
        var paid = await unitOfWork.UserEnrollmentRepository.CheckIfUserPaidForUnit(userId, unitAndWeekNumber.Item1);
        if (!paid)
            throw new BadRequestException("User with this unit number and grade does not exist.");
        var unitStartDate =
            await unitOfWork.UserWeeklyActivityRepository.GetUserWeeklyActivityStartedDateByUnitNumber(userId,
                unitAndWeekNumber.Item1);
        var weeksAccess = ApplicationUtils.CalculateWeeksAccess(unitStartDate.Date);
        ApplicationUtils.ThrowExceptionIfCannotAccessToWeek(weeksAccess, unitAndWeekNumber.Item2);
        var groupId = Guid.NewGuid();
        var date = DateTime.Now;
        var questionAttemptsDto = questionAttempts.Select(questionAttempt => new QuestionAttemptDto
            {
                UserId = userId,
                SelectedOptionIndex = questionAttempt.SelectedOptionIndex,
                QuestionId = questionAttempt.QuestionId,
                GroupId = groupId,
                Date = date,
            })
            .ToList();

        await unitOfWork.QuestionAttemptRepository.SubmitQuestionAttempt(questionAttemptsDto);
    }
}