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
        var weekId = await unitOfWork.QuestionRepository.GetWeekIdByQuestionId(questionAttempts[0].QuestionId);
        var week = await unitOfWork.WeekRepository.GetWeekById(weekId);
        var paid = await unitOfWork.UserEnrollmentRepository.CheckIfUserPaidForUnit(userId, week.UnitNumber);
        if (!paid)
            throw new BadRequestException("User with this unit number and grade does not exist.");
        var unitStartDate =
            await unitOfWork.UserWeeklyActivityRepository.GetUserWeeklyActivityStartedDateByUnitNumber(userId,
                week.UnitNumber);
        var weeksAccess = ApplicationUtils.CalculateWeeksAccess(unitStartDate.Date);
        ApplicationUtils.ThrowExceptionIfCannotAccessToWeek(weeksAccess, week.Number);
        var groupId = Guid.NewGuid();
        var date = DateTime.Now;
        var unitWeekQuestionAnswers =
            await unitOfWork.QuestionRepository.GetUnitWeekQuestionsWithAnswers(week.Number, week.UnitNumber);
        var questionAttemptsDto = questionAttempts.Select(questionAttempt => new QuestionAttemptDto
            {
                UserId = userId,
                SelectedOptionIndex = questionAttempt.SelectedOptionIndex,
                QuestionId = questionAttempt.QuestionId,
                GroupId = groupId,
                Date = date,
            })
            .ToList();

        var correctAnswers = questionAttemptsDto.Count(questionAttempt =>
            unitWeekQuestionAnswers.Any(q =>
                q.QuestionId == questionAttempt.QuestionId && q.AnswerId == questionAttempt.SelectedOptionIndex));

        try
        {
            await unitOfWork.QuestionAttemptRepository.SubmitQuestionAttempt(questionAttemptsDto);
            await unitOfWork.AttemptResultRepository.AddAttemptResult(groupId, correctAnswers);
            unitOfWork.Commit();
        }
        catch
        {
            unitOfWork.Rollback();
        }
    }
}