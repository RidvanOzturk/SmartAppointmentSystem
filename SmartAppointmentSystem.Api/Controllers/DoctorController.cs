using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetDoctorUser(CancellationToken cancellationToken)
    {
        var doctorId = HttpContext.User.GetUserId();
        var doctor = await doctorUserService.GetDoctorByIdAsync(doctorId, cancellationToken);
        return Ok(doctor);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await doctorUserService.GetDoctorByIdAsync(id, cancellationToken);
        if (doctor == null)
        {
            return NotFound();
        }
        return Ok(doctor);
    }

    [HttpGet("new-doctors")]
    public async Task<IActionResult> GetNewAddedDoctors(CancellationToken cancellationToken)
    {
        var doctor = await doctorUserService.GetNewAddedDoctors(cancellationToken);
        return Ok(doctor);
    }

    [HttpGet("top-rated")]
    public async Task<IActionResult> GetTopRatedDoctors(CancellationToken cancellationToken)
    {
        var doctors = await doctorUserService.GetTopRatedDoctorsAsync(cancellationToken);
        if (doctors.Count == 0)
        {
            return NotFound();
        }
        return Ok(doctors);
    }

    [HttpGet("most-appointment")]
    public async Task<IActionResult> GetMostAppointmentDoctor(CancellationToken cancellationToken)
    {
        var doctor = await doctorUserService.GetDoctorsWithMostAppointmentsAsync(cancellationToken);
        if (doctor == null)
        {
            return NotFound();
        }
        return Ok(doctor);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetDoctorByNameSearch([FromQuery] string query, CancellationToken cancellationToken)
    {
        var doctors = await doctorUserService.SearchDoctorsNameAsync(query, cancellationToken);
        if (doctors.Count == 0)
        {
            return NotFound();
        }
        return Ok(doctors);
    }

    [HttpGet("search-branch")]
    public async Task<IActionResult> GetDoctorByBranchSearch([FromQuery] int query, CancellationToken cancellationToken)
    {
        var doctors = await doctorUserService.SearchDoctorsBranchAsync(query, cancellationToken);
        if (doctors.Count == 0)
        {
            return NotFound();
        }
        return Ok(doctors);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> CreateDoctorUser([FromBody] DoctorUserSignUpRequestModel doctorUserRequest, CancellationToken cancellationToken)
    {
        var doctorEntity = doctorUserRequest.Map();
        var doctor = await doctorUserService.CreateDoctorAsync(doctorEntity, cancellationToken);
        if (doctor == false)
        {
            return BadRequest("The doctor is already registered.");
        }
        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllDoctors(CancellationToken cancellationToken)
    {
        var doctors = await doctorUserService.GetAllDoctorsAsync(cancellationToken);
        return Ok(doctors);
    }

    [HttpPost("login")]
    public async Task<ActionResult<DoctorUserLoginRequestModel>> LoginUser([FromBody] DoctorUserLoginRequestModel request, CancellationToken cancellationToken)
    {
        var doctorEntity = request.Map();
        var doctor = await doctorUserService.LoginUserAsync(doctorEntity, cancellationToken);
        if (doctor == null)
        {
            return BadRequest();
        }
        return Ok(doctor);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctorUser([FromRoute] Guid id, [FromBody] DoctorUserRequestModel doctorUserRequest, CancellationToken cancellationToken)
    {
        var isDoctorExist = await doctorUserService.IsDoctorExistAsync(id, cancellationToken);
        if (!isDoctorExist)
        {
            return NotFound();
        }
        var doctor = doctorUserRequest.Map();
        await doctorUserService.UpdateDoctorByIdAsync(id, doctor, cancellationToken);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var isDoctorExist = await doctorUserService.IsDoctorExistAsync(id, cancellationToken);
        if (!isDoctorExist)
        {
            return NotFound();
        }
        await doctorUserService.DeleteDoctorByIdAsync(id, cancellationToken);
        return Ok();
    }
    //[HttpPost("refresh-token")]
    //[AllowAnonymous]
    //public async Task<IActionResult> Refresh([FromBody] RefreshRequest request, CancellationToken cancellationToken)
    //{
    //    var result = await doctorUserService.RefreshTokenAsync(request.RefreshToken, cancellationToken);
    //    if (result == null)
    //    {
    //        return Unauthorized("Authorize Time Out");
    //    }
    //    return Ok(result);
    //}
}
