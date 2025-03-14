﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController(IDoctorUserService doctorUserService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetDoctorUser(CancellationToken cancellationToken)
    {
        var doctorId = HttpContext.User.GetUserId();
        var doctor = await doctorUserService.GetDoctorByIdAsync(doctorId, cancellationToken);
        return Ok(doctor);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await doctorUserService.GetDoctorByIdAsync(id, cancellationToken);
        if (doctor == null) 
        { 
            return NotFound();
        }
        return Ok(doctor);
    }

    [HttpGet("new-doctors")]
    public async Task<IActionResult> GetNewAddedDoctors(CancellationToken cancellationToken)
    {
        var doctor = await doctorUserService.GetNewAddedDoctors(cancellationToken);
        return Ok(doctor);
    }

    [HttpGet("top-rated")]
    public async Task<IActionResult> GetTopRatedDoctors(CancellationToken cancellationToken)
    {
        var doctors = await doctorUserService.GetTopRatedDoctorsAsync(cancellationToken);
        if (doctors.Count == 0)
        {
            return NotFound();
        }
        return Ok(doctors);
    }

    [HttpGet("most-appointment")]
    public async Task<IActionResult> GetMostAppointmentDoctor(CancellationToken cancellationToken)
    {
        var doctor = await doctorUserService.GetDoctorsWithMostAppointmentsAsync(cancellationToken);
        if (doctor == null)
        {
            return NotFound();
        }
        return Ok(doctor);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetDoctorBySearch([FromQuery] string query, CancellationToken cancellationToken)
    {
        var doctors = await doctorUserService.SearchDoctorsNameAsync(query, cancellationToken);
        if (doctors.Count == 0) 
        {
            return NotFound();
        }
        return Ok(doctors);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> CreateDoctorUser([FromBody] DoctorUserSignUpRequestModel doctorUserRequest, CancellationToken cancellationToken)
    {
        var doctor = doctorUserRequest.Map();
        await doctorUserService.CreateDoctorAsync(doctor, cancellationToken);
        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllDoctors(CancellationToken cancellationToken)
    {
        var doctors = await doctorUserService.GetAllDoctorsAsync(cancellationToken);
        return Ok(doctors);
    }

    [HttpPost("login")]
    public async Task<ActionResult<DoctorUserLoginRequestModel>> LoginUser([FromBody] DoctorUserLoginRequestModel request, CancellationToken cancellationToken)
    {
        var doctorEntity = request.Map();
        var doctor = await doctorUserService.LoginUserAsync(doctorEntity, cancellationToken);
        if (!doctor.AuthenticateResult)
        {
            return BadRequest();
        }

        return Ok(doctor);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctorUser([FromRoute] Guid id, [FromBody] DoctorUserRequestModel doctorUserRequest , CancellationToken cancellationToken)
    {
        // var doctorId = HttpContext.User.GetUserId();
        var isDoctorExist = await doctorUserService.IsDoctorExistAsync(id, cancellationToken);
        if (!isDoctorExist)
        {
            return NotFound();
        }
        var doctorEntity = doctorUserRequest.Map();
        await doctorUserService.UpdateDoctorByIdAsync(id, doctorEntity, cancellationToken);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var isDoctorExist = await doctorUserService.IsDoctorExistAsync(id, cancellationToken);
        if (!isDoctorExist)
        {
            return NotFound();
        }
        await doctorUserService.DeleteDoctorByIdAsync(id, cancellationToken);
        return Ok();
    }
}
