using Microsoft.AspNetCore.Http;
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
    public async Task<IActionResult> CreateAppointment(AppointmentRequestModel requestModel)
    {
        var fill = requestModel.Map();
        var gettingFilled = await appointmentService.CreateAppointment(fill);
        if (!gettingFilled)
        {
            return NotFound("Appointment Not Found");
        }
        return Ok(gettingFilled);
    }
}
