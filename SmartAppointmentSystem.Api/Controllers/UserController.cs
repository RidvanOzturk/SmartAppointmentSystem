using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Api.Models.Validators;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;
using SmartAppointmentSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : Controller
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await userService.GetUserByIdAsync(id);

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
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetUsersAsync();
        if (users.Count < 1 || users == null)
        {
            return BadRequest();
        }
        return Ok(users);
    }


    [HttpPost]
    public async Task<IActionResult> CreateUser(UserRequestModel request, [FromServices] IValidator<UserRequestModel> validator) // fromservice???
    {
        var fluent = await validator.ValidateAsync(request);
        if (!fluent.IsValid)
        {
            return BadRequest();
        }
        var user = request.Map();
        var gettingUser = await userService.RegisterAsync(user);
        if (!gettingUser)
        {
            return BadRequest("User invalid");
        }
        return Ok(gettingUser);
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
        user.Email = request.Email;
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
