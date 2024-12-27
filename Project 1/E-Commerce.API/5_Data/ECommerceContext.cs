using Microsoft.EntityFrameworkCore;
using Ecommerce.API.Models;

namespace Ecommerce.API.Data;

public partial class EcommerceContext: DbContext{
  
  public EcommerceContext(){}
  public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options){}

  public DbSet<User> Users { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder){

    // User
    modelBuilder.Entity<User>()
      .Property(u => u.Id)
      .ValueGeneratedOnAdd();

    modelBuilder.Entity<User>()
      .Property(u => u.Username)
      .IsRequired()
      .HasMaxLength(50);
    
    modelBuilder.Entity<User>()
      .Property(e => e.Email)
      .IsRequired();

    modelBuilder.Entity<User>()
      .HasIndex(e => e.Email)
      .IsUnique();


    // Product
    modelBuilder.Entity<Product>()
      .Property(p => p.Name)
      .IsRequired()
      .HasMaxLength(100);

    modelBuilder.Entity<Product>()
      .Property(p => p.Price)
      .HasColumnType("decimal(5,2)");

    modelBuilder.Entity<Product>()
      .Property(u => u.Id)
      .IsRequired()
      .ValueGeneratedOnAdd();

    modelBuilder.Entity<Product>()
      .Property(p => p.Stock)
      .IsRequired()
      .HasDefaultValue(0);

    //Order
    modelBuilder.Entity<Order>()
      .Property(o => o.TotalAmount)
      .HasColumnType("decimal(5,2)");

    modelBuilder.Entity<Order>()
      .HasOne(o => o.User)
      .WithMany()
      .HasForeignKey(o => o.UserId);

    modelBuilder.Entity<Order>()
      .HasMany(o => o.OrderItems)
      .WithOne(oi => oi.Order)
      .HasForeignKey(oi => oi.OrderId);


    //OrderItem
    modelBuilder.Entity<OrderItem>()
      .Property(oi => oi.Price)
      .HasColumnType("decimal(18,2)");

    modelBuilder.Entity<OrderItem>()
      .HasOne(oi => oi.Product)
      .WithMany()
      .HasForeignKey(oi => oi.ProductId);

      // Seed Data
      modelBuilder.Entity<User>().HasData(
        new User { Id = 1, Username = "admin", Email = "admin@example.com", Role = UserRole.Admin },
        new User { Id = 2, Username = "sam", Email = "sam@example.com", Role = UserRole.User },
        new User { Id = 3, Username = "yuki", Email = "yuki@example.com", Role = UserRole.User }
      );

      modelBuilder.Entity<Product>().HasData(
        new Product { Id = 1, Name = "Laptop", Price = 999.99, Stock = 10, Category = "Electronics", CreatedAt = DateTime.UtcNow },
        new Product { Id = 2, Name = "Mouse", Price = 19.99, Stock = 10, Category = "Electronics", CreatedAt = DateTime.UtcNow },
        new Product { Id = 3, Name = "Keyboard", Price = 29.99, Stock = 10, Category = "Electronics", CreatedAt = DateTime.UtcNow },
        new Product { Id = 4, Name = "Shirt", Price = 10.99, Stock = 10, Category = "Clothing", CreatedAt = DateTime.UtcNow }
      );
  }

}