using ECommerce.API.Repository;
using ECommerce.API.Models;

namespace ECommerce.API.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User CreateUser(User user)
    {
        if (user.Username != null && user.Username.EndsWith(".admin", StringComparison.OrdinalIgnoreCase))
        {
            user.IsAdmin = true;
        }
        else
        {
            user.IsAdmin = false;
        }

        return _userRepository.Create(user);
    }

    public User? GetUser(int id)
    {
        return _userRepository.GetById(id);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    public User UpdateUser(int id, User updated)
    {
        var existing = _userRepository.GetById(id)
            ?? throw new Exception("User not found");

        if (updated.Username != null && updated.Username.EndsWith(".admin", StringComparison.OrdinalIgnoreCase))
            updated.IsAdmin = true;
        else
            updated.IsAdmin = false;

        existing.Username = updated.Username;
        existing.Password = updated.Password;
        existing.FirstName = updated.FirstName;
        existing.LastName = updated.LastName;
        existing.IsAdmin = updated.IsAdmin;

        return _userRepository.Update(existing);
    }

    public void DeleteUser(int id)
    {
        var existing = _userRepository.GetById(id);
        if (existing == null) return;
        _userRepository.Delete(existing);
    }

    public User? GetUserByUsername(string username)
    {
        return _userRepository.GetByUsername(username);
    }
}