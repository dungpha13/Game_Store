using GameStore.Api.Models;

namespace GameStore.Api.Repositories.InterFaces;

public interface IUsersRepository
{
    User? GetUser(int id);
    void Create(User user);
    void Update(User updatedUser);
    void Delete(int id);
    User? UserExists(string email);
    Cart? GetCartByUser(User user);
}