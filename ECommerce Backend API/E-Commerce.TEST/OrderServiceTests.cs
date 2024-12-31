using Xunit;
using Moq;
using ECommerce.API.Models;
using ECommerce.API.Repository;
using ECommerce.API.Services;
using System.Collections.Generic;
using System.Linq;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<ICartRepository> _cartRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _cartRepositoryMock = new Mock<ICartRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _orderService = new OrderService(_orderRepositoryMock.Object, _cartRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public void CreateOrderFromCart_SuccessfullyCreatesOrder()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser" };
        var cart = new Cart
        {
            Id = 1,
            UserId = 1,
            CartItems = new List<CartItem>
        {
            new CartItem
            {
                Id = 101,
                ProductId = 1,
                Product = new Product { Id = 1, Name = "Product1", Price = 20.0 }
            }
        }
        };

        var order = new Order
        {
            UserId = 1,
            OrderItems = new List<OrderItem>
        {
            new OrderItem { ProductId = 1, Price = 20.0 }
        }
        };

        _userRepositoryMock.Setup(repo => repo.GetByUsername("testuser")).Returns(user);
        _cartRepositoryMock.Setup(repo => repo.GetByUserId(user.Id)).Returns(cart);
        _orderRepositoryMock.Setup(repo => repo.Create(It.IsAny<Order>()))
            .Returns<Order>(o =>
            {
                o.Id = 1;
                foreach (var item in o.OrderItems)
                {
                    item.Product = new Product { Id = 1, Name = "Product1", Price = 20.0 };
                }
                return o;
            });

        // Act
        var result = _orderService.CreateOrderFromCart("testuser");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.OrderId);
        Assert.Equal("testuser", result.Username);
        Assert.Single(result.Items);
        Assert.Equal("Product1", result.Items.First().ProductName);
        _userRepositoryMock.Verify(repo => repo.GetByUsername("testuser"), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.GetByUserId(user.Id), Times.Once);
        _orderRepositoryMock.Verify(repo => repo.Create(It.IsAny<Order>()), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.Update(It.IsAny<Cart>()), Times.Once);
    }


    [Fact]
    public void CreateOrderFromCart_ThrowsException_WhenUserNotFound()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetByUsername("testuser")).Returns((User?)null);

        // Act & Assert
        Assert.Throws<Exception>(() => _orderService.CreateOrderFromCart("testuser"));
        _userRepositoryMock.Verify(repo => repo.GetByUsername("testuser"), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.GetByUserId(It.IsAny<int>()), Times.Never);
        _orderRepositoryMock.Verify(repo => repo.Create(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public void CreateOrderFromCart_ThrowsException_WhenCartIsEmpty()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser" };
        var emptyCart = new Cart { Id = 1, UserId = 1, CartItems = new List<CartItem>() };

        _userRepositoryMock.Setup(repo => repo.GetByUsername("testuser")).Returns(user);
        _cartRepositoryMock.Setup(repo => repo.GetByUserId(user.Id)).Returns(emptyCart);

        // Act & Assert
        Assert.Throws<Exception>(() => _orderService.CreateOrderFromCart("testuser"));
        _userRepositoryMock.Verify(repo => repo.GetByUsername("testuser"), Times.Once);
    }

    [Fact]
    public void GetOrder_ReturnsOrder_WhenOrderExists()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            UserId = 1,
            User = new User { Id = 1, Username = "testuser" },
            OrderItems = new List<OrderItem>
            {
                new OrderItem { Id = 201, ProductId = 1, Product = new Product { Name = "Product1" }, Price = 20.0 }
            }
        };

        _orderRepositoryMock.Setup(repo => repo.GetById(1)).Returns(order);

        // Act
        var result = _orderService.GetOrder(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.OrderId);
        Assert.Equal("testuser", result.Username);
        Assert.Single(result.Items);
        Assert.Equal("Product1", result.Items.First().ProductName);
        _orderRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public void GetOrder_ThrowsException_WhenOrderDoesNotExist()
    {
        // Arrange
        _orderRepositoryMock.Setup(repo => repo.GetById(1)).Returns((Order?)null);

        // Act & Assert
        Assert.Throws<Exception>(() => _orderService.GetOrder(1));
        _orderRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }
}