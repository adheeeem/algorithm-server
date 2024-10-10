namespace Domain.Entities;

public class UserWeeklyActivity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset StartDate { get; set; } = DateTimeOffset.Now;
    public bool IsCompleted { get; set; }
    public int WeekId { get; set; }
}
