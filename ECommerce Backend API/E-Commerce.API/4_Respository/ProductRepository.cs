using ECommerce.API.Data;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.API.Repository;
public class ProductRepository : IProductRepository
{
    private readonly ECommerceContext _context;

    public ProductRepository(ECommerceContext context)
    {
        _context = context;
    }

    public Product Create(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product? GetById(int id)
    {
        return _context.Products.Find(id);
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public Product Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();
        return product;
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}

