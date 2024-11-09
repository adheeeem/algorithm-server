using Application.DTOs;
using Application.Interfaces.Repository;
using Dapper;
using System.Data;

namespace Infrastructure.Repository;

public class WeekRepository(IDbConnection connection) : IWeekRepository
{
	private const string WeekTable = "week";

	public async Task<int> CreateWeek(CreateWeekDto week)
	{
		var query = $"insert into {WeekTable} (number, grade, questions_download_url, unit_number) " +
			$"values (@Number, @Grade, @QuestionsDownloadUrl, @UnitNumber) returning id";
		int id = await connection.ExecuteScalarAsync<int>(query, week);
		return id;
	}

	public async Task<int> GetWeekId(int unitNumber, int weekNumber, int grade)
	{
		var query = $"select id from {WeekTable} where number=@weekNumber and unit_number=@unitNumber and grade=@grade";
		int id = await connection.QuerySingleOrDefaultAsync<int>(query, new { unitNumber, weekNumber, grade });
		return id;
	}

	public async Task<bool> CheckIfWeekExists(int unitNumber, int weekNumber, int grade)
	{
		var query = $"select 1 from {WeekTable} where number=@weekNumber and unit_number=@unitNumber and grade=@grade";
		int id = await connection.QuerySingleOrDefaultAsync<int>(query, new { unitNumber, weekNumber, grade });
		return id == 1;
	}
}
