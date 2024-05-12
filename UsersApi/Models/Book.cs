namespace UsersApi.Models;
public class Book
{
    public string? name { get; set; }
    public string? author { get; set; }
    public int pages { get; set; }
    public IEnumerable<string>? tags { get; set; }
}