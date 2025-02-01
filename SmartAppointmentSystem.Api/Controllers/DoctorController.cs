using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<IActionResult> GetDoctor()
    {
        var doctorId = HttpContext.User.GetUserId();
        var getDoc = await doctorUserService.GetDoctorById(doctorId);
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorUserRequestModel doctorUserRequest)
    {
        var mapping = doctorUserRequest.Map();
        var createDoc = await doctorUserService.CreateDoctor(mapping);
        return Ok(createDoc);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctor([FromRoute] Guid id, [FromBody] DoctorUserRequestModel doctorUserRequest)
    {
        var mapping = doctorUserRequest.Map();
        var updateDoc = await doctorUserService.UpdateDoctorById(id, mapping);
        return Ok(updateDoc);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor([FromRoute] Guid id)
    {

    }


}
