namespace Application.Responses;

public class UserEnrollmentResponse
{
	public int UnitNumber { get; set; }
	public bool IsCompleted { get; set; }
	public bool Paid { get; set; }
	public bool Enrolled { get; set; }
	public DateTimeOffset Date { get; set; }
}
