using Application.Interfaces;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repository;

public class UserEnrollmentRepository(IDbConnection connection) : IUserEnrollmentRepository
{
	private const string UserEnrollmentTable = "app_user_enrollment";

	public async Task<bool> CheckIfUserPaidForUnit(int userId, int unitNumber)
	{
		string query = $"select 1 from {UserEnrollmentTable} where app_user_id=@userId and unit_number=@unitNumber and paid=TRUE";
		var response = await connection.QueryFirstOrDefaultAsync<int>(query, new { userId, unitNumber });
		return response == 1;
	}

	public async Task<int> CreateUserEnrollment(int userId, int unitNumber, bool isPaid = false)
	{
		string query = $"insert into {UserEnrollmentTable} (app_user_id, unit_number, paid) values (@userId, @unitNumber, @isPaid) returning id";
		var response = await connection.ExecuteScalarAsync<int>(query, new { userId, unitNumber, isPaid });
		return response;
	}

	public async Task EnrollUser(int userId, int unitNumber)
	{
		string query = $"update {UserEnrollmentTable} set enrolled=TRUE where unit_number=@unitNumber and app_user_id=@userId;";
		await connection.ExecuteAsync(query, new { userId, unitNumber });
	}

	public async Task<UserEnrollment> GetUserEnrollment(int userId, int unitNumber)
	{
		string query = $"select enrolled, paid, unit_number as unitNumber, is_completed as isCompleted, date from {UserEnrollmentTable} where app_user_id=@userId and unit_number=@unitNumber";
		var response = await connection.QuerySingleOrDefaultAsync<UserEnrollment>(query, new { userId, unitNumber });
		return response;
	}

	public async Task UpdateUserEnrollmentPaidStatus(int userId, int unitNumber, bool isPaid)
	{
		string query = $"update {UserEnrollmentTable} set paid=@isPaid where unit_number=@unitNumber and app_user_id=@userId;";
		await connection.ExecuteScalarAsync<bool>(query, new { userId, isPaid, unitNumber });
	}


}
