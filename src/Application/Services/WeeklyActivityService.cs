using Application.Interfaces;
using Application.Responses;

namespace Application.Services;

public class WeeklyActivityService(IUnitOfWork unitOfWork)
{
    public async Task<UnitWeeksAccess> GetUnitWeeksAccess(int userId, int unitNumber)
    {
        var isPaid = await unitOfWork.UserEnrollmentRepository.CheckIfUserPaidForUnit(userId, unitNumber);
        if (!isPaid)
        {
            var response = new UnitWeeksAccess();
            return response;
        }
        var unitStartDate =
            await unitOfWork.UserWeeklyActivityRepository.GetUserWeeklyActivityStartedDateByUnitNumber(userId, unitNumber);
        return ApplicationUtils.CalculateWeeksAccess(unitStartDate.Date);
    }
}