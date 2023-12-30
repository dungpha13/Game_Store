using GameStore.Api.Models;

namespace GameStore.Api.Repositories;

public interface IGamesRepository
{
    IEnumerable<GameCard> GetGames();
    GameCard? GetGame(int id);
    void Create(GameCard game);
    void Update(GameCard updatedGame);
    void Delete(int id);
}