using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models.DTOs;

public record UserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string ImageUri
);

public record LoginUserDto(
        [EmailAddress][StringLength(50)] string Email,
        [Required] string Password
);

public record CreateUserDto(
        [Required][StringLength(30)] string FirstName,
        [Required][StringLength(30)] string LastName,
        [EmailAddress][StringLength(50)] string Email,
        [Required] string Password,
        [Url][StringLength(100)] string ImageUri,
        [EnumDataType(typeof(Role))] string UserRole
);

public record UpdateUserDto(
        [StringLength(30)] string? FirstName,
        [StringLength(30)] string? LastName,
        // [EmailAddress][StringLength(50)] string? Email,
        string? Password,
        [Url][StringLength(100)] string? ImageUri,
        [EnumDataType(typeof(Role))] string? UserRole
);
