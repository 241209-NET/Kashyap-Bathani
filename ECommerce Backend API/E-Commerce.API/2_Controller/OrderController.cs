using ECommerce.API.Models;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("user/{username}")]
    public ActionResult<OrderDto> CreateOrder(string username)
    {
        try
        {
            var orderDto = _orderService.CreateOrderFromCart(username);
            return Ok(orderDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{orderId}")]
    public ActionResult<OrderDto> GetOrder(int orderId)
    {
        try
        {
            var orderDto = _orderService.GetOrder(orderId);
            return Ok(orderDto);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}


