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
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequestModel requestModel)
    {
        var fill = requestModel.Map();
        var gettingFilled = await appointmentService.CreateAppointmentAsync(fill);
        if (!gettingFilled)
        {
            return StatusCode(500, "Appointment could not create");
        }
        return Ok(gettingFilled);
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAppointments()
    {
        var getAllApp = await appointmentService.GetAllAppointmentsAsync();
        if (getAllApp.Count < 1 || getAllApp == null)
        {
            return NotFound("There is no appointment.");
        }
        return Ok(getAllApp);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        var getAppo = await appointmentService.GetAppointmentsByIdAsync(id);
        if (getAppo == null)
        {
            return NotFound();
        }
        return Ok(getAppo);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("patient-appointments")]
    public async Task<IActionResult> GetPatientsAppointments()
    {
        var userId = HttpContext.User.GetUserId();
        var appointments = await appointmentService.GetUserAppointmentsAsync(userId);

        if (appointments == null || !appointments.Any())
        {
            return NotFound("There is no appointment.");
        }

        return Ok(appointments);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment([FromRoute] Guid id, AppointmentRequestModel request)
    {
        var appointmentId = await appointmentService.GetAppointmentsByIdAsync(id);

        if (appointmentId == null)
        {
            return NotFound();
        }

        var appointmentMapping = request.Map();
        var app = await appointmentService.UpdateAppointmentByIdAsync(id, appointmentMapping);
        return Ok(app);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment([FromRoute] Guid id)
    {
        var app = await appointmentService.DeleteAppointmentByIdAsync(id);
        if (!app)
        {
            return BadRequest();
        }
        return Ok("Deleted Appoinment");
    }
}
