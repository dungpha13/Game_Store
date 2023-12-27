using GameStore.Api.Models;

namespace GameStore.Api.Repositories;

public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games = new()
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

    public IEnumerable<Game> GetGames()
    {
        return games;
    }

    public Game? GetGame(int id)
    {
        return games.Find(game => game.Id == id);
    }

    public void Create(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
    }

    public void Update(Game updatedGame)
    {
        var index = games.FindIndex(game => game.Id == updatedGame.Id);
        games[index] = updatedGame;
    }

    public void Delete(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);
    }
}