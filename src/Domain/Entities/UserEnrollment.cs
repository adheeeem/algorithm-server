namespace Domain.Entities;

public class UserEnrollment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int UnitNumber { get; set; }
    public bool IsCompleted { get; set; }
    public bool Paid { get; set; }
    public bool Enrolled { get; set; }
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
}
