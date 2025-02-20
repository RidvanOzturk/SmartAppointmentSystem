using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
public class PatientController(IPatientUserService userPatientService) : Controller
{
    [Authorize(Roles = "Patient", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<IActionResult> GetUserAsync()
    {
        var userId = HttpContext.User.GetUserId();

        var user = await userPatientService.GetUserByIdAsync(userId);

        if (user == null)
        {
            return NotFound("Patient not found");
        }

        return Ok(user);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid id)
    {
        var user = await userPatientService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("Patient not found");
        }
        return Ok(user);
    }


    [HttpPost("PatientUserLogin")]
    [AllowAnonymous]
    public async Task<ActionResult<PatientUserRequestModel>> LoginUserAsync([FromBody] PatientUserRequestModel request)
    {
        var user = request.Map();
        var result = await userPatientService.LoginUserAsync(user);
        if (result.AuthenticateResult == false)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await userPatientService.GetUsersAsync();
        if (users.Count < 1 || users == null)
        {
            return BadRequest();
        }
        return Ok(users);
    }


    [HttpPost]
    public async Task<IActionResult> CreatePatientUserAsync(PatientUserRequestModel request, [FromServices] IValidator<PatientUserRequestModel> validator)
    {
        var fluent = await validator.ValidateAsync(request);
        if (!fluent.IsValid)
        {
            return BadRequest();
        }
        var user = request.Map();
        var gettingUser = await userPatientService.RegisterAsync(user);
        if (!gettingUser)
        {
            return BadRequest("User invalid");
        }
        return Ok(request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatientUserAsync([FromRoute] Guid id, PatientUserRequestModel request)
    {
        var userId = await userPatientService.GetUserByIdAsync(id);

        if (userId == null)
        {
            return NotFound();
        }

        var user = request.Map();

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatientUserAsync([FromRoute] Guid id)
    {
        var user = await userPatientService.DeleteUserById(id);
        if (!user)
        {
            return BadRequest();
        }
        return Ok();
    }
}
