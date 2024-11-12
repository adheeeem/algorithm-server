using Application.DTOs;
using Application.Interfaces.Repository;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repository;

public class SchoolRepository(IDbConnection connection) : ISchoolRepository
{
	private const string SchoolTable = "school";

	public async Task<bool> CheckIfSchoolExists(int schoolId)
	{
		const string query = $"select 1 from {SchoolTable} where id = @schoolId";
		var id = await connection.QuerySingleOrDefaultAsync<int>(query, new { schoolId });
		return id == 1;
	}

	public async Task<int> CreateSchool(CreateSchoolDto school)
	{
		var result = await connection.ExecuteScalarAsync<int>($"insert into {SchoolTable} (name, region, city, country) values (@name, @region, @city, @country) returning id", school);
		return result;
	}

	public async Task<int> GetSchoolCount()
	{
		const string query = $"select count(*) from {SchoolTable};";
		var cnt = await connection.ExecuteScalarAsync<int>(query);
		return cnt;
	}

	public async Task<List<School>> GetSchools(int limit = 0, int page = 0, string name = "", string region = "", string city = "", string country = "")
	{
		var query = $"select * from {SchoolTable} ";

		var conditions = new List<string>();
		if (name.Length != 0)
			conditions.Add("name = @name");
		if (region.Length != 0)
			conditions.Add("region = @region");
		if (city.Length != 0)
			conditions.Add("city = @city");
		if (country.Length != 0)
			conditions.Add("country = @country");

		if (conditions.Count != 0)
			query += "where " + string.Join(" and ", conditions);

		if (limit != 0)
			if (page != 0)
				query += $" limit {limit} offset {(page - 1) * limit}";
			else
				query += $" limit {limit}";

		var result = await connection.QueryAsync<School>(query, new {name, region, city, country, limit, page});
		return result.ToList();
	}
}
