namespace ECommerce.API.Models;
public class OrderDto
{
    public int OrderId { get; set; }
    public string Username { get; set; } = string.Empty;
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public int OrderItemId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
}
