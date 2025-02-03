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
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetDoctorUserAsync()
    {
        var doctorId = HttpContext.User.GetUserId();
        var getDoc = await doctorUserService.GetDoctorById(doctorId);
        return Ok(getDoc);
    }
    [HttpPost]
    public async Task<IActionResult> CreateDoctorUserAsync([FromBody] DoctorUserRequestModel doctorUserRequest)
    {
        var mapping = doctorUserRequest.Map();
        var createDoc = await doctorUserService.CreateDoctor(mapping);
        return Ok(createDoc);
    }

    [HttpPost("DoctorUserLogin")]
    public async Task<ActionResult<DoctorUserLoginRequestModel>> LoginUserAsync([FromBody] DoctorUserLoginRequestModel request)
    {
        var user = request.Map();
        var result = await doctorUserService.LoginUserAsync(user);
        if (result.AuthenticateResult == false)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut]
    public async Task<IActionResult> UpdateDoctorUserAsync([FromBody] DoctorUserRequestModel doctorUserRequest)
    {
        var mapping = doctorUserRequest.Map();
        var doctorId = HttpContext.User.GetUserId();
        var updateDoc = await doctorUserService.UpdateDoctorById(doctorId, mapping);
        return Ok(updateDoc);
    }

    [Authorize(Roles = "Doctor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorUserAsync([FromRoute] Guid id)
    {
        var deleteDoc = await doctorUserService.DeleteDoctorById(id);
        return Ok("Deleted");
    }
}
