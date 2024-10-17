namespace Domain.Entities;
public class Question
{
	public int Id { get; set; }
	public string QuestionTj { get; set; } = string.Empty;
	public string QuestionRu { get; set; } = string.Empty;
	public string QuestionEn { get; set; } = string.Empty;
	public string[] OptionsTj { get; set; } = [];
	public string[] OptionsRu { get; set; } = [];
	public string[] OptionsEn { get; set; } = [];
	public string ImageId { get; set; } = string.Empty;
	public int AnswerId { get; set; }
	public int WeekId { get; set; }
}
