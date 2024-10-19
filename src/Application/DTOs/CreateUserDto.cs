using Domain.Enums;

namespace Application.DTOs;

public class CreateUserDto
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int Grade { get; set; } = 1;
    public int SchoolId { get; set; }
    public int TotalScore { get; set; } = 0;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
