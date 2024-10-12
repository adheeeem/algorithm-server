namespace Application.DTOs;

public class CreateWeekDto
{
    public int Number { get; set; }
    public int Grade { get; set; }
    public int UnitNumber { get; set; }
    public string QuestionsDownloadUrl { get; set; } = string.Empty;
}
