namespace Application.Requests;

public class AddNewQuestionRequest
{
	public string QuestionTj { get; set; } = string.Empty;
	public string QuestionRu { get; set; } = string.Empty;
	public string QuestionEn { get; set; } = string.Empty;
	public string[] OptionsTj { get; set; } = [];
	public string[] OptionsRu { get; set; } = [];
	public string[] OptionsEn { get; set; } = [];
	public int AnswerId { get; set; }
}
