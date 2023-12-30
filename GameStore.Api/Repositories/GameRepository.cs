using GameStore.Api.Data;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class GameRepository : IGamesRepository
{
    private readonly DataBaseContext _context;

    public GameRepository(DataBaseContext dbContext)
    {
        _context = dbContext;
    }

    public IEnumerable<GameCard> GetGames()
    {
        return _context.Games.AsNoTracking().ToList();
    }

    public GameCard? GetGame(int id)
    {
        return _context.Games.Find(id);
    }

    public void Create(GameCard game)
    {
        _context.Games.Add(game);
        _context.SaveChanges();
    }

    public void Update(GameCard updatedGame)
    {
        _context.Update(updatedGame);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.Games.Where(game => game.Id == id)
                        .ExecuteDelete();
    }
}