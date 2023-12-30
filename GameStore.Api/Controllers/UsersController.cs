using System.Security.Claims;
using GameStore.Api.Models;
using GameStore.Api.Models.DTOs;
using GameStore.Api.Repositories.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _repository;

    public UsersController(IUsersRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}"), Authorize]
    public IActionResult GetUser(int id)
    {
        var userIdentity = HttpContext.User;

        if (userIdentity.Identity?.IsAuthenticated == true)
        {
            var userEmail = userIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            User? user = _repository.GetUser(id);

            if (user is null)
                return NotFound("Sorry, but this user doesn't exist!");

            return user.Email == userEmail
                ? Ok(user.AsUserDto())
                : Unauthorized("You are not authorized to access this user!");
        }

        return Unauthorized("You are not authorized to access this user!");
    }

    [HttpPut("{id}"), Authorize]
    public IActionResult UpdateUser(int id, UpdateUserDto request)
    {
        var userIdentity = HttpContext.User;

        if (userIdentity.Identity?.IsAuthenticated == true)
        {
            var userEmail = userIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            User? user = _repository.GetUser(id);

            if (user is null)
                return NotFound("Sorry, but this user doesn't exist!");

            if (user.Email != userEmail)
                return Unauthorized("You are not authorized to access this user!");

            if (!string.IsNullOrEmpty(request.FirstName))
                user.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;

            // if (!string.IsNullOrEmpty(request.Email))
            //     user.Email = request.Email;

            if (!string.IsNullOrEmpty(request.ImageUri))
                user.ImageUri = request.ImageUri;

            if (!string.IsNullOrEmpty(request.UserRole) && Enum.TryParse(request.UserRole, out Role role))
                user.UserRole = role;

            if (!string.IsNullOrEmpty(request.Password))
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.PasswordHash = passwordHash;
            }

            _repository.Update(user);

            return NoContent();
        }

        return Unauthorized("You are not authorized to access this user!");
    }

    [HttpDelete("{id}"), Authorize]
    public IActionResult DeleteUser(int id)
    {
        var userIdentity = HttpContext.User;

        if (userIdentity.Identity?.IsAuthenticated == true)
        {
            var userEmail = userIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            User? existingUser = _repository.GetUser(id);

            if (existingUser is null)
                return NotFound("Sorry, but this user doesn't exist!");

            if (existingUser.Email != userEmail)
                return NotFound("You are not authorized to access this user!");

            if (existingUser is not null)
                _repository.Delete(existingUser.Id);

            return NoContent();

        }

        return Unauthorized("You are not authorized to access this user!");

    }

}