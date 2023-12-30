using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(30)]
    public required string LastName { get; set; }

    [EmailAddress]
    [StringLength(50)]
    public required string Email { get; set; }

    [Required]
    public required string PasswordHash { get; set; }

    [Url]
    [StringLength(100)]
    public required string ImageUri { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Role UserRole { get; set; } = Role.User;

    public Cart Cart { get; set; } = new();

}

public enum Role
{
    Admin,
    User
}