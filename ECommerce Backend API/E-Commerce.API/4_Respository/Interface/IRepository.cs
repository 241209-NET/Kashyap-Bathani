using ECommerce.API.Models;

namespace ECommerce.API.Repository;

public interface IUserRepository
{
    User Create(User user);
    User? GetById(int id);
    IEnumerable<User> GetAll();
    User Update(User user);
    void Delete(User user);
    User? GetByUsername(string username);
}

public interface IProductRepository
{
    Product Create(Product product);
    Product? GetById(int id);
    IEnumerable<Product> GetAll();
    Product Update(Product product);
    void Delete(Product product);
}

public interface ICartRepository
{
    Cart? GetById(int cartId);
    Cart? GetByUserId(int userId);
    Cart Create(Cart cart);
    Cart Update(Cart cart);
    void Delete(Cart cart);
}

public interface IOrderRepository
{
    Order Create(Order order);
    Order? GetById(int orderId);
}


