using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}