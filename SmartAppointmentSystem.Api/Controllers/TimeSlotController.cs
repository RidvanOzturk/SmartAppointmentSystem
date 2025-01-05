using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers
{
    public class TimeSlotController(ITimeSlotBusiness timeSlotBusiness) : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
