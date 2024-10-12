namespace Domain.Entities;

public class UserQuestionAttempt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
}
