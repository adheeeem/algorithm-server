using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class UserUtils : IUserUtils
{
    public string HashPassword(string password, byte[] salt)
    {
        throw new NotImplementedException();
    }

    public string GenerateJwtToken(IConfiguration configuration, User user)
    {
        throw new NotImplementedException();
    }
}