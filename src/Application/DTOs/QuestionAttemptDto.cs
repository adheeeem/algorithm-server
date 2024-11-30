namespace Application.DTOs;

public class QuestionAttemptDto
{
    public int UserId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int QuestionId { get; set; }
    public int SelectedOptionIndex { get; set; }
    public Guid GroupId { get; set; }
}