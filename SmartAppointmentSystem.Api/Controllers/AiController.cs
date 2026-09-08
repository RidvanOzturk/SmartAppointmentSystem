using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs.AI;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

namespace SmartAppointmentSystem.Api.Controllers;

[ApiController]
[EnableRateLimiting("fixed")]
[Route("api/ai")]
public sealed class AiController(IAiChatService aiChatService, IAppointmentService appointmentService) : ControllerBase
{
    [HttpPost("chat")]
    public async Task<ActionResult<AiChatResponseModel>> Chat([FromBody] AiChatRequestModel request, CancellationToken cancellationToken)
    {
        string response = await aiChatService.ChatAsync(request.Prompt, cancellationToken);

        return Ok(new AiChatResponseModel(response));
    }
    [HttpPost("appointment-search")]
    public async Task<ActionResult<AppointmentSearchResponseDTO>> SearchAppointments([FromBody] AiChatRequestModel request, CancellationToken cancellationToken)
    {
        AppointmentSearchIntent search = await aiChatService.ParseAppointmentSearchAsync(request.Prompt, cancellationToken);

        if (string.IsNullOrWhiteSpace(search.Branch))
            return BadRequest("Branch information is required.");

        if (!search.Date.HasValue)
            return BadRequest("Date information is required.");

        List<AvailableAppointmentResponseDTO> appointments = await appointmentService.GetAvailableAppointmentsAsync(search.Branch, search.Date.Value, search.AfterTime, cancellationToken);

        string branch = appointments.FirstOrDefault()?.Branch ?? search.Branch;

        var items = appointments
            .Select(x => new AppointmentSearchItemResponseDTO(x.DoctorId, x.DoctorName, x.Time))
            .ToList();

        return Ok(new AppointmentSearchResponseDTO(branch, search.Date.Value, items));
    }
}