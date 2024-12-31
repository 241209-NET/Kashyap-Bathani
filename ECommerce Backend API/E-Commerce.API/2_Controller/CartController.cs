using ECommerce.API.Models;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("user/{username}")]
    public ActionResult<CartDto> GetCart(string username)
    {
        try
        {
            var cartDto = _cartService.GetOrCreateCart(username);
            return Ok(cartDto);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("user/{username}/items")]
    public ActionResult<CartDto> AddItem(string username, [FromQuery] int productId)
    {
        try
        {
            var cartDto = _cartService.AddItemToCart(username, productId);
            return Ok(cartDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("user/{username}/items/{cartItemId}")]
    public ActionResult<CartDto> RemoveItem(string username, int cartItemId)
    {
        try
        {
            var cartDto = _cartService.RemoveItemFromCart(username, cartItemId);
            return Ok(cartDto);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("user/{username}/clear")]
    public IActionResult ClearCart(string username)
    {
        try
        {
            _cartService.ClearCart(username);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}

