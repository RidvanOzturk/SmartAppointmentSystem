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
    public async Task<IActionResult> GetDoctorUser()
    {
        var doctorId = HttpContext.User.GetUserId();
        var getDoc = await doctorUserService.GetDoctorByIdAsync(doctorId);
        return Ok(getDoc);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        var getDocById = await doctorUserService.GetDoctorByIdAsync(id);
        if (getDocById == null) 
        { 
            return NotFound("Doctor not found!");
        }
        return Ok(getDocById);
    }

    [HttpGet("toprated")]
    public async Task<IActionResult> GetTopRatedDoctors()
    {
        var doctors = await doctorUserService.GetTopRatedDoctorsAsync();
        if (doctors.Count < 1 || doctors == null)
        {
            return NotFound("There are no doctors with ratings.");
        }
        return Ok(doctors);
    }

    [HttpGet("most-appointment")]
    public async Task<IActionResult> GetMostAppointmentDoctor()
    {
        var doctor = await doctorUserService.GetDoctorWithMostAppointmentsAsync();
        if (doctor == null)
        {
            return NotFound("There are no doctors with most appointmnet");
        }
        return Ok(doctor);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetDoctorBySearch([FromQuery] string query)
    {
        var doctors = await doctorUserService.SearchDoctorsNameAsync(query);
        if (doctors == null || doctors.Count < 1) 
        {
            return NotFound("There is no doctor like "+$"{query}");
        }
        return Ok(doctors);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> CreateDoctorUser([FromBody] DoctorUserRequestModel doctorUserRequest)
    {
        var mapping = doctorUserRequest.Map();
        var createDoc = await doctorUserService.CreateDoctorAsync(mapping);
        return Ok(createDoc);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllDoctors()
    {
        var getAllDoc = await doctorUserService.GetAllDoctorsAsync();
        return Ok(getAllDoc);
    }

    [HttpPost("login")]
    public async Task<ActionResult<DoctorUserLoginRequestModel>> LoginUser([FromBody] DoctorUserLoginRequestModel request)
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
    public async Task<IActionResult> UpdateDoctorUser([FromBody] DoctorUserRequestModel doctorUserRequest)
    {
        var mapping = doctorUserRequest.Map();
        var doctorId = HttpContext.User.GetUserId();
        var updateDoc = await doctorUserService.UpdateDoctorByIdAsync(doctorId, mapping);
        return Ok(updateDoc);
    }

    [Authorize(Roles = "Doctor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorUser([FromRoute] Guid id)
    {
        var deleteDoc = await doctorUserService.DeleteDoctorByIdAsync(id);
        return Ok("Deleted");
    }
}
