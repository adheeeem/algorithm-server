using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Interfaces;

public interface IUserUtils
{
    string HashPassword(string password, byte[] salt);
    string GenerateJwtToken(IConfiguration configuration, User user);
}