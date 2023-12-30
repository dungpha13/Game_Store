using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models;

public class CartItem
{
    public int Id { get; set; }

    [Range(1, 10)]
    public int Quantity { get; set; }

    [Range(1, 100)]
    public double Price { get; set; }
    public required Cart Cart { get; set; }
    public required GameCard Game { get; set; }
}