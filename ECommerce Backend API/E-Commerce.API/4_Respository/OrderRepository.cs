using ECommerce.API.Data;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.API.Repository;
public class OrderRepository : IOrderRepository
{
    private readonly ECommerceContext _context;

    public OrderRepository(ECommerceContext context)
    {
        _context = context;
    }

    public Order Create(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
    }

    public Order? GetById(int orderId)
    {
        return _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Include(o => o.User)
            .FirstOrDefault(o => o.Id == orderId);
    }

}
