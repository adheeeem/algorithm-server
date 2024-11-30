using Application.DTOs;
using Application.Interfaces.Repository;
using Dapper;
using System.Data;
using Domain.Entities;

namespace Infrastructure.Repository;

public class WeekRepository(IDbConnection connection) : IWeekRepository
{
    private const string WeekTable = "week";

    public async Task<int> CreateWeek(CreateWeekDto week)
    {
        const string query = $"insert into {WeekTable} (number, grade, questions_download_url, unit_number) " +
                             $"values (@Number, @Grade, @QuestionsDownloadUrl, @UnitNumber) returning id";
        var id = await connection.ExecuteScalarAsync<int>(query, week);
        return id;
    }

    public async Task<int> GetWeekId(int unitNumber, int weekNumber, int grade)
    {
        const string query =
            $"select id from {WeekTable} where number=@weekNumber and unit_number=@unitNumber and grade=@grade";
        var id = await connection.QuerySingleOrDefaultAsync<int>(query, new { unitNumber, weekNumber, grade });
        return id;
    }

    public async Task<bool> CheckIfWeekExists(int unitNumber, int weekNumber, int grade)
    {
        const string query =
            $"select 1 from {WeekTable} where number=@weekNumber and unit_number=@unitNumber and grade=@grade";
        var id = await connection.QuerySingleOrDefaultAsync<int>(query, new { unitNumber, weekNumber, grade });
        return id == 1;
    }

    public async Task<Week> GetWeekById(int id)
    {
        const string query = $"""
                              select id, number, grade, questions_download_url as questionDownloadUrl, unit_number as unitNumber
                              from {WeekTable} where id=@id
                              """;
        return await connection.QuerySingleAsync<Week>(query, new { id });
    }
}