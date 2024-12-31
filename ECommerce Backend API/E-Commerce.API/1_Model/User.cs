using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool IsAdmin { get; set; } = false;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

