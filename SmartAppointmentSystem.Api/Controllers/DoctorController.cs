using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController(IDoctorUserService doctorUserService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctor(Guid id)
    {
        var getDoc = await doctorUserService.GetDoctorById(id);
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorUserRequestModel doctorUserRequest)
    {
        var mapping = doctorUserRequest.Map();
        var createDoc = await doctorUserService.CreateDoctor(mapping);
        return Ok(createDoc);
    }


}
