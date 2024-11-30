using Domain.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions;
using Application.Responses;

namespace Application;

public static class ApplicationUtils
{
	public static string HashPassword(string password, byte[] salt) => Convert.ToBase64String(
			KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 10000,
				numBytesRequested: 256 / 8
			)
	);
	public static string GenerateJwtToken(IConfiguration configuration, User user)
	{
		SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["JWTSecret"]!));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		List<Claim> claims = [
			new Claim("id", user.Id.ToString()),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		];


		var token = new JwtSecurityToken(
			configuration["JWT-Issuer"],
			configuration["JWT-Audience"],
			claims,
			expires: DateTime.Now.AddMinutes(120),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public static void ThrowExceptionIfCannotAccessToWeek(UnitWeeksAccess weekAccess, int weekNumber)
	{
		if ((weekNumber == 1 && !weekAccess.Week1) || (weekNumber == 2 && !weekAccess.Week2) ||
		    (weekNumber == 3 && !weekAccess.Week3) || (weekNumber == 4 && !weekAccess.Week4))
			throw new BadRequestException("You do not have an access to this week.");
	}

	public static UnitWeeksAccess CalculateWeeksAccess(DateTime unitStartDate)
	{
		var unitWeeksAccess = new UnitWeeksAccess
		{
			Week1 = true
		};
		
		var today = DateTime.Today;
		var daysDifference = today.Subtract(unitStartDate.Date).Days;

		switch (daysDifference)
		{
			case <= 14 and > 6:
				unitWeeksAccess.Week2 = true;
				unitWeeksAccess.Week3 = false;
				unitWeeksAccess.Week4 = false;
				return unitWeeksAccess;
			case <= 21 and > 14:
				unitWeeksAccess.Week2 = true;
				unitWeeksAccess.Week3 = true;
				unitWeeksAccess.Week4 = false;
				return unitWeeksAccess;
			case > 21:
				unitWeeksAccess.Week2 = true;
				unitWeeksAccess.Week3 = true;
				unitWeeksAccess.Week4 = true;
				return unitWeeksAccess;
			default:
				return unitWeeksAccess;
		}
	}
}
