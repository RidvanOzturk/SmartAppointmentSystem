using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;
namespace SmartAppointmentSystem.Api.Controllers
{
    public class UserController(IUserBusiness userBusiness) : Controller
    {

        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Register(RegisterRequestModel request)
        {
            if (!ModelState.IsValid || request == null)
            {
                return BadRequest();
            }
            var requestDTO = request.RegisterMap();
            return Ok();
        }
        public IActionResult Index()
        {
            return Ok();
        }

    }
}
