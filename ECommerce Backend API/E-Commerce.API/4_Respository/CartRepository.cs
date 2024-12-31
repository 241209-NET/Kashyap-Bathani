using ECommerce.API.Data;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.API.Repository;
public class CartRepository : ICartRepository
{
    private readonly ECommerceContext _context;

    public CartRepository(ECommerceContext context)
    {
        _context = context;
    }

    public Cart? GetById(int cartId)
    {
        return _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefault(c => c.Id == cartId);
    }

    public Cart? GetByUserId(int userId)
    {
        return _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefault(c => c.UserId == userId);
    }


    public Cart Create(Cart cart)
    {
        _context.Carts.Add(cart);
        _context.SaveChanges();
        return cart;
    }

    public Cart Update(Cart cart)
    {
        _context.Entry(cart).State = EntityState.Modified;
        _context.SaveChanges();
        return cart;
    }

    public void Delete(Cart cart)
    {
        _context.Carts.Remove(cart);
        _context.SaveChanges();
    }
}
