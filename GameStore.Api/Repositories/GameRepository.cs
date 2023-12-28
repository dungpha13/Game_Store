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

    public IEnumerable<Game> GetGames()
    {
        return _context.Games.AsNoTracking().ToList();
    }

    public Game? GetGame(int id)
    {
        return _context.Games.Find(id);
    }

    public void Create(Game game)
    {
        _context.Games.Add(game);
        _context.SaveChanges();
    }

    public void Update(Game updatedGame)
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