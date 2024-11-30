namespace Application.Requests;

public class QuestionAttemptRequest
{
    public int QuestionId { get; set; }
    public int SelectedOptionIndex { get; set; }
}