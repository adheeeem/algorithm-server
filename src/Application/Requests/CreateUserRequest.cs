using Domain.Enums;

namespace Application.Requests;

public class CreateUserRequest
{
	public string Firstname { get; set; } = string.Empty;
	public string Lastname { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
	public int Grade { get; set; } = 1;
	public int SchoolId { get; set; }
	public string Email { get; set; } = string.Empty;
	public DateTime DateOfBirth { get; set; }
	public Gender Gender { get; set; }
}
