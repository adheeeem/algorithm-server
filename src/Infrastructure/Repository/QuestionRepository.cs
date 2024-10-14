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

	public async Task<List<QuestionFullDto>> GetAllQuestions(int limit, int page, int weekNumber, int unitNumber)
	{
		string query = "select question_tj, question_ru, question_en, options_tj, options_ru, options_en, answer_id, grade, unit_number, number from question inner join week on week.id = question.week_id ";

		if (weekNumber != 0 && unitNumber != 0)
			query += "where number=@weekNumber and unit_number=@unitNumber ";
		else if (weekNumber != 0 && unitNumber == 0)
			query += "where number=@weekNumber ";
		else if (weekNumber == 0 && unitNumber != 0)
			query += "where unit_number=@unitNumber ";

		if (limit != 0)
			if (page != 0)
				query += $"limit {limit} offset {(page - 1) * limit}";
			else
				query += $"limit {limit}";

		var result = await connection.QueryAsync<QuestionFullDto>(query, new {unitNumber, weekNumber});
		return result.ToList();
	}

	public async Task<int> GetQuestionCount()
	{
		string query = $"select count(*) from {QuestionTable};";
		int cnt = await connection.ExecuteScalarAsync<int>(query);
		return cnt;
	}
}
