using ECommerce.API.Models;
using ECommerce.API.Repository;

namespace ECommerce.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;

    public OrderService(
        IOrderRepository orderRepository,
        ICartRepository cartRepository,
        IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _userRepository = userRepository;
    }

    public OrderDto CreateOrderFromCart(string username)
    {
        var user = _userRepository.GetByUsername(username);
        if (user == null)
        {
            throw new Exception($"User '{username}' not found.");
        }

        var cart = _cartRepository.GetByUserId(user.Id);
        if (cart == null || cart.CartItems.Count == 0)
        {
            throw new Exception($"Cart is empty for user '{username}'.");
        }

        var order = new Order
        {
            UserId = user.Id
        };

        foreach (var cartItem in cart.CartItems)
        {
            if (cartItem.Product == null)
            {
                throw new Exception($"CartItem {cartItem.Id} has no associated product loaded.");
            }

            var orderItem = new OrderItem
            {
                ProductId = cartItem.ProductId,
                Product = cartItem.Product,
                Price = cartItem.Product.Price
            };
            order.OrderItems.Add(orderItem);
        }

        _orderRepository.Create(order);

        cart.CartItems.Clear();
        _cartRepository.Update(cart);

        return MapOrderToDto(order, user.Username);
    }

    public OrderDto GetOrder(int orderId)
    {
        var order = _orderRepository.GetById(orderId);
        if (order == null)
            throw new Exception($"Order {orderId} not found.");

        var username = order.User?.Username ?? "";

        return MapOrderToDto(order, username);
    }

    private OrderDto MapOrderToDto(Order order, string username)
    {

        return new OrderDto
        {
            OrderId = order.Id,
            Username = username,
            Items = order.OrderItems.Select(oi => new OrderItemDto
            {
                OrderItemId = oi.Id,
                ProductName = oi.Product?.Name ?? "",
                Description = oi.Product?.Description ?? "",
                Price = oi.Price
            }).ToList()
        };
    }
}
