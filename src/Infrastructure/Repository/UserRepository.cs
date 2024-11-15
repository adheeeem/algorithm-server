using Application.DTOs;
using Application.Interfaces.Repository;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repository;

public class UserRepository(IDbConnection connection, IUserEnrollmentRepository userEnrollmentRepository) : IUserRepository
{
	private const string UserTable = "app_user";

	public async Task<bool> CheckIfUserExists(string username)
	{
		const string query = $"select 1 from {UserTable} where username = @username";
		var id = await connection.QuerySingleOrDefaultAsync<int>(query, new { username });
		return id == 1;
	}

	public async Task<int> CreateUser(CreateUserDto user)
	{
		const string newUserQuery = $"insert into {UserTable} " +
		                            $"(firstname, lastname, username, phone, grade, school_id, email, dob, gender, password_hash, salt) values " +
		                            $"(@firstname, @lastname, @username, @phone, @grade, @schoolId, @email, @dateOfBirth, @gender, @passwordHash, @salt) " +
		                            $"returning id;";
		var id = await connection.ExecuteScalarAsync<int>(newUserQuery, user);
		return id;
	}

	public async Task<User> GetUserById(int id)
	{
		const string query = $"select id, firstname, lastname, username, phone, grade, school_id as schoolId, total_score as totalScore, email, is_active as isActive, dob as dateOfBirth, gender, role from {UserTable} where id=@id;";
		var result = await connection.QueryFirstOrDefaultAsync<User>(query, new { id });
		return result;
	}

	public async Task<User> GetUserByUsername(string username)
	{
		const string query = $"select id, firstname, lastname, username, phone, grade, school_id as schoolId, total_score as totalScore, email, dob, gender, role from {UserTable} where username=@username;";
		var result = await connection.QueryFirstOrDefaultAsync<User>(query, new { username });
		return result;
	}

	public async Task<(string, string)> GetUserPasswordHashAndSalt(string username)
	{
		const string query = $"select password_hash, salt from {UserTable} where username=@username;";
		var result = await connection.QueryFirstOrDefaultAsync<(string, string)>(query, new { username });
		return result;
	}
}
