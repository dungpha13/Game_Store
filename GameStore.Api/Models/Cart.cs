namespace GameStore.Api.Models;

public class Cart
{
    public int Id { get; set; }
    public double TotalCheckout { get; set; } = 0.0;
    public List<CartItem> Items { get; set; } = [];
}