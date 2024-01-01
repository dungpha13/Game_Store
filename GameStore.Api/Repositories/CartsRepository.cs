using GameStore.Api.Data;
using GameStore.Api.Models;
using GameStore.Api.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class CartsRepository : ICartsRepository
{
    private readonly DataBaseContext _context;

    public CartsRepository(DataBaseContext context)
    {
        _context = context;
    }

    public void AddToCart(List<Item> items, Cart cart)
    {
        if (cart != null)
        {
            foreach (var item in items)
            {
                var existingCartItem = _context.CartItems
                    .Include(ci => ci.Item)
                        .ThenInclude(i => i.Game)
                    .FirstOrDefault(ci => (ci.Item.Game.Id == item.Game.Id) && (ci.CartId == cart.Id));

                if (existingCartItem is null)
                {
                    var cartItem = new CartItem
                    {
                        Item = item,
                        Cart = cart
                    };

                    _context.CartItems.Add(cartItem);
                }
                else
                {
                    existingCartItem.Item.Quantity += item.Quantity;
                    _context.Items.Update(existingCartItem.Item);
                }
            }
            _context.SaveChanges();
        }
    }

    public void RemoveFromCart(int itemId, Cart cart)
    {
        if (cart != null)
        {
            var existingCartItem = _context.CartItems
                .Include(ci => ci.Item)
                .FirstOrDefault(ci => (ci.Item.Id == itemId) && (ci.CartId == cart.Id));

            if (existingCartItem is not null)
            {
                _context.CartItems.Remove(existingCartItem);
                _context.SaveChanges();
            }
        }
    }
}

