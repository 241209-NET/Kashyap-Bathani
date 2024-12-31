using Xunit;
using Moq;
using ECommerce.API.Models;
using ECommerce.API.Repository;
using ECommerce.API.Services;
using System.Collections.Generic;
using System.Linq;

public class CartServiceTests
{
    private readonly Mock<ICartRepository> _cartRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _cartRepositoryMock = new Mock<ICartRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _cartService = new CartService(_cartRepositoryMock.Object, _productRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public void GetOrCreateCart_ReturnsCart_WhenCartExists()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser" };
        var cart = new Cart { Id = 1, UserId = 1, CartItems = new List<CartItem>() };

        _userRepositoryMock.Setup(repo => repo.GetByUsername("testuser")).Returns(user);
        _cartRepositoryMock.Setup(repo => repo.GetByUserId(1)).Returns(cart);

        // Act
        var result = _cartService.GetOrCreateCart("testuser");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.CartId);
        Assert.Empty(result.CartItems);
        _userRepositoryMock.Verify(repo => repo.GetByUsername("testuser"), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.GetByUserId(1), Times.Once);
    }

    [Fact]
    public void GetOrCreateCart_CreatesCart_WhenCartDoesNotExist()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser" };
        var newCart = new Cart { Id = 1, UserId = 1 };

        _userRepositoryMock.Setup(repo => repo.GetByUsername("testuser")).Returns(user);
        _cartRepositoryMock.Setup(repo => repo.GetByUserId(1)).Returns((Cart?)null);
        _cartRepositoryMock.Setup(repo => repo.Create(It.IsAny<Cart>())).Returns(newCart);

        // Act
        var result = _cartService.GetOrCreateCart("testuser");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.CartId);
        _userRepositoryMock.Verify(repo => repo.GetByUsername("testuser"), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.GetByUserId(1), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.Create(It.IsAny<Cart>()), Times.Once);
    }

    [Fact]
    public void AddItemToCart_AddsItem_WhenProductExists()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser" };
        var cart = new Cart { Id = 1, UserId = 1, CartItems = new List<CartItem>() };
        var product = new Product { Id = 101, Name = "Product1", Price = 20.0 };

        _userRepositoryMock.Setup(repo => repo.GetByUsername("testuser")).Returns(user);
        _cartRepositoryMock.Setup(repo => repo.GetByUserId(1)).Returns(cart);
        _productRepositoryMock.Setup(repo => repo.GetById(101)).Returns(product);

        // Act
        var result = _cartService.AddItemToCart("testuser", 101);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.CartItems);
        Assert.Equal("Product1", result.CartItems.First().ProductName);
        _userRepositoryMock.Verify(repo => repo.GetByUsername("testuser"), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.GetByUserId(1), Times.Once);
        _productRepositoryMock.Verify(repo => repo.GetById(101), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.Update(It.IsAny<Cart>()), Times.Once);
    }


    [Fact]
    public void RemoveItemFromCart_RemovesItem_WhenItemExists()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser" };
        var cart = new Cart
        {
            Id = 1,
            UserId = 1,
            CartItems = new List<CartItem>
            {
                new CartItem { Id = 201, ProductId = 101, Price = 20.0 }
            }
        };

        _userRepositoryMock.Setup(repo => repo.GetByUsername("testuser")).Returns(user);
        _cartRepositoryMock.Setup(repo => repo.GetByUserId(1)).Returns(cart);

        // Act
        var result = _cartService.RemoveItemFromCart("testuser", 201);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.CartItems);
        _userRepositoryMock.Verify(repo => repo.GetByUsername("testuser"), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.GetByUserId(1), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.Update(It.IsAny<Cart>()), Times.Once);
    }

    [Fact]
    public void ClearCart_RemovesAllItems_FromCart()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser" };
        var cart = new Cart
        {
            Id = 1,
            UserId = 1,
            CartItems = new List<CartItem>
            {
                new CartItem { Id = 201, ProductId = 101, Price = 20.0 },
                new CartItem { Id = 202, ProductId = 102, Price = 30.0 }
            }
        };

        _userRepositoryMock.Setup(repo => repo.GetByUsername("testuser")).Returns(user);
        _cartRepositoryMock.Setup(repo => repo.GetByUserId(1)).Returns(cart);

        // Act
        _cartService.ClearCart("testuser");

        // Assert
        Assert.Empty(cart.CartItems);
        _userRepositoryMock.Verify(repo => repo.GetByUsername("testuser"), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.GetByUserId(1), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.Update(It.IsAny<Cart>()), Times.Once);
    }
}
