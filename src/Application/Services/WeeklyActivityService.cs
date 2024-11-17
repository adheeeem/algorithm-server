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

        var unitWeeksAccess = new UnitWeeksAccess
        {
            Week1 = true
        };

        var unitStartDate =
            await unitOfWork.UserWeeklyActivityRepository.GetWeeklyActivityStartedDateByUnitNumber(userId, unitNumber);
        var today = DateTime.Today;
        var daysDifference = today.Subtract(unitStartDate.Date).Days;

        switch (daysDifference)
        {
            case <= 14 and > 6:
                unitWeeksAccess.Week2 = true;
                unitWeeksAccess.Week3 = false;
                unitWeeksAccess.Week4 = false;
                return unitWeeksAccess;
            case <= 21 and > 14:
                unitWeeksAccess.Week2 = true;
                unitWeeksAccess.Week3 = true;
                unitWeeksAccess.Week4 = false;
                return unitWeeksAccess;
            case > 21:
                unitWeeksAccess.Week2 = true;
                unitWeeksAccess.Week3 = true;
                unitWeeksAccess.Week4 = true;
                return unitWeeksAccess;
            default:
                return unitWeeksAccess;
        }
    }
}