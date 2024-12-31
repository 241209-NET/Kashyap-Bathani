namespace ECommerce.API.Models;

public class CartDto
{
    public string Username { get; set; } = string.Empty;
    public int CartId { get; set; }
    public List<CartItemDto> CartItems { get; set; } = new();
}

public class CartItemDto
{
    public int CartItemId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; } = 0.0;
}
