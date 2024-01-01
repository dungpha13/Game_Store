using GameStore.Api.Models.DTOs;

namespace GameStore.Api.Models;

public static class ModelExtensions
{
    public static UserDto AsUserDto(this User user)
    {
        return new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.ImageUri
        );
    }
    public static GameDto AsGameDto(this GameCard game)
    {
        return new GameDto(
            game.Id,
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate,
            game.ImageUri
        );
    }
    // public static CartDto AsCartDto(this Cart cart)
    // {
    //     return new CartDto(
    //         cart.Id,
    //         cart.TotalCheckout,
    //         cart.Items.Select(i => i.AsCartItemDto()).ToList()
    //     );
    // }

    // public static CartItemDto AsCartItemDto(this CartItem cartItem)
    // {
    //     return new CartItemDto(
    //         cartItem?.Quantity ?? 0,
    //         cartItem?.Game.Id ?? -1
    //     );
    // }
}