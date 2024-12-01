namespace Application.Responses;

public class MinimalQuestionAttemptResultByWeekAndUnitNumberResponse
{
    public int CorrectAnswers { get; set; }
    public DateTime Date { get; set; }
    public int NumberOfQuestions { get; set; }
}