using Application.DTOs;
using Application.Interfaces;
using Dapper;
using System.Data;

namespace Infrastructure.Repository;

public class SchoolRepository(IDbConnection connection) : ISchoolRepository
{
	private const string SchoolTable = "school";
	public async Task<int> AddNewSchool(CreateSchoolDto school)
	{
		var result = await connection.ExecuteScalarAsync<int>($"insert into {SchoolTable} (name, region, city, country) values (@name, @region, @city, @country) returning id", school);
		return result;
	}
}
