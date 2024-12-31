namespace ECommerce.API.Models;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public Cart? Cart { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
}