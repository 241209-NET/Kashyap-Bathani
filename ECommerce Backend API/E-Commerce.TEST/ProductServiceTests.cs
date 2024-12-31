using Xunit;
using Moq;
using ECommerce.API.Services;
using ECommerce.API.Models;
using ECommerce.API.Repository;
using System.Collections.Generic;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly ProductServices _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _productService = new ProductServices(_productRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public void CreateProduct_Success_WhenUserIsAdmin()
    {
        // Arrange
        var adminUser = new User { Id = 1, IsAdmin = true };
        var product = new Product { Name = "Test Product", Price = 50.0 };

        _userRepositoryMock.Setup(repo => repo.GetById(1)).Returns(adminUser);
        _productRepositoryMock.Setup(repo => repo.Create(It.IsAny<Product>())).Returns<Product>(p => p);

        // Act
        var result = _productService.CreateProduct(product, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Name);
        _userRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
        _productRepositoryMock.Verify(repo => repo.Create(It.Is<Product>(p => p.Name == "Test Product")), Times.Once);
    }

    [Fact]
    public void CreateProduct_ThrowsUnauthorized_WhenUserIsNotAdmin()
    {
        // Arrange
        var nonAdminUser = new User { Id = 2, IsAdmin = false };
        var product = new Product { Name = "Test Product", Price = 50.0 };

        _userRepositoryMock.Setup(repo => repo.GetById(2)).Returns(nonAdminUser);

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => _productService.CreateProduct(product, 2));
        _userRepositoryMock.Verify(repo => repo.GetById(2), Times.Once);
        _productRepositoryMock.Verify(repo => repo.Create(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public void GetProduct_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Existing Product" };
        _productRepositoryMock.Setup(repo => repo.GetById(1)).Returns(product);

        // Act
        var result = _productService.GetProduct(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Existing Product", result.Name);
        _productRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public void GetProduct_ReturnsNull_WhenProductDoesNotExist()
    {
        // Arrange
        _productRepositoryMock.Setup(repo => repo.GetById(1)).Returns((Product?)null);

        // Act
        var result = _productService.GetProduct(1);

        // Assert
        Assert.Null(result);
        _productRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public void GetAllProducts_ReturnsListOfProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1" },
            new Product { Id = 2, Name = "Product2" }
        };

        _productRepositoryMock.Setup(repo => repo.GetAll()).Returns(products);

        // Act
        var result = _productService.GetAllProducts();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _productRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public void UpdateProduct_Success_WhenUserIsAdmin()
    {
        // Arrange
        var adminUser = new User { Id = 1, IsAdmin = true };
        var existingProduct = new Product { Id = 1, Name = "Old Product", Price = 20.0 };
        var updatedProduct = new Product { Name = "Updated Product", Price = 30.0 };

        _userRepositoryMock.Setup(repo => repo.GetById(1)).Returns(adminUser);
        _productRepositoryMock.Setup(repo => repo.GetById(1)).Returns(existingProduct);
        _productRepositoryMock.Setup(repo => repo.Update(It.IsAny<Product>())).Returns<Product>(p => p);

        // Act
        var result = _productService.UpdateProduct(1, updatedProduct, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Product", result.Name);
        Assert.Equal(30.0, result.Price);
        _productRepositoryMock.Verify(repo => repo.Update(It.Is<Product>(p => p.Name == "Updated Product")), Times.Once);
    }

    [Fact]
    public void UpdateProduct_ThrowsUnauthorized_WhenUserIsNotAdmin()
    {
        // Arrange
        var nonAdminUser = new User { Id = 2, IsAdmin = false };
        var updatedProduct = new Product { Name = "Updated Product", Price = 30.0 };

        _userRepositoryMock.Setup(repo => repo.GetById(2)).Returns(nonAdminUser);

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => _productService.UpdateProduct(1, updatedProduct, 2));
        _userRepositoryMock.Verify(repo => repo.GetById(2), Times.Once);
        _productRepositoryMock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public void DeleteProduct_RemovesProduct_WhenUserIsAdmin()
    {
        // Arrange
        var adminUser = new User { Id = 1, IsAdmin = true };
        var product = new Product { Id = 1, Name = "Product To Delete" };

        _userRepositoryMock.Setup(repo => repo.GetById(1)).Returns(adminUser);
        _productRepositoryMock.Setup(repo => repo.GetById(1)).Returns(product);

        // Act
        _productService.DeleteProduct(1, 1);

        // Assert
        _productRepositoryMock.Verify(repo => repo.Delete(It.Is<Product>(p => p.Id == 1)), Times.Once);
    }

    [Fact]
    public void DeleteProduct_ThrowsUnauthorized_WhenUserIsNotAdmin()
    {
        // Arrange
        var nonAdminUser = new User { Id = 2, IsAdmin = false };

        _userRepositoryMock.Setup(repo => repo.GetById(2)).Returns(nonAdminUser);

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => _productService.DeleteProduct(1, 2));
        _userRepositoryMock.Verify(repo => repo.GetById(2), Times.Once);
        _productRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Never);
    }
}
