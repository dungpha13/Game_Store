using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameStore.Api.Helpers;
using GameStore.Api.Models;
using GameStore.Api.Models.DTOs;
using GameStore.Api.Repositories.InterFaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _repository;
    public AuthController(IConfiguration configuration, IUsersRepository repository)
    {
        _repository = repository;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public IActionResult Register(CreateUserDto request)
    {

        if (_repository.UserExists(request.Email) is not null)
            return BadRequest("Email has been used by another accout!");

        string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            ImageUri = request.ImageUri,
            PasswordHash = passwordHash,
            Cart = new Cart()
        };

        if (!Enum.TryParse(request.UserRole, out Role role))
            return BadRequest("Role is invalid!");
        // else
        user.UserRole = role;

        _repository.Create(user);

        return Ok(user.AsUserDto());
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUserDto request)
    {
        User? existingUser = _repository.UserExists(request.Email);

        if (existingUser is null)
            return BadRequest("Email or Password is incorrect!");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash))
            return BadRequest("Email or Password is incorrect!");

        string token = JwtHelper.CreateToken(existingUser, _configuration.GetSection("AppSettings:Token").Value!);

        return Ok(token);
    }
}