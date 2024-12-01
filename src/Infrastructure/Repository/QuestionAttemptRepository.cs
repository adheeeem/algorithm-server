using System.Data;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Dapper;

namespace Infrastructure.Repository;

public class QuestionAttemptRepository(IDbConnection connection) : IQuestionAttemptRepository
{
    private const string QuestionAttemptTable = "app_user_question_attempt";

    public async Task SubmitQuestionAttempt(List<QuestionAttemptDto> questionAttempts)
    {
        connection.Open();
        var transaction = connection.BeginTransaction();
        const string query = $"""
                              insert into {QuestionAttemptTable} (app_user_id, date, question_id, selected_option_index, group_id) 
                              values (@UserId, @Date, @QuestionId, @SelectedOptionIndex, @GroupId)
                              """;
        await connection.ExecuteAsync(query, questionAttempts, transaction: transaction);
        try
        {
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}