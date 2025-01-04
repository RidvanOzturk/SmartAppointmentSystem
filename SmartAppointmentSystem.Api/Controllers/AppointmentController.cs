using Microsoft.AspNetCore.Mvc;

namespace SmartAppointmentSystem.Api.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
