using GameStore.Api.Models;

namespace GameStore.Api.Repositories.InterFaces;

public interface ICartsRepository
{
    void AddToCart(List<Item> items, Cart cart);
    void RemoveFromCart(int itemId, Cart cart);
    // void CheckoutCart();
}