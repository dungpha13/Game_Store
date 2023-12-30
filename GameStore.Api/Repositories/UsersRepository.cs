using GameStore.Api.Data;
using GameStore.Api.Models;
using GameStore.Api.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DataBaseContext _context;

    public UsersRepository(DataBaseContext dbContext)
    {
        _context = dbContext;
    }

    public void Create(User user)
    {
        _context.Add(user);
        _context.SaveChanges();
    }

    public User? GetUser(int id)
    {
        return _context.Users.Find(id);
    }

    public void Delete(int id)
    {
        _context.Users.Where(user => user.Id == id)
                        .ExecuteDelete();
    }

    public void Update(User updatedUser)
    {
        _context.Update(updatedUser);
        _context.SaveChanges();
    }

    public User? UserExists(string email)
    {
        return _context.Users.FirstOrDefault(user => user.Email == email);
    }
}