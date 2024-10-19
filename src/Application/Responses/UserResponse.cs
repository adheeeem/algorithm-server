using Domain.Enums;

namespace Application.Responses;

public class UserResponse
{
	public int Id { get; set; }
	public string Firstname { get; set; } = string.Empty;
	public string Lastname { get; set; } = string.Empty;
	public string Username { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public int Grade { get; set; }
	public int SchoolId { get; set; }
	public int TotalScore { get; set; }
	public string Email { get; set; } = string.Empty;
	public DateTime DateOfBirth { get; set; }
	public Gender Gender { get; set; }
	public Role Role { get; set; }
	public bool IsActive { get; set; }
}
