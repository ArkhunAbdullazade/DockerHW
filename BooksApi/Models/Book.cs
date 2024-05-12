namespace BooksApi.Models;
public class Book
{
    public object _id { get; set; }
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Author { get; set; }
    public int Pages { get; set; }
    public IEnumerable<string>? Tags { get; set; }
}