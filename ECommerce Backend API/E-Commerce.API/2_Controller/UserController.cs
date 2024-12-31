using ECommerce.API.Models;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        return Ok(_userService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _userService.GetUser(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        var created = _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult<User> UpdateUser(int id, [FromBody] User user)
    {
        try
        {
            var updated = _userService.UpdateUser(id, user);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        return NoContent();
    }
}

