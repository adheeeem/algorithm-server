namespace Application.DTOs;

public class MinimalQuestionAttemptResultByWeekAndUnitNumberDto
{
    public Guid GroupId { get; set; }
    public int UserId { get; set; }
    public int CorrectAnswers { get; set; }
    public DateTime Date { get; set; }
    public int WeekNumber { get; set; }
    public int UnitNumber { get; set; }
}