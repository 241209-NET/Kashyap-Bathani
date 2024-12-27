namespace Ecommerce.API.Models;

public class Order{

  public int Id { get; set; }
  public int UserId { get; set; }
  public User? User { get; set; }
  public List<OrderItem> OrderItems { get; set; } = [];
  public double TotalAmount { get; set; }
  public DateTime PlacedAt { get; set; }

}