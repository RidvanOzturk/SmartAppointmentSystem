using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers;

[ApiController]
[EnableRateLimiting("fixed")]
[Route("api/ai")]
public sealed class AiController(IAiChatService aiChatService) : ControllerBase
{
    [HttpPost("chat")]
    public async Task<ActionResult<AiChatResponseModel>> Chat(
        [FromBody] AiChatRequestModel request,
        CancellationToken cancellationToken)
    {
        string response = await aiChatService.ChatAsync(
            request.Prompt,
            cancellationToken);

        return Ok(new AiChatResponseModel(response));
    }
}
