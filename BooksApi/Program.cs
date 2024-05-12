using BooksApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/api/Books/{userId}", async (int userId) =>
{
    var client = new MongoClient("mongodb://mongodb:27017");

    // client.DropDatabase("BooksDb");

    var booksDb = client.GetDatabase("BooksDb");

    // booksDb.DropCollection("Books");

    // await booksDb.CreateCollectionAsync("Books");

    var collection = booksDb.GetCollection<Book>("Books");

    // await collection.InsertOneAsync(new Book {
    //     UserId = 1,
    //     Name = "Dune",
    //     Author = "Frank Herbert",
    //     Pages = 600,
    //     Tags = Enumerable.Empty<string>(),
    // });

    var books = collection.Find(Builders<Book>.Filter.Eq("UserId", userId)).ToEnumerable();

    return books;
})
.WithName("GetBooksByUserId")
.WithOpenApi();

app.Run();