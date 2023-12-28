using GameStore.Api.Models;
using GameStore.Api.Repositories;

namespace GameStore.Api.Routes;

public static class GamesRoutes
{
    const string GetGameEndPoint = "GetGame";
    public static RouteGroupBuilder MapGamesRoutes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games").WithParameterValidation();

        group.MapGet("/", (IGamesRepository repository) =>
            Results.Ok(repository.GetGames().Select(game => game.AsDto())));

        group.MapGet("/{id}", (IGamesRepository repository, int id) =>
        {
            Game? game = repository.GetGame(id);

            return game is not null ?
                Results.Ok(game.AsDto()) :
                Results.NotFound("Sorry, but this game doesn't exist!");

        }).WithName(GetGameEndPoint);

        group.MapPost("/", (IGamesRepository repository, CreateGameDto gameDto) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            repository.Create(game);

            return Results.CreatedAtRoute(GetGameEndPoint, new { id = game.Id }, game);

        });

        group.MapPut("/{id}", (IGamesRepository repository, int id, UpdateGameDto updateGameDto) =>
        {
            Game? exsitingGame = repository.GetGame(id);

            if (exsitingGame is null)
            {
                return Results.NotFound("Sorry, but this game doesn't exist!");
            }

            Game updateGame = new()
            {
                Id = id,
                Name = updateGameDto.Name,
                Genre = updateGameDto.Genre,
                Price = updateGameDto.Price,
                ReleaseDate = updateGameDto.ReleaseDate,
                ImageUri = updateGameDto.ImageUri
            };

            repository.Update(updateGame);

            return Results.NoContent();

        });

        group.MapDelete("/{id}", (IGamesRepository repository, int id) =>
        {
            Game? exsitingGame = repository.GetGame(id);

            if (exsitingGame is not null)
            {
                repository.Delete(exsitingGame.Id);
            }

            return Results.NoContent();
        });

        return group;
    }
}