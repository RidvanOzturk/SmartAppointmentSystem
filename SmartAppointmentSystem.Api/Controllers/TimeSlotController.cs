using Microsoft.AspNetCore.Mvc;

namespace SmartAppointmentSystem.Api.Controllers
{
    public class TimeSlotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
