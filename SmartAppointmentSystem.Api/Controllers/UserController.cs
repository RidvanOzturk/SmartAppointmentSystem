using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : Controller
{
    static List<User> users = new List<User>()
    {
        new User { Id = Guid.NewGuid(), Name="Rıdvan", Mail="ozturkridvan1@gmail.com", PasswordHash="12345" }
    };

    [HttpGet("{id}")]
    public IActionResult GetUser([FromRoute] Guid id)
    {
        var user = users.FirstOrDefault(x => x.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    //[HttpGet]
    //public IActionResult GetUserabc([FromQuery] string name, [FromQuery] int length)
    //{
    //    var user = registerRequests.FirstOrDefault(x => x.Name.Contains(name) && x.Name.Length > 3);

    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    return Ok(user);
    //}

    [HttpGet("all")]
    public IActionResult GetAllUsers()
    {
        return Ok(users);
    }


    [HttpPost]
    public IActionResult CreateUser(UserRequestModel request)
    {
        // Bunlar yerine Fluent Validation

        if (string.IsNullOrEmpty(request.Name))
        {
            return BadRequest("Name is not valid");
        }

        if (string.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Email is not valid");
        }

        if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 5)
        {
            return BadRequest("Password is not valid");
        }

        var user = request.Map();

        users.Add(user);

        return Ok(user.Id);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] Guid id, UserRequestModel request)
    {
        var user = users.FirstOrDefault(x => x.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        user.Name = request.Name;
        user.Mail = request.Email;
        user.PasswordHash = request.Password;
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        users.RemoveAll(x => x.Id == id);
        return Ok();
    }
}
