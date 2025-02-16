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
    [HttpGet("healthcheck")]
    public IActionResult HealthCheck()
    {
        return Ok("Working properly!!!");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentRequestModel requestModel)
    {
        var fill = requestModel.Map();
        var gettingFilled = await appointmentService.CreateAppointment(fill);
        if (!gettingFilled)
        {
            return StatusCode(500, "Appointment could not create");
        }
        return Ok(gettingFilled);
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAppointmentsAsync()
    {
        var getAllApp = await appointmentService.GetAllAppointments();
        if (getAllApp.Count < 1 || getAllApp == null)
        {
            return NotFound("There is no appointment.");
        }
        return Ok(getAllApp);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointmentAsync(Guid id)
    {
        var getAppo = await appointmentService.GetAppointmentsById(id);
        if (getAppo == null)
        {
            return NotFound();
        }
        return Ok(getAppo);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("apps")]
    public async Task<IActionResult> GetUserAppointmentsAsync()
    {
        var userId = HttpContext.User.GetUserId();
        var appointments = await appointmentService.GetUserAppointments(userId);

        if (appointments == null || !appointments.Any())
        {
            return NotFound("There is no appointment.");
        }

        return Ok(appointments);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointmentAsync([FromRoute] Guid id, AppointmentRequestModel request)
    {
        var appointmentId = await appointmentService.GetAppointmentsById(id);

        if (appointmentId == null)
        {
            return NotFound();
        }

        var appointmentMapping = request.Map();
        var app = await appointmentService.UpdateAppointmentById(id, appointmentMapping);
        return Ok(app);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointmentAsync([FromRoute] Guid id)
    {
        var app = await appointmentService.DeleteAppointmentById(id);
        if (!app)
        {
            return BadRequest();
        }
        return Ok("Deleted Appoinment");
    }
}
