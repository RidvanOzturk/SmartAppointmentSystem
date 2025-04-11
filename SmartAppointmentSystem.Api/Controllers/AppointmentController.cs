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
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequestModel requestModel, CancellationToken cancellationToken = default)
    {
        var appointment = requestModel.Map();
        await appointmentService.CreateAppointmentAsync(appointment, cancellationToken);
        return Ok();
    }

    [Authorize]
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

    [Authorize]
    [HttpGet("available/{id}")]
    public async Task<IActionResult> GetAvailableTimeSlots([FromQuery] Guid id, DateTime date, CancellationToken cancellationToken)
    {
        var appointmentTimeSlot = await appointmentService.GetAvailableTimeSlotsForDoctorAsync(id, date, cancellationToken);
        if (appointmentTimeSlot.Count == 0)
        {
            return NotFound();
        }
        return Ok(appointmentTimeSlot);
    }

    [Authorize]
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

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment([FromRoute] Guid id, AppointmentRequestModel request, CancellationToken cancellationToken = default)
    {
        var isAppointmentExist = await appointmentService.IsAppointmentExistAsync(id, cancellationToken);
        if (!isAppointmentExist)
        {
            return NotFound();
        }
        var updateAppointment = request.Map();
        await appointmentService.UpdateAppointmentByIdAsync(id, updateAppointment, cancellationToken);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var isAppointmentExist = await appointmentService.IsAppointmentExistAsync(id, cancellationToken);
        if (!isAppointmentExist)
        {
            return NotFound();
        }
        await appointmentService.DeleteAppointmentByIdAsync(id, cancellationToken);
        return Ok();
    }
}
