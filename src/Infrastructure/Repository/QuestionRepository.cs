using Application.DTOs;
using Application.Interfaces.Repository;
using Dapper;
using System.Data;

namespace Infrastructure.Repository;

public class QuestionRepository(IDbConnection connection) : IQuestionRepository
{
    private const string QuestionTable = "question";
    private const string WeekTable = "week";

    public async Task<int> AddNewQuestion(CreateQuestionDto question)
    {
        const string query = $"insert into {QuestionTable} " +
                             $"(question_tj, question_en, question_ru, " +
                             $"options_tj, options_en, options_ru, answer_id, week_id) " +
                             $"values (@questiontj, @questionen, @questionru, @optionstj, @optionsen, @optionsru, @answerid, @weekid) returning id";

        var id = await connection.ExecuteScalarAsync<int>(query, question);
        return id;
    }

    public async Task<int> GetWeekIdByQuestionId(int questionId)
    {
        const string query =
            $"select week_id from {QuestionTable} where id = @questionId";
        var response = await connection.QuerySingleAsync<int>(query, new { questionId });
        return response;
    }

    public async Task<List<UnitWeekQuestionWithAnswerDto>> GetUnitWeekQuestionsWithAnswers(int weekNumber,
        int unitNumber)
    {
        const string query = $"""
                              select q.id as questionId, q.answer_id as answerId 
                              from {QuestionTable} q inner join {WeekTable} w on w.id = q.week_id
                              where w.number = @weekNumber and w.unit_number = @unitNumber
                              """;
        var result = await connection.QueryAsync<UnitWeekQuestionWithAnswerDto>(query, new { weekNumber, unitNumber });
        return result.ToList();
    }

    public async Task<bool> CheckIfQuestionExists(int questionId)
    {
        const string query = $"select 1 from {QuestionTable} where id=@questionId";
        var id = await connection.QuerySingleOrDefaultAsync<int>(query, new { questionId });
        return id == 1;
    }

    public async Task DeleteQuestion(int id)
    {
        const string query = $"delete from {QuestionTable} where id = @id";
        await connection.ExecuteAsync(query, new { id });
    }

    public async Task<List<QuestionFullDto>> GetAllQuestions(int limit, int page, int weekNumber, int unitNumber,
        int grade)
    {
        var query =
            "select question.id, question_tj as questionTj, question_ru as questionRu, question_en as questionEn, options_tj as optionsTj, options_ru as optionsRu, options_en as optionsEn, answer_id as answerId, grade, unit_number as unitNumber, number as weekNumber, image_id as imageId from question inner join week on week.id = question.week_id ";

        var conditions = new List<string>();
        if (weekNumber != 0) conditions.Add("number = @weekNumber");
        if (unitNumber != 0) conditions.Add("unit_number = @unitNumber");
        if (grade != 0) conditions.Add("grade = @grade");

        if (conditions.Count != 0)
            query += "where " + string.Join(" and ", conditions);

        if (limit != 0)
            if (page != 0)
                query += $" limit {limit} offset {(page - 1) * limit}";
            else
                query += $" limit {limit}";

        var result = await connection.QueryAsync<QuestionFullDto>(query, new { unitNumber, weekNumber, grade });
        return result.ToList();
    }

    public async Task<int> GetQuestionCount()
    {
        const string query = $"select count(*) from {QuestionTable};";
        var cnt = await connection.ExecuteScalarAsync<int>(query);
        return cnt;
    }

    public async Task SetImageId(int questionId, Guid imageId)
    {
        const string query = $"update {QuestionTable} set image_id = @imageId where id = @questionId;";
        await connection.ExecuteAsync(query, new { questionId, imageId });
    }
}