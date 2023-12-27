using GameStore.Api.Models;

List<Game> games = new()
{
    new Game()
    {
        Id = 1,
        Name = "Minecraft",
        Genre = "Survival",
        Price = 19.99M,
        ReleaseDate = new DateTime(2016, 10, 1),
        ImageUri = "https://placehold.co/100"
    },
    new Game()
    {
        Id = 2,
        Name = "Street Fighter II",
        Genre = "Survival",
        Price = 49.99M,
        ReleaseDate = new DateTime(2016, 10, 1),
        ImageUri = "https://placehold.co/100"
    },
    new Game()
    {
        Id = 3,
        Name = "Final Fantasy XIV",
        Genre = "Roleplaying",
        Price = 9.99M,
        ReleaseDate = new DateTime(2016, 10, 1),
        ImageUri = "https://placehold.co/100"
    }
};

const string GetGameEndPoint = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var group = app.MapGroup("/games");

group.MapGet("/", () => Results.Ok(games));
group.MapGet("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is null)
    {
        return Results.NotFound("Sorry, but this game doesn't exist!");
    }

    return Results.Ok(game);

}).WithName(GetGameEndPoint);
group.MapPost("/", (Game game) =>
{
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndPoint, new { id = game.Id }, game);

});
group.MapPut("/{id}", (int id, Game game) =>
{
    Game? exsitingGame = games.Find(game => game.Id == id);

    if (exsitingGame is null)
    {
        return Results.NotFound("Sorry, but this game doesn't exist!");
    }

    exsitingGame.Name = game.Name;
    exsitingGame.Genre = game.Genre;
    exsitingGame.Price = game.Price;
    exsitingGame.ReleaseDate = game.ReleaseDate;
    exsitingGame.ImageUri = game.ImageUri;

    return Results.NoContent();

});
group.MapDelete("/{id}", (int id) =>
{
    Game? exsitingGame = games.Find(game => game.Id == id);

    if (exsitingGame is not null)
    {
        games.Remove(exsitingGame);
    }

    return Results.NoContent();
});

app.Run();
