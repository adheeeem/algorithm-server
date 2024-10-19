using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository
{
	Task<int> CreateUser(CreateUserDto user);
	Task<(string, string)> GetUserPasswordHashAndSalt(string username);
	Task<User> GetUserByUsername(string username);
	Task<bool> CheckIfUserExists(string username);
}
