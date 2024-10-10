namespace Domain.Entities;
public class Week
{
    public int Id { get; set; }
    public int Number { get; set; }
    public int Grade { get; set; }
    public string QuestionsDownloadUrl { get; set; } = string.Empty;
    public int UnitNumber { get; set; }
}
