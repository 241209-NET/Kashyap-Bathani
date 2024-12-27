using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models;

public enum UserRole{
  Admin,
  User
}

public class User{

  public int Id {get; set;}

  [Required]
  [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
  public string? Username {get; set;}

  // [Required]
  // [StringLength(50, MinimumLength = 3, ErrorMessage = "Password must be between 3 and 50 characters.")]
  public string? Password { get; set; }

  [Required]
  public UserRole Role { get; set; } = UserRole.User;// "Admin" or "User"

  [Required]
  [EmailAddress(ErrorMessage = "Invalid email address.")]
  public string? Email { get; set; }
  public DateTime CreatedAt { get; set; }

}