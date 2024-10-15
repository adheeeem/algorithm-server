using Application.DTOs;
using Application.Interfaces;
using Dapper;
using System.Data;

namespace Infrastructure.Repository;

public class QuestionRepository(IDbConnection connection) : IQuestionRepository
{
	private const string QuestionTable = "question";
	public async Task<int> AddNewQuestion(CreateQuestionDto question)
	{
		string query = $"insert into {QuestionTable} " +
			$"(question_tj, question_en, question_ru, " +
			$"options_tj, options_en, options_ru, answer_id, week_id) " +
			$"values (@questiontj, @questionen, @questionru, @optionstj, @optionsen, @optionsru, @answerid, @weekid) returning id";

		int id = await connection.ExecuteScalarAsync<int>(query, question);
		return id;
	}

	public async Task DeleteQuestion(int id)
	{
		string query = $"delete from {QuestionTable} where id = @id";
		await connection.ExecuteAsync(query, new { id });
	}

	public async Task<List<QuestionFullDto>> GetAllQuestions(int limit, int page, int weekNumber, int unitNumber, int grade)
	{
		string query = "select question.id, question_tj as questionTj, question_ru as questionRu, question_en as questionEn, options_tj as optionsTj, options_ru as optionsRu, options_en as optionsEn, answer_id as answerId, grade, unit_number as unitNumber, number as weekNumber from question inner join week on week.id = question.week_id ";

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
		string query = $"select count(*) from {QuestionTable};";
		int cnt = await connection.ExecuteScalarAsync<int>(query);
		return cnt;
	}
}
