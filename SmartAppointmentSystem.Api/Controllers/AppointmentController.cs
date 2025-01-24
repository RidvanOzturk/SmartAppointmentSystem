using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAppointment(AppointmentRequestModel requestModel)
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
    public async Task<IActionResult> GetAllAppointments()
    {
        var getAllApp = await appointmentService.GetAllAppointments();
        if (getAllApp.Count < 1  || getAllApp == null)
        {
            return NotFound("There is no appointment.");
        }
        return Ok(getAllApp);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        var getAppo = await appointmentService.GetAppointmentsById(id);
        if (getAppo==null)
        {
            return NotFound();
        }
        return Ok(getAppo);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment([FromRoute] Guid id, AppointmentRequestModel request)
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
    public async Task<IActionResult> DeleteAppointment([FromRoute] Guid id)
    {
        var app = await appointmentService.DeleteAppointmentById(id);
        if (!app)
        {
            return BadRequest();
        }
        return Ok("Deleted Appoinment");
    }
}
