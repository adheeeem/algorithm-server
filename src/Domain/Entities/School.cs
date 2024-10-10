namespace Domain.Entities;

public class School
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Region { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
	public string Country { get; set; } = string.Empty;
}
