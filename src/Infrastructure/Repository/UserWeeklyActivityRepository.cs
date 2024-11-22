using System.Data;
using Application.Interfaces.Repository;
using Dapper;

namespace Infrastructure.Repository;

public class UserWeeklyActivityRepository(IDbConnection connection) : IUserWeeklyActivityRepository
{
    private const string AppUserWeeklyActivity = "app_user_weekly_activity";

    public async Task CreateWeeklyActivity(int userId, int weekId, bool isCompleted = false)
    {
        const string query =
            $"insert into {AppUserWeeklyActivity} (app_user_id, week_id, is_completed) values (@userId, @weekId, @isCompleted);";
        await connection.ExecuteAsync(query, new { userId, weekId, isCompleted });
    }

    public async Task<DateTimeOffset> GetWeeklyActivityStartedDateByUnitNumber(int userId, int unitNumber)
    {
        const string query =
            $"select start_date from {AppUserWeeklyActivity} where unit_number = @unitNumber and app_user_id = @userId;";
        var dateTime = await connection.QueryFirstOrDefaultAsync<DateTimeOffset>(query, new { unitNumber, userId });
        return dateTime;
    }
}