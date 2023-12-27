using GameStore.Api.Models;

namespace GameStore.Api.Repositories;

public interface IGamesRepository
{
    IEnumerable<Game> GetGames();
    Game? GetGame(int id);
    void Create(Game game);
    void Update(Game updatedGame);
    void Delete(int id);
}