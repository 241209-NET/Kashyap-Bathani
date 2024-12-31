using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Models;
public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public List<CartItem> CartItems { get; set; } = new();
}
