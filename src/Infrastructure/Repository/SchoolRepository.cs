using Application.DTOs;
using Application.Interfaces;
using Dapper;
using System.Data;

namespace Infrastructure.Repository;

public class SchoolRepository(IDbConnection connection) : ISchoolRepository
{
	private const string SchoolTable = "school";

	public async Task<bool> CheckIfSchoolExists(int schoolId)
	{
		var query = $"select 1 from {SchoolTable} where id = @schoolId";
		int id = await connection.QuerySingleOrDefaultAsync<int>(query, new { schoolId });
		return id == 1;
	}

	public async Task<int> CreateSchool(CreateSchoolDto school)
	{
		var result = await connection.ExecuteScalarAsync<int>($"insert into {SchoolTable} (name, region, city, country) values (@name, @region, @city, @country) returning id", school);
		return result;
	}
}
