using System.Data;
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
}