using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimeSlotController(ITimeSlotService timeSlotService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTimeSlotAsync([FromBody] TimeSlotRequestModel timeSlotRequest)
    {
        var mapping = timeSlotRequest.Map();
        var timeSlot = await timeSlotService.CreateTimeSlot(mapping);

        return Ok(timeSlot);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTimeSlotAsync(Guid id)
    {
        var getTimeSlot = await timeSlotService.GetTimeSlotById(id);
        if (getTimeSlot == null)
        {
            return NotFound();
        }
        return Ok(getTimeSlot);
    }
    [HttpGet("doctorTimeSlots/{id}")]
    public async Task<IActionResult> GetDoctorTimeSlotsAsync(Guid id)
    {
        var getDoctorTs = await timeSlotService.GetDoctorTimeSlots(id);
        if (getDoctorTs == null)
        {
            return NotFound();
        }
        return Ok(getDoctorTs);
    }
    [HttpGet("availablets/{id}")]
    public async Task<IActionResult> AvailableTimeSlotsDoctorAsync(Guid id)
    {
        var getSuitApp = await timeSlotService.AvailableTimeSlotDoctor(id);
        if (getSuitApp == null)
        {
            return NotFound();
        }
        return Ok(getSuitApp);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTimeSlotsAsync()
    {
        var getAll = await timeSlotService.GetAllTimeSlots();
        if (getAll == null)
        {
            return BadRequest();
        }
        return Ok(getAll);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTimeSlotAsync([FromRoute] Guid id, TimeSlotRequestModel timeSlotRequest)
    {
        var getId = await timeSlotService.GetTimeSlotById(id);
        if (getId == null)
        {
            return NotFound();
        }
        var mapping = timeSlotRequest.Map();
        var timeSlot = await timeSlotService.UpdateTimeSlotById(id, mapping);
        return Ok(timeSlot);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteTimeSlotAsync([FromRoute] Guid id)
    {
        var delete = await timeSlotService.DeleteTimeSlotById(id);
        if (!delete)
        {
            return NotFound();
        }
        return Ok(delete);
    }

}
