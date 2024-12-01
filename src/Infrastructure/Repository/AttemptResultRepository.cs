using System.Data;
using Application.DTOs;
using Application.Interfaces.Repository;
using Dapper;

namespace Infrastructure.Repository;

public class AttemptResultRepository(IDbConnection connection) : IAttemptResultRepository
{
    private const string AttemptResultTable = "attempt_result";

    public async Task AddAttemptResult(Guid attemptGroupId, int correctAnswers)
    {
        const string query =
            $"insert into {AttemptResultTable} (attempt_group_id, correct_answers) values (@attemptGroupId, @correctAnswers)";
        await connection.ExecuteAsync(query, new { attemptGroupId, correctAnswers });
    }

    public async Task<List<MinimalQuestionAttemptResultByWeekAndUnitNumberDto>>
        GetMinimalQuestionAttemptResultsByWeekAndUnitNumber(int weekNumber, int unitNumber, int userId)
    {
        const string query = """
                             select 
                                 auqa.group_id as groupId, 
                                 auqa.app_user_id as userId, 
                                 ar.correct_answers as correctAnswers, 
                                 auqa.date, 
                                 w.number as weekNumber, 
                                 w.unit_number as unitNumber
                             from app_user_question_attempt auqa
                                      inner join attempt_result ar on ar.attempt_group_id = auqa.group_id
                                      inner join question q on auqa.question_id = q.id
                                      inner join week w on w.id = q.week_id
                             where auqa.app_user_id = @userId and w.number = @weekNumber and w.unit_number = @unitNumber
                             group by auqa.group_id, auqa.app_user_id, ar.correct_answers, auqa.date, w.number, w.unit_number
                             order by auqa.date desc
                             """;
        var result =
            await connection.QueryAsync<MinimalQuestionAttemptResultByWeekAndUnitNumberDto>(query,
                new { userId, unitNumber, weekNumber });
        return result.ToList();
    }
}