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

    [HttpGet, Authorize]
    public IActionResult GetGames()
    {
        var games = _repository.GetGames().Select(game => game.AsGameDto());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(games);
    }

    [HttpGet("{id}", Name = "GetGame")]
    public IActionResult GetGame(int id)
    {
        GameCard? game = _repository.GetGame(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return game is not null ?
            Ok(game.AsGameDto()) :
            NotFound("Sorry, but this game doesn't exist!");
    }

    [HttpPost]
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

    [HttpPut("{id}")]
    public IActionResult UpdateGame(int id, UpdateGameDto request)
    {
        GameCard? exsitingGame = _repository.GetGame(id);

        if (exsitingGame is null)
        {
            return NotFound("Sorry, but this game doesn't exist!");
        }

        exsitingGame.Name = request.Name;
        exsitingGame.Price = request.Price;
        exsitingGame.Genre = request.Genre;
        exsitingGame.ReleaseDate = request.ReleaseDate;
        exsitingGame.ImageUri = request.ImageUri;

        _repository.Update(exsitingGame);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGame(int id)
    {
        GameCard? exsitingGame = _repository.GetGame(id);

        if (exsitingGame is not null)
        {
            _repository.Delete(exsitingGame.Id);
        }

        return NoContent();
    }
}