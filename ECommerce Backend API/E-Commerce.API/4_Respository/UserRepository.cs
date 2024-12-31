using ECommerce.API.Data;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.API.Repository;
public class UserRepository : IUserRepository
{
    private readonly ECommerceContext _context;

    public UserRepository(ECommerceContext context)
    {
        _context = context;
    }

    public User Create(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User? GetById(int id)
    {
        return _context.Users.Find(id);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();
        return user;
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public User? GetByUsername(string username)
    {
        return _context.Users
            .FirstOrDefault(u => u.Username == username);
    }
}

