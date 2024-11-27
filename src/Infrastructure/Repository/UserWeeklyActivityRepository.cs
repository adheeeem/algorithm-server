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

    public async Task<DateTimeOffset> GetUserWeeklyActivityStartedDateByUnitNumber(int userId, int unitNumber)
    {
        const string query = """
                             select auwa.start_date from app_user_weekly_activity auwa 
                                             inner join week w on auwa.week_id = w.id
                                             where w.unit_number = @unitNumber and auwa.app_user_id = @userId
                             """;
        var dateTime = await connection.QueryFirstOrDefaultAsync<DateTime>(query, new { unitNumber, userId });
        return dateTime;
    }
}