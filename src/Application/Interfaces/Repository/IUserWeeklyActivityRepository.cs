namespace Application.Interfaces.Repository;

public interface IUserWeeklyActivityRepository
{
    Task CreateWeeklyActivity(int userId, int weekId, bool isCompleted = false);
}