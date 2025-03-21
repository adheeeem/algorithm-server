﻿namespace Application.DTOs;

public class QuestionFullDto
{
    public int Id { get; set; }
    public string QuestionTj { get; set; } = string.Empty;
	public string QuestionRu { get; set; } = string.Empty;
	public string QuestionEn { get; set; } = string.Empty;
	public string[] OptionsTj { get; set; } = [];
	public string[] OptionsRu { get; set; } = [];
	public string[] OptionsEn { get; set; } = [];
    public int Grade { get; set; }
    public int WeekNumber { get; set; }
    public int UnitNumber { get; set; }
    public string ImageId { get; set; } = string.Empty;
    public int AnswerId { get; set; }
}
