using System.Data.SqlClient;
using Dapper;
using UsersApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/api/Users/Detailed/{id}", async (int id) =>
{
    var connection = new SqlConnection("Server=mssqldb;Database=UsersDb;User Id=sa;Password=Ark_Hun1401!;TrustServerCertificate=true;");
    var httpClient = new HttpClient();

    var user = await connection.QueryFirstOrDefaultAsync<User>($"select * from Users where Id = {id}");

    var response = await httpClient.GetAsync($"http://booksapi/api/Books/{user.Id}");

    var books = await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();

    return new {
        username = user.Username,
        birthdate = user.Birthdate,
        books,
    };
})
.WithName("GetUsers")
.WithOpenApi();

app.Run();