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
    public async Task<IActionResult> CreateTimeSlot([FromBody] TimeSlotRequestModel timeSlotRequest)
    {
        var mapping = timeSlotRequest.Map();
        var timeSlot = await timeSlotService.CreateTimeSlotAsync(mapping);

        return Ok(timeSlot);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTimeSlot(Guid id)
    {
        var getTimeSlot = await timeSlotService.GetTimeSlotByIdAsync(id);
        if (getTimeSlot == null)
        {
            return NotFound();
        }
        return Ok(getTimeSlot);
    }
    [HttpGet("doctor-timeslot/{id}")]
    public async Task<IActionResult> GetDoctorTimeSlots(Guid id)
    {
        var getDoctorTs = await timeSlotService.GetDoctorTimeSlotsAsync(id);
        if (getDoctorTs == null)
        {
            return NotFound();
        }
        return Ok(getDoctorTs);
    }
    [HttpGet("avaliable-doctor-timeslot/{id}")]
    public async Task<IActionResult> AvailableTimeSlotsDoctor(Guid id)
    {
        var getSuitApp = await timeSlotService.AvailableTimeSlotDoctorAsync(id);
        if (getSuitApp == null)
        {
            return NotFound();
        }
        return Ok(getSuitApp);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTimeSlots()
    {
        var getAll = await timeSlotService.GetAllTimeSlotsAsync();
        if (getAll == null)
        {
            return BadRequest();
        }
        return Ok(getAll);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTimeSlot([FromRoute] Guid id, TimeSlotRequestModel timeSlotRequest)
    {
        var getId = await timeSlotService.GetTimeSlotByIdAsync(id);
        if (getId == null)
        {
            return NotFound();
        }
        var mapping = timeSlotRequest.Map();
        var timeSlot = await timeSlotService.UpdateTimeSlotByIdAsync(id, mapping);
        return Ok(timeSlot);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimeSlot([FromRoute] Guid id)
    {
        var delete = await timeSlotService.DeleteTimeSlotByIdAsync(id);
        if (!delete)
        {
            return NotFound();
        }
        return Ok(delete);
    }

}
