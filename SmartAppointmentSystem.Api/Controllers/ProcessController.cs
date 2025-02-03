using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProcessController(IProcessService processService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProcessAsync([FromBody] ProcessRequestModel processRequest)
    {
        if (processRequest == null)
        {
            return BadRequest();
        }
        var mapping = processRequest.Map();
        var process = await processService.CreateProcess(mapping);
        return Ok(process);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProcessAsync(Guid id)
    {
        var getProcess = await processService.GetProcessById(id);
        if (getProcess == null)
        {
            return NotFound();
        }
        return Ok(getProcess);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllProcessesAsync()
    {
        var getAll = await processService.GetAllProcesses();
        if (getAll == null)
        {
            return NotFound();
        }
        return Ok(getAll);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProcessAsync([FromRoute] Guid id, ProcessRequestModel processRequestModel)
    {
        var mapping = processRequestModel.Map();
        var process = await processService.UpdateProcessById(id, mapping);
        return Ok(process);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProcessAsync([FromRoute] Guid id)
    {
        var process = await processService.DeleteProcessById(id);
        if (!process)
        {
            NotFound();
        }
        return Ok(process);
    }
}
