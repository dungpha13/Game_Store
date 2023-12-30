// using GameStore.Api.Models;
// using GameStore.Api.Repositories;

// namespace GameStore.Api.Routes;

// public static class GamesRoute
// {
//     const string GetGameEndPoint = "GetGame";
//     public static RouteGroupBuilder MapGamesRoute(this IEndpointRouteBuilder routes)
//     {
//         var group = routes.MapGroup("/api/games").WithParameterValidation();

//         group.MapGet("/", (IGamesRepository repository) =>
//             Results.Ok(repository.GetGames().Select(game => game.AsDto())));

//         group.MapGet("/{id}", (IGamesRepository repository, int id) =>
//         {
//             GameCard? game = repository.GetGame(id);

//             return game is not null ?
//                 Results.Ok(game.AsDto()) :
//                 Results.NotFound("Sorry, but this game doesn't exist!");

//         }).WithName(GetGameEndPoint);

//         group.MapPost("/", (IGamesRepository repository, CreateGameDto gameDto) =>
//         {
//             GameCard game = new()
//             {
//                 Name = gameDto.Name,
//                 Genre = gameDto.Genre,
//                 Price = gameDto.Price,
//                 ReleaseDate = gameDto.ReleaseDate,
//                 ImageUri = gameDto.ImageUri
//             };

//             repository.Create(game);

//             return Results.CreatedAtRoute(GetGameEndPoint, new { id = game.Id }, game);

//         });

//         group.MapPut("/{id}", (IGamesRepository repository, int id, UpdateGameDto updateGameDto) =>
//         {
//             GameCard? exsitingGame = repository.GetGame(id);

//             if (exsitingGame is null)
//             {
//                 return Results.NotFound("Sorry, but this game doesn't exist!");
//             }

//             exsitingGame.Name = updateGameDto.Name;
//             exsitingGame.Price = updateGameDto.Price;
//             exsitingGame.Genre = updateGameDto.Genre;
//             exsitingGame.ReleaseDate = updateGameDto.ReleaseDate;
//             exsitingGame.ImageUri = updateGameDto.ImageUri;

//             repository.Update(exsitingGame);

//             return Results.NoContent();

//         });

//         group.MapDelete("/{id}", (IGamesRepository repository, int id) =>
//         {
//             GameCard? exsitingGame = repository.GetGame(id);

//             if (exsitingGame is not null)
//             {
//                 repository.Delete(exsitingGame.Id);
//             }

//             return Results.NoContent();
//         });

//         return group;
//     }
// }