namespace Application.Requests;

public class CreateQuestionRequest
{
	public string QuestionTj { get; set; } = string.Empty;
	public string QuestionRu { get; set; } = string.Empty;
	public string QuestionEn { get; set; } = string.Empty;
	public string[] OptionsTj { get; set; } = [];
	public string[] OptionsRu { get; set; } = [];
	public string[] OptionsEn { get; set; } = [];
    public int UnitNumber { get; set; }
    public int WeekNumber { get; set; }
    public int Grade { get; set; }
    public int AnswerId { get; set; }
}
