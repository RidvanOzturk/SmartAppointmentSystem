using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController(IPatientUserService userPatientService) : Controller
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<IActionResult> GetPatientUser(CancellationToken cancellationToken)
    {
        var patientId = HttpContext.User.GetUserId();
        await userPatientService.GetPatientUserByIdAsync(patientId, cancellationToken);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientUserById(Guid id, CancellationToken cancellationToken)
    {
        var patient = await userPatientService.GetPatientUserByIdAsync(id, cancellationToken);

        if (patient == null)
        {
            return NotFound();
        }
        return Ok(patient);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<PatientUserRequestModel>> PatientUserLogin([FromBody] PatientUserLoginRequestModel request, CancellationToken cancellationToken)
    {
        var patientEntity = request.Map();
        var patient = await userPatientService.LoginPatientUserAsync(patientEntity, cancellationToken);

        if (patient == null)
        {
            return BadRequest();
        }

        return Ok(patient);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPatients(CancellationToken cancellationToken)
    {
        var patients = await userPatientService.GetPatientUsersAsync(cancellationToken);

        if (patients.Count == 0)
        {
            return BadRequest();
        }

        return Ok(patients);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> CreatePatientUser(PatientUserRequestModel request, [FromServices] IValidator<PatientUserRequestModel> validator, CancellationToken cancellationToken)
    {
        //validator will be added

        var patientEntity = request.Map();
        var patient = await userPatientService.CreatePatientAsync(patientEntity, cancellationToken);

        if (patient == false)
        {
            return BadRequest("The patient is already registered.");
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatientUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var isPatientExist = await userPatientService.IsPatientExistAsync(id, cancellationToken);
        if (!isPatientExist)
        {
            return NotFound();
        }
        await userPatientService.DeletePatientUserByIdAsync(id, cancellationToken);
        return Ok();
    }
}
