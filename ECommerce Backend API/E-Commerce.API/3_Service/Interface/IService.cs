namespace ECommerce.API.Services;

using ECommerce.API.Models;

public interface IUserService
{
    User CreateUser(User user);
    User? GetUser(int id);
    IEnumerable<User> GetAllUsers();
    User UpdateUser(int id, User user);
    void DeleteUser(int id);
    User? GetUserByUsername(string username);

}

public interface IOrderService
{
    OrderDto CreateOrderFromCart(string username);
    OrderDto? GetOrder(int orderId);
}

public interface IProductService
{
    Product CreateProduct(Product product, int userId);
    Product? GetProduct(int id);
    IEnumerable<Product> GetAllProducts();
    Product UpdateProduct(int id, Product product, int userId);
    void DeleteProduct(int id, int userId);
}


public interface ICartService
{
    CartDto GetOrCreateCart(string username);
    CartDto AddItemToCart(string username, int productId);
    CartDto RemoveItemFromCart(string username, int cartItemId);
    void ClearCart(string username);
}
