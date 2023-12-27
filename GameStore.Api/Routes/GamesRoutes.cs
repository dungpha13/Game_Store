using GameStore.Api.Models;
using GameStore.Api.Repositories;

namespace GameStore.Api.Routes;

public static class GamesRoutes
{
    const string GetGameEndPoint = "GetGame";
    public static RouteGroupBuilder MapGamesRoutes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games").WithParameterValidation();

        group.MapGet("/", (IGamesRepository repository) => Results.Ok(repository.GetGames()));

        group.MapGet("/{id}", (IGamesRepository repository, int id) =>
        {
            Game? game = repository.GetGame(id);

            return game is not null ?
                Results.Ok(game) :
                Results.NotFound("Sorry, but this game doesn't exist!");

        }).WithName(GetGameEndPoint);

        group.MapPost("/", (IGamesRepository repository, Game game) =>
        {
            repository.Create(game);

            return Results.CreatedAtRoute(GetGameEndPoint, new { id = game.Id }, game);

        });

        group.MapPut("/{id}", (IGamesRepository repository, int id, Game game) =>
        {
            Game? exsitingGame = repository.GetGame(id);

            if (exsitingGame is null)
            {
                return Results.NotFound("Sorry, but this game doesn't exist!");
            }

            game.Id = id;

            repository.Update(game);

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