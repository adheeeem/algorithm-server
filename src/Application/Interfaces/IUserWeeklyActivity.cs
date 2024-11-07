namespace Application.Interfaces;

public interface IUserWeeklyActivity
{
    Task CreateWeeklyActivity(int userId, int weekId, bool isCompleted = false);
}