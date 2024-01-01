using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameStore.Api.Models;

public class CartItem
{
    [JsonIgnore]
    public int CartId { get; set; }

    [JsonIgnore]
    public int ItemId { get; set; }

    [JsonIgnore]
    public Cart Cart { get; set; }
    public Item Item { get; set; }
}