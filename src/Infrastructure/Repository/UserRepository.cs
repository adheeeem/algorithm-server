using Application.DTOs;
using Application.Interfaces.Repository;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repository;

public class UserRepository(IDbConnection connection, IUserEnrollmentRepository userEnrollmentRepository) : IUserRepository
{
	private const string UserTable = "app_user";
	private const string UserEnrollmentTable = "app_user_enrollment";

	public async Task<bool> CheckIfUserExists(string username)
	{
		var query = $"select 1 from {UserTable} where username = @username";
		int id = await connection.QuerySingleOrDefaultAsync<int>(query, new { username });
		return id == 1;
	}

	public async Task<int> CreateUser(CreateUserDto user)
	{
		connection.Open(); // Use async open
		using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
		try
		{
			const string newUserQuery = $"insert into {UserTable} " +
			                            $"(firstname, lastname, username, phone, grade, school_id, email, dob, gender, password_hash, salt) values " +
			                            $"(@firstname, @lastname, @username, @phone, @grade, @schoolId, @email, @dateOfBirth, @gender, @passwordHash, @salt) " +
			                            $"returning id;";

			// Pass the transaction to the command
			var id = await connection.ExecuteScalarAsync<int>(newUserQuery, user, transaction);
				
			const string userEnrollmentQuery = $"insert into {UserEnrollmentTable} (app_user_id, unit_number, paid) values (@userId, @unitNumber, @isPaid) returning id";
			await connection.ExecuteScalarAsync<int>(userEnrollmentQuery, new { userId=id, unitNumber=1, isPaid=false }, transaction);

			transaction.Commit();
			return id;
		}
		catch (Exception ex)
		{
			transaction.Rollback();
			throw; 
		}
		finally
		{
			connection.Close();
		}
	}

	public async Task<User> GetUserById(int id)
	{
		string query = $"select id, firstname, lastname, username, phone, grade, school_id as schoolId, total_score as totalScore, email, is_active as isActive, dob as dateOfBirth, gender, role from {UserTable} where id=@id;";
		var result = await connection.QueryFirstOrDefaultAsync<User>(query, new { id });
		return result;
	}

	public async Task<User> GetUserByUsername(string username)
	{
		string query = $"select id, firstname, lastname, username, phone, grade, school_id as schoolId, total_score as totalScore, email, dob, gender, role from {UserTable} where username=@username;";
		var result = await connection.QueryFirstOrDefaultAsync<User>(query, new { username });
		return result;
	}

	public async Task<(string, string)> GetUserPasswordHashAndSalt(string username)
	{
		string query = $"select password_hash, salt from {UserTable} where username=@username;";
		var result = await connection.QueryFirstOrDefaultAsync<(string, string)>(query, new { username });
		return result;
	}
}
