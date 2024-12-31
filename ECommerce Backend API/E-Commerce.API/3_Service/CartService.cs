using ECommerce.API.Repository;
using ECommerce.API.Models;
using System.Linq;

namespace ECommerce.API.Services;
public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public CartService(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public CartDto GetOrCreateCart(string username)
    {
        var user = _userRepository.GetByUsername(username);
        if (user == null)
            throw new Exception($"User '{username}' not found.");

        var cart = _cartRepository.GetByUserId(user.Id);
        if (cart == null)
        {
            cart = new Cart
            {
                Id = user.Id,
                UserId = user.Id
            };
            _cartRepository.Create(cart);
        }

        return MapCartToDto(cart, user.Username);
    }

    public CartDto AddItemToCart(string username, int productId)
    {
        var user = _userRepository.GetByUsername(username)
            ?? throw new Exception($"User '{username}' not found.");

        var cart = _cartRepository.GetByUserId(user.Id)
            ?? throw new Exception($"No cart found for user '{username}'.");

        var product = _productRepository.GetById(productId)
            ?? throw new Exception($"Product with ID {productId} not found.");


        cart.CartItems.Add(new CartItem
        {
            ProductId = productId,
            Product = product,
            Description = product.Description,
            Price = product.Price
        });

        _cartRepository.Update(cart);

        return MapCartToDto(cart, user.Username);
    }

    public CartDto RemoveItemFromCart(string username, int cartItemId)
    {
        var user = _userRepository.GetByUsername(username);
        if (user == null)
            throw new Exception($"User '{username}' not found.");

        var cart = _cartRepository.GetByUserId(user.Id)
            ?? throw new Exception($"No cart found for user '{username}'.");

        var item = cart.CartItems.FirstOrDefault(i => i.Id == cartItemId)
            ?? throw new Exception($"CartItem with ID {cartItemId} not found.");

        cart.CartItems.Remove(item);
        _cartRepository.Update(cart);

        return MapCartToDto(cart, user.Username);
    }

    public void ClearCart(string username)
    {
        var user = _userRepository.GetByUsername(username);
        if (user == null)
            throw new Exception($"User '{username}' not found.");

        var cart = _cartRepository.GetByUserId(user.Id)
            ?? throw new Exception($"No cart found for user '{username}'.");

        cart.CartItems.Clear();
        _cartRepository.Update(cart);
    }

    private CartDto MapCartToDto(Cart cart, string username)
    {
        return new CartDto
        {
            Username = username,
            CartId = cart.Id,
            CartItems = cart.CartItems.Select(ci => new CartItemDto
            {
                CartItemId = ci.Id,
                ProductName = ci.Product?.Name ?? "Unknown Product",
                Description = ci.Description ?? "No description available",
                Price = ci.Price ?? 0.0
            }).ToList()
        };
    }

}

