using System.Security.Claims;
using GameStore.Api.Models;
using GameStore.Api.Models.DTOs;
using GameStore.Api.Repositories;
using GameStore.Api.Repositories.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly ICartsRepository _cartRepository;
    private readonly IUsersRepository _userRepository;
    private readonly IGamesRepository _gameRepository;

    public CartsController(
        ICartsRepository cartRepository,
        IUsersRepository userRepository,
        IGamesRepository gameRepository
    )
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _gameRepository = gameRepository;
    }

    [HttpPut("addtocart"), Authorize]
    public IActionResult AddItemToCart(AddToCartDto request)
    {
        var userIdentity = HttpContext.User;

        if (userIdentity.Identity?.IsAuthenticated == true)
        {
            var userEmail = userIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            User? user = _userRepository.UserExists(userEmail!);

            if (user is null)
                return NotFound("Sorry, but this user doesn't exist!");

            foreach (var item in request.Items)
                if (!_gameRepository.ExistGame(item.GameId))
                    return NotFound("Sorry, but game with id {" + item.GameId + "} doesn't exist");

            List<Item> items = request.Items.Select(i => new Item
            {
                Game = _gameRepository.GetGame(i.GameId)!,
                Quantity = i.Quantity
            }).ToList();

            _cartRepository.AddToCart(items, user.Cart);

            return Ok(user.Cart);
        }

        return Unauthorized("You are not authorized to access this user!");
    }

    [HttpPut("removefromcart/{id}"), Authorize]
    public IActionResult RemoveItemFromCart(int id)
    {
        var userIdentity = HttpContext.User;

        if (userIdentity.Identity?.IsAuthenticated == true)
        {
            var userEmail = userIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            User? user = _userRepository.UserExists(userEmail!);

            if (user is null)
                return NotFound("Sorry, but this user doesn't exist!");

            _cartRepository.RemoveFromCart(id, user.Cart);

            return Ok(user.Cart);
        }

        return Unauthorized("You are not authorized to access this user!");
    }
}