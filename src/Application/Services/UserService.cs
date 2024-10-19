using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Application.Services;

public class UserService(IUserRepository userRepository, ISchoolRepository schoolRepository, IConfiguration configuration)
{
	public async Task<int> Register(CreateUserRequest request)
	{
		if (!(await schoolRepository.CheckIfSchoolExists(request.SchoolId)))
			throw new BadRequestException("School with this id number does not exist.");

		var userDto = new CreateUserDto();

		byte[] salt = new byte[128 / 8];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(salt);
		string hashedPassword = ApplicationUtils.HashPassword(request.Password, salt);

		userDto.DateOfBirth = request.DateOfBirth;
		userDto.Firstname = request.Firstname;
		userDto.Username = request.Username;
		userDto.Lastname = request.Lastname;
		userDto.Email = request.Email;
		userDto.Phone = request.Phone;
		userDto.Grade = request.Grade;
		userDto.SchoolId = request.SchoolId;
		userDto.Gender = request.Gender;
		userDto.PasswordHash = hashedPassword;
		userDto.Salt = Convert.ToBase64String(salt);

		return await userRepository.CreateUser(userDto);
	}

	public async Task<AuthenticationResponse> Login(LoginRequest request)
	{
		var response = new AuthenticationResponse();

		if (!(await userRepository.CheckIfUserExists(request.Username)))
			throw new BadRequestException("invalid username or password");

		var result = await userRepository.GetUserPasswordHashAndSalt(request.Username);

		byte[] saltBytes = Convert.FromBase64String(result.Item2);

		string hashedPassword = ApplicationUtils.HashPassword(request.Password, saltBytes);
		if (string.Compare(result.Item1, hashedPassword) != 0)
			throw new BadRequestException("invalid username or password");

		var user = await userRepository.GetUserByUsername(request.Username);
		response.AccessToken = ApplicationUtils.GenerateJwtToken(configuration, user);
		response.ExpiresAt = 7200000;
		return response;
	}
}
