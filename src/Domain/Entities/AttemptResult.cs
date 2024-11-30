namespace Domain.Entities;

public class AttemptResult
{
    public int Id { get; set; }
    public Guid AttemptGroupId { get; set; }
    public int CorrectAnswers { get; set; }
}