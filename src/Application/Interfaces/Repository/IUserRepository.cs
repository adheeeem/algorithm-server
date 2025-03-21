﻿using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Repository;

public interface IUserRepository
{
	Task<int> CreateUser(CreateUserDto user);
	Task<(string, string)> GetUserPasswordHashAndSalt(string username);
	Task<User> GetUserByUsername(string username);
	Task<User> GetUserById(int id);
	Task<bool> CheckIfUserExists(string username);
}
