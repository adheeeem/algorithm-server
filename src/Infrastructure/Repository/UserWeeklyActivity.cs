using System.Data;
using Application.Interfaces;
using Dapper;

namespace Infrastructure.Repository;

public class UserWeeklyActivity(IDbConnection connection) : IUserWeeklyActivity
{
    private const string AppUserWeeklyActivity = "app_user_weekly_activity";
    public async Task CreateWeeklyActivity(int userId, int weekId, bool isCompleted = false)
    {
        var query = $"insert into {AppUserWeeklyActivity} (app_user_id, week_id, is_completed) values (@userId, @weekId, @isCompleted);";
        await connection.ExecuteAsync(query, new {userId, weekId, isCompleted });
    }
}