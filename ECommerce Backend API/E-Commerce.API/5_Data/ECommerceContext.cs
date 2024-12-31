using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Data;

public class ECommerceContext : DbContext
{

  public ECommerceContext() { }
  public ECommerceContext(DbContextOptions<ECommerceContext> options)
      : base(options)
  { }

  public DbSet<Product> Products { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Cart> Carts { get; set; }
  public DbSet<CartItem> CartItems { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }


}

