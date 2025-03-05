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
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        var patientId = HttpContext.User.GetUserId();
        await userPatientService.GetUserByIdAsync(patientId, cancellationToken);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var patient = await userPatientService.GetUserByIdAsync(id, cancellationToken);
        if (patient == null)
        {
            return NotFound();
        }
        return Ok(patient);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<PatientUserRequestModel>> LoginUser([FromBody] PatientUserRequestModel request, CancellationToken cancellationToken)
    {
        var patient = request.Map();
        var result = await userPatientService.LoginUserAsync(patient, cancellationToken);
        if (result.AuthenticateResult == false)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPatients(CancellationToken cancellationToken)
    {
        var patients = await userPatientService.GetUsersAsync(cancellationToken);
        if (patients.Count == 0)
        {
            return BadRequest();
        }
        return Ok(patients);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> CreatePatientUser(PatientUserRequestModel request, [FromServices] IValidator<PatientUserRequestModel> validator, CancellationToken cancellationToken)
    {
        var fluent = await validator.ValidateAsync(request);
        if (!fluent.IsValid)
        {
            return BadRequest();
        }
        var patient = request.Map();
        await userPatientService.RegisterAsync(patient, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatientUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await userPatientService.DeleteUserByIdAsync(id, cancellationToken);
        return Ok();
    }
}
