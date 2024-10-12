using Application.DTOs;
using Application.Interfaces;
using Dapper;
using System.Data;

namespace Infrastructure.Repository;

public class QuestionRepository(IDbConnection connection) : IQuestionRepository
{
	private const string QuestionTable = "question";
	public async Task<int> AddNewQuestion(AddNewQuestionDto question)
	{
		string query = $"insert into {QuestionTable} " +
			$"(question_tj, question_en, question_ru, " +
			$"options_tj, options_en, options_ru, answer_id) " +
			$"values (@questiontj, @questionen, @questionru, @optionstj, @optionsen, @optionsru, @answerid) returning id";

		int id = await connection.ExecuteScalarAsync<int>(query, question);
		return id;
	}
}
