namespace Application.Interfaces.Repository;

public interface IUserWeeklyActivityRepository
{
    Task CreateWeeklyActivity(int userId, int weekId, bool isCompleted = false);
    Task<DateTimeOffset> GetUserWeeklyActivityStartedDateByUnitNumber(int userId, int unitNumber);
}