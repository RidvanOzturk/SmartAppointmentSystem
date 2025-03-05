using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimeSlotController(ITimeSlotService timeSlotService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTimeSlot([FromBody] TimeSlotRequestModel timeSlotRequest, CancellationToken cancellationToken)
    {
        var timeSlot = timeSlotRequest.Map();
        await timeSlotService.CreateTimeSlotAsync(timeSlot, cancellationToken);
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTimeSlot(Guid id, CancellationToken cancellationToken)
    {
        var timeSlot = await timeSlotService.GetTimeSlotByIdAsync(id, cancellationToken);
        if (timeSlot == null)
        {
            return NotFound();
        }
        return Ok(timeSlot);
    }
    [HttpGet("doctor-timeslot/{id}")]
    public async Task<IActionResult> GetDoctorTimeSlots(Guid id, CancellationToken cancellationToken)
    {
        var timeSlot = await timeSlotService.GetDoctorTimeSlotsAsync(id, cancellationToken);
        if (timeSlot == null)
        {
            return NotFound();
        }
        return Ok(timeSlot);
    }
    [HttpGet("avaliable-doctor-timeslot/{id}")]
    public async Task<IActionResult> AvailableTimeSlotsDoctor(Guid id, CancellationToken cancellationToken)
    {
        var timeSlots = await timeSlotService.AvailableTimeSlotDoctorAsync(id, cancellationToken);
        if (timeSlots == null)
        {
            return NotFound();
        }
        return Ok(timeSlots);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTimeSlots(CancellationToken cancellationToken)
    {
        var timeSlots = await timeSlotService.GetAllTimeSlotsAsync(cancellationToken);
        return Ok(timeSlots);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTimeSlot([FromRoute] Guid id, TimeSlotRequestModel timeSlotRequest, CancellationToken cancellationToken)
    {
        var isTimeSlotExist = await timeSlotService.IsTimeSlotExistAsync(id, cancellationToken);
        if (!isTimeSlotExist)
        {
            return NotFound();
        }
        var updateTimeSlot = timeSlotRequest.Map();
        await timeSlotService.UpdateTimeSlotByIdAsync(id, updateTimeSlot, cancellationToken);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimeSlot([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var isTimeSlotExist = await timeSlotService.IsTimeSlotExistAsync(id, cancellationToken);
        if (!isTimeSlotExist)
        {
            return NotFound();
        }
        await timeSlotService.DeleteTimeSlotByIdAsync(id, cancellationToken);
        return Ok();
    }

}
