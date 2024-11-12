using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Application.Services;

public class UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
{
	public async Task<int> Register(CreateUserRequest request)
	{
		if (!(await unitOfWork.SchoolRepository.CheckIfSchoolExists(request.SchoolId)))
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

		return await unitOfWork.UserRepository.CreateUser(userDto);
	}

	public async Task<AuthenticationResponse> Login(LoginRequest request)
	{
		var response = new AuthenticationResponse();

		if (!(await unitOfWork.UserRepository.CheckIfUserExists(request.Username)))
			throw new BadRequestException("invalid username or password");

		var result = await unitOfWork.UserRepository.GetUserPasswordHashAndSalt(request.Username);

		var saltBytes = Convert.FromBase64String(result.Item2);

		var hashedPassword = ApplicationUtils.HashPassword(request.Password, saltBytes);
		if (string.CompareOrdinal(result.Item1, hashedPassword) != 0)
			throw new BadRequestException("invalid username or password");

		var user = await unitOfWork.UserRepository.GetUserByUsername(request.Username);
		response.AccessToken = ApplicationUtils.GenerateJwtToken(configuration, user);
		response.ExpiresAt = 7200000;
		return response;
	}

	public async Task<UserResponse> GetUser(int id)
	{
		var user = await unitOfWork.UserRepository.GetUserById(id);
		if (user == null)
			throw new RecordNotFoundException("user with this id does not exist");
		var userResponse = new UserResponse()
		{
			Id = user.Id,
			Username = user.Username,
			Firstname = user.Firstname,
			Lastname = user.Lastname,
			Email = user.Email,
			Phone = user.Phone,
			Grade = user.Grade,
			SchoolId = user.SchoolId,
			TotalScore = user.TotalScore,
			DateOfBirth = user.DateOfBirth,
			Gender = user.Gender,
			Role = user.Role,
			IsActive = user.IsActive,
		};

		return userResponse;
	}
}
