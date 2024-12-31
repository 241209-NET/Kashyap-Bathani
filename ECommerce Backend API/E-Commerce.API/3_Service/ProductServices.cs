using ECommerce.API.Repository;
using ECommerce.API.Models;

namespace ECommerce.API.Services;


public class ProductServices : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public ProductServices(IProductRepository productRepository, IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public Product CreateProduct(Product product, int userId)
    {
        var user = _userRepository.GetById(userId)
            ?? throw new Exception("User not found");
        if (!user.IsAdmin)
            throw new UnauthorizedAccessException("Only admins can create products.");

        return _productRepository.Create(product);
    }

    public Product? GetProduct(int id)
    {
        return _productRepository.GetById(id);
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _productRepository.GetAll();
    }

    public Product UpdateProduct(int id, Product updated, int userId)
    {
        var user = _userRepository.GetById(userId)
            ?? throw new Exception("User not found");
        if (!user.IsAdmin)
            throw new UnauthorizedAccessException("Only admins can update products.");

        var existing = _productRepository.GetById(id)
            ?? throw new Exception("Product not found");

        existing.Name = updated.Name;
        existing.Description = updated.Description;
        existing.Price = updated.Price;

        return _productRepository.Update(existing);
    }

    public void DeleteProduct(int id, int userId)
    {
        var user = _userRepository.GetById(userId)
            ?? throw new Exception("User not found");
        if (!user.IsAdmin)
            throw new UnauthorizedAccessException("Only admins can delete products.");

        var existing = _productRepository.GetById(id);
        if (existing == null) return;
        _productRepository.Delete(existing);
    }
}

