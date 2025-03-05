using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequestModel requestModel, CancellationToken cancellationToken = default)
    {
        var appointment = requestModel.Map();
        await appointmentService.CreateAppointmentAsync(appointment, cancellationToken);
        return Ok();
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAppointments(CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentService.GetAllAppointmentsAsync(cancellationToken);
        if (appointments.Count == 0)
        {
            return NotFound();
        }
        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(Guid id, CancellationToken cancellationToken = default)
    {
        var appointment = await appointmentService.GetAppointmentsByIdAsync(id, cancellationToken);
        if (appointment == null)
        {
            return NotFound();
        }
        return Ok(appointment);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("patient-appointments")]
    public async Task<IActionResult> GetPatientsAppointments(CancellationToken cancellationToken = default)
    {
        var userId = HttpContext.User.GetUserId();
        var appointments = await appointmentService.GetUserAppointmentsAsync(userId, cancellationToken);

        if (appointments.Count != 0)
        {
            return NotFound();
        }

        return Ok(appointments);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment([FromRoute] Guid id, AppointmentRequestModel request, CancellationToken cancellationToken = default)
    {
        var appointment = await appointmentService.GetAppointmentsByIdAsync(id, cancellationToken);

        if (appointment == null)
        {
            return NotFound();
        }

        var UpdateAppointment = request.Map();
        await appointmentService.UpdateAppointmentByIdAsync(id, UpdateAppointment, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var app = await appointmentService.DeleteAppointmentByIdAsync(id, cancellationToken);
        return Ok();
    }
}
