using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameStore.Api.Models;

public class Item
{
    public int Id { get; set; }

    [Range(1, 10)]
    public int Quantity { get; set; }
    public required GameCard Game { get; set; }

    [JsonIgnore]
    public List<CartItem> CartItems { get; set; }
}