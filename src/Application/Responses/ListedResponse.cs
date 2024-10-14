namespace Application.Responses;

public class ListedResponse<T>
{
    public int Total { get; set; }
    public List<T> Items { get; set; } = [];
}
