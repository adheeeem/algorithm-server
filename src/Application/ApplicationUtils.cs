using Domain.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
}
