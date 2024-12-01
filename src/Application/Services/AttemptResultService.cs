using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;

namespace Application.Services;

public class AttemptResultService(IUnitOfWork unitOfWork)
{
    public async Task<List<MinimalQuestionAttemptResultByWeekAndUnitNumberResponse>> GetUserWeekUnitAttemptResults(
        int weekNumber,
        int unitNumber, int userId)
    {
        var paid = await unitOfWork.UserEnrollmentRepository.CheckIfUserPaidForUnit(userId, unitNumber);
        if (!paid)
            throw new BadRequestException("User with this unit number and grade does not exist.");
        var unitStartDate =
            await unitOfWork.UserWeeklyActivityRepository.GetUserWeeklyActivityStartedDateByUnitNumber(userId,
                unitNumber);
        var weeksAccess = ApplicationUtils.CalculateWeeksAccess(unitStartDate.Date);
        ApplicationUtils.ThrowExceptionIfCannotAccessToWeek(weeksAccess, weekNumber);

        var userGrade = (await unitOfWork.UserRepository.GetUserById(userId)).Grade;
        var numberOfQuestions =
            await unitOfWork.QuestionRepository.GetWeekUnitQuestionCountByGrade(weekNumber, unitNumber, userGrade);
        var results =
            await unitOfWork.AttemptResultRepository.GetMinimalQuestionAttemptResultsByWeekAndUnitNumber(weekNumber,
                unitNumber, userId);

        return results.Select(r => new MinimalQuestionAttemptResultByWeekAndUnitNumberResponse
        {
            CorrectAnswers = r.CorrectAnswers,
            Date = r.Date,
            NumberOfQuestions = numberOfQuestions
        }).ToList();
    }
}