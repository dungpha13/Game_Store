using GameStore.Api.Models;
using GameStore.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGamesRepository _repository;

    public GamesController(IGamesRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetGames()
    {
        var games = _repository.GetGames().Select(game => game.AsGameDto());

        return Ok(games);
    }

    [HttpGet("{id}", Name = "GetGame")]
    public IActionResult GetGame(int id)
    {
        GameCard? game = _repository.GetGame(id);

        return game is not null ?
            Ok(game.AsGameDto()) :
            NotFound("Sorry, but this game doesn't exist!");
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public IActionResult AddGame(CreateGameDto request)
    {
        GameCard game = new()
        {
            Name = request.Name,
            Genre = request.Genre,
            Price = request.Price,
            ReleaseDate = request.ReleaseDate,
            ImageUri = request.ImageUri
        };

        _repository.Create(game);

        return CreatedAtRoute(nameof(GetGame), new { id = game.Id }, game);
    }

    [HttpPut("{id}"), Authorize(Roles = "Admin")]
    public IActionResult UpdateGame(int id, UpdateGameDto request)
    {
        GameCard? existingGame = _repository.GetGame(id);

        if (existingGame is null)
        {
            return NotFound("Sorry, but this game doesn't exist!");
        }

        if (!string.IsNullOrEmpty(request.Name))
            existingGame.Name = request.Name;

        if (request.Price.HasValue)
            existingGame.Price = request.Price.Value;

        if (!string.IsNullOrEmpty(request.Genre))
            existingGame.Genre = request.Genre;

        if (request.ReleaseDate.HasValue)
            existingGame.ReleaseDate = request.ReleaseDate.Value;

        if (!string.IsNullOrEmpty(request.ImageUri))
            existingGame.ImageUri = request.ImageUri;

        _repository.Update(existingGame);

        return NoContent();
    }

    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public IActionResult DeleteGame(int id)
    {
        GameCard? existingGame = _repository.GetGame(id);

        if (existingGame is not null)
            _repository.Delete(existingGame.Id);

        return NoContent();
    }
}