using ECommerce.API.Models;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
        return Ok(_productService.GetAllProducts());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _productService.GetProduct(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct([FromQuery] int userId, [FromBody] Product product)
    {
        try
        {
            var created = _productService.CreateProduct(product, userId);
            return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Product> UpdateProduct(int id, [FromQuery] int userId, [FromBody] Product product)
    {
        try
        {
            var updated = _productService.UpdateProduct(id, product, userId);
            return Ok(updated);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id, [FromQuery] int userId)
    {
        try
        {
            _productService.DeleteProduct(id, userId);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}


